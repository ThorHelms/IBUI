using System;

namespace Assets.IBUI.Interfaces
{
    public interface IAnimateActivate
    {
        void AnimateActivation(Action onAnimationFinishedCallback);
    }
}