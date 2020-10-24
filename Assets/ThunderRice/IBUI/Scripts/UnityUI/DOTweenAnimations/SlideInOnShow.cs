using System;
using System.Linq;
using Assets.IBUI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Assets.IBUI.UnityUI.DOTweenAnimations
{
    public class SlideInOnShow : MonoBehaviour, IAnimateShow
    {
        public float AnimationTime = 1.0f;
        public string[] NegativeStates = new string[0];

        private Rect _originalRect;
        private Vector3 _originalPosition;

        void Awake()
        {
            GetOriginalPosition();
            GetOriginalRect();
        }

        public void AnimateShowing(Action onAnimationFinishedCallback, string oldState)
        {
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