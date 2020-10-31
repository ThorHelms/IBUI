using System;

namespace Assets.ThunderRice.IBUI.Scripts.Interfaces
{
    public interface IShowable
    {
        void ShowImmediately();
        void Show(string oldState, Action shownCallback = null);
        void HideImmediately();
        void Hide(string newState, Action hiddenCallback = null);
    }
}