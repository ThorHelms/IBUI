using System;
using System.Linq;
using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI.DOTweenAnimations
{
    public class SlideInOut : MonoBehaviour, IAnimateShow, IAnimateHide
    {
        public float AnimationTime = .2f;
        public string[] NegativeStates = new string[0];
        public string[] DoNotAnimateStates = new string[0];

        private Rect _originalRect;
        private Vector3 _originalPosition;

        void Awake()
        {
            GetOriginalPosition();
            GetOriginalRect();
        }

        public void AnimateShowing(Action onAnimationFinishedCallback, string oldState)
        {
            if (!ShouldAnimateState(oldState))
            {
                onAnimationFinishedCallback?.Invoke();
                return;
            }

            var rect = GetOriginalRect();
            transform.position = GetOriginalPosition();

            var newX = NegativeStates.Any(state => state == oldState)
                ? transform.position.x + rect.width
                : transform.position.x - rect.width;

            transform
                .DOMoveX(newX, AnimationTime)
                .OnComplete(() => onAnimationFinishedCallback?.Invoke())
                .From()
                .SetUpdate(UpdateType.Normal, true)
                ;
        }

        public void AnimateHiding(Action onAnimationFinishedCallback, string newState)
        {
            if (!ShouldAnimateState(newState))
            {
                onAnimationFinishedCallback?.Invoke();
                return;
            }

            var rect = GetOriginalRect();
            transform.position = GetOriginalPosition();

            var newX = NegativeStates.Any(state => state == newState)
                ? transform.position.x + rect.width
                : transform.position.x - rect.width;

            transform
                .DOMoveX(newX, AnimationTime)
                .OnComplete(() => onAnimationFinishedCallback?.Invoke())
                .SetUpdate(UpdateType.Normal, true)
                ;
        }

        private bool ShouldAnimateState(string state)
        {
            return !DoNotAnimateStates.Any(state2 =>
                state2 == state || (String.IsNullOrEmpty(state2) && String.IsNullOrEmpty(state)));
        }

        private Rect GetOriginalRect()
        {
            if (_originalRect == default)
            {
                var rectTransform = GetComponent<RectTransform>();
                _originalRect = rectTransform.rect;
            }

            return _originalRect;
        }

        private Vector3 GetOriginalPosition()
        {
            if (_originalPosition == default)
            {
                _originalPosition = transform.position;
            }

            return _originalPosition;
        }
    }
}