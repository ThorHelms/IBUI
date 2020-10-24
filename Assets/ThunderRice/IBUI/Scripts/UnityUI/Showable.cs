using System;
using System.Linq;
using Assets.IBUI.Interfaces;
using UnityEngine;

namespace Assets.IBUI.UnityUI
{
    public class Showable : MonoBehaviour, IShowable
    {
        private IFocusable _focusable;
        private IActivatable _activatable;
        private IAnimateShow[] _animateShow;
        private IAnimateHide[] _animateHide;

        private bool _initiated = false;

        private void Init()
        {
            if (_initiated)
            {
                return;
            }

            _initiated = true;
            _focusable = GetComponent<IFocusable>();
            _activatable = GetComponent<IActivatable>();
            _animateShow = GetComponents<IAnimateShow>();
            _animateHide = GetComponents<IAnimateHide>();
        }

        void Start()
        {
            Init();
        }

        public void ShowImmediately()
        {
            Init();
            gameObject.SetActive(true);
            if (_focusable != null)
            {
                _focusable.Focus();
            }
        }

        public void Show(string oldState, Action shownCallback = null)
        {
            Init();
            if (_animateShow != null && _animateShow.Length > 0)
            {
                gameObject.SetActive(true);
                if (_activatable != null)
                {
                    _activatable.DeactivateImmediately();
                }

                var finished = new bool[_animateShow.Length];
                for (var i = 0; i < _animateShow.Length; i++)
                {
                    var i1 = i;
                    _animateShow[i].AnimateShowing(() =>
                    {
                        finished[i1] = true;
                        if (!finished.All(f => f)) return;
                        if (_activatable != null)
                        {
                            _activatable.ActivateImmediately();
                            shownCallback?.Invoke();
                        }
                        else if (_focusable != null)
                        {
                            // It is assumed that IActivatable will focus
                            _focusable.Focus();
                            shownCallback?.Invoke();
                        }
                    }, oldState);
                }
            }
            else
            {
                ShowImmediately();
                shownCallback?.Invoke();
            }
        }

        public void HideImmediately()
        {
            Init();
            gameObject.SetActive(false);
        }

        public void Hide(string newState, Action hiddenCallback = null)
        {
            Init();
            if (_animateHide != null && _animateHide.Length > 0)
            {
                if (_activatable != null)
                {
                    _activatable.DeactivateImmediately();
                }

                var finished = new bool[_animateHide.Length];
                for (var i = 0; i < _animateHide.Length; i++)
                {
                    var i1 = i;
                    _animateHide[i].AnimateHiding(() =>
                    {
                        finished[i1] = true;
                        if (finished.All(f => f))
                        {
                            gameObject.SetActive(false);
                            hiddenCallback?.Invoke();
                        }
                    }, newState);
                }
            }
            else
            {
                HideImmediately();
                hiddenCallback?.Invoke();
            }
        }
    }
}