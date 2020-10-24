using System;

namespace Assets.IBUI.Interfaces
{
    public interface IAnimateShow
    {
        void AnimateShowing(Action onAnimationFinishedCallback, string oldState);
    }
}