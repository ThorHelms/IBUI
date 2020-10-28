using System;
using System.Linq;
using Assets.IBUI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Assets.IBUI.UnityUI.DOTweenAnimations
{
    public class SlideOutOnHide : MonoBehaviour, IAnimateHide
    {
        public float AnimationTime = .2f;
        public string[] NegativeStates = new string[0];

        private Rect _originalRect;
        private Vector3 _originalPosition;

        void Awake()
        {
            GetOriginalPosition();
            GetOriginalRect();
        }

        public void AnimateHiding(Action onAnimationFinishedCallback, string newState)
        {
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