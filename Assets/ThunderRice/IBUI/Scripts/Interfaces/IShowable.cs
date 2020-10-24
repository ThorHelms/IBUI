using System;

namespace Assets.IBUI.Interfaces
{
    public interface IShowable
    {
        void ShowImmediately();
        void Show(string oldState, Action shownCallback = null);
        void HideImmediately();
        void Hide(string newState, Action hiddenCallback = null);
    }
}