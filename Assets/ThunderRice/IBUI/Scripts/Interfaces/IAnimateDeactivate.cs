using System;

namespace Assets.IBUI.Interfaces
{
    public interface IAnimateDeactivate
    {
        void AnimateDeactivation(Action onAnimationFinishedCallback);
    }
}