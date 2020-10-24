using UnityEngine;

namespace Assets.IBUI.Interfaces
{
    public interface IFocusable
    {
        void Focus(GameObject focusGameObject = null);
    }
}