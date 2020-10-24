using System;

namespace Assets.IBUI.Interfaces
{
    public interface IAnimateHide
    {
        void AnimateHiding(Action onAnimationFinishedCallback, string newState);
    }
}