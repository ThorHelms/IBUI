using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI
{
    public class DoNotSelect : MonoBehaviour, ISelectHandler
    {
        private bool _shouldDeselect;

        void Update()
        {
            if (_shouldDeselect && EventSystem.current.currentSelectedGameObject == gameObject)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            _shouldDeselect = false;
        }

        public void OnSelect(BaseEventData eventData)
        {
            _shouldDeselect = true;
        }
    }
}