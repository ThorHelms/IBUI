using System;

namespace Assets.ThunderRice.IBUI.Scripts.Interfaces
{
    public interface IAnimateShow
    {
        void AnimateShowing(Action onAnimationFinishedCallback, string oldState);
    }
}