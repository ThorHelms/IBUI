using System.Collections.Generic;
using System.Linq;
using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI
{
    public class Activatable : MonoBehaviour, IActivatable
    {
        private IFocusable _onFocused;
        private IAnimateActivate[] _animateActivate;
        private IAnimateDeactivate[] _animateDeactivate;

        private bool _initiated = false;

        private GameObject _lastSelectedBeforeDeactivation;

        private void Init()
        {
            if (_initiated)
            {
                return;
            }

            _initiated = true;
            _onFocused = GetComponent<IFocusable>();
            _animateActivate = GetComponents<IAnimateActivate>();
            _animateDeactivate = GetComponents<IAnimateDeactivate>();
        }

        void Start()
        {
            Init();
        }

        public void ActivateImmediately()
        {
            Init();
            foreach (var selectable in GetSelectables())
            {
                selectable.interactable = true;
            }

            if (_onFocused != null)
            {
                _onFocused.Focus(_lastSelectedBeforeDeactivation);
                _lastSelectedBeforeDeactivation = null;
            }
        }

        public void Activate()
        {
            Init();
            if (_animateActivate != null && _animateActivate.Length > 0)
            {
                var finished = new bool[_animateActivate.Length];
                for (var i = 0; i < _animateActivate.Length; i++)
                {
                    var i1 = i;
                    _animateActivate[i].AnimateActivation(() =>
                    {
                        finished[i1] = true;
                        if (finished.All(f => f))
                        {
                            ActivateImmediately();
                        }
                    });
                }
            }
            else
            {
                ActivateImmediately();
            }
        }

        public void DeactivateImmediately()
        {
            Init();
            _lastSelectedBeforeDeactivation = null;
            foreach (var selectable in GetSelectables())
            {
                if (selectable.gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    _lastSelectedBeforeDeactivation = selectable.gameObject;
                }
                selectable.interactable = false;
            }
        }

        public void Deactivate()
        {
            Init();
            if (_animateDeactivate != null && _animateDeactivate.Length > 0)
            {
                var finished = new bool[_animateDeactivate.Length];
                for (var i = 0; i < _animateDeactivate.Length; i++)
                {
                    var i1 = i;
                    _animateDeactivate[i].AnimateDeactivation(() =>
                    {
                        finished[i1] = true;
                        if (finished.All(f => f))
                        {
                            DeactivateImmediately();
                        }
                    });
                }
            }
            else
            {
                DeactivateImmediately();
            }
        }

        private IEnumerable<Selectable> GetSelectables()
        {
            return gameObject.GetComponentsInChildren<Selectable>();
        }
    }
}