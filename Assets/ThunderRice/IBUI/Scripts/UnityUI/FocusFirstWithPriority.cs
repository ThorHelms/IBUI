using System.Linq;
using Assets.IBUI.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.IBUI.UnityUI
{
    public class FocusFirstWithPriority : MonoBehaviour, IFocusable
    {
        public GameObject[] Priority = new GameObject[0];

        public void Focus(GameObject focusGameObject = null)
        {
            var selectable = GetFirstSelectable(focusGameObject);
            if (selectable != null)
            {
                EventSystem.current.SetSelectedGameObject(null); // Hack to force selected-state to show up in the UI
                EventSystem.current.SetSelectedGameObject(selectable);
            }
        }

        private GameObject GetFirstSelectable(GameObject focusGameObject = null)
        {
            if (focusGameObject != null)
            {
                var selectable = focusGameObject.GetComponent<Selectable>();
                if (selectable != null && selectable.interactable)
                {
                    return focusGameObject;
                }
            }
            foreach (var prioritized in Priority)
            {
                var selectable = GetFirstSelectableFromTarget(prioritized);
                if (selectable != null)
                {
                    return selectable;
                }
            }

            return GetFirstSelectableFromTarget(gameObject);
        }

        private GameObject GetFirstSelectableFromTarget(GameObject target)
        {
            var selectables = target.GetComponentsInChildren<Selectable>();
            return selectables.FirstOrDefault(selectable => selectable.interactable)?.gameObject;
        }
    }
}