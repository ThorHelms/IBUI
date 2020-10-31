using System;

namespace Assets.ThunderRice.IBUI.Scripts.Interfaces
{
    public interface IAnimateDeactivate
    {
        void AnimateDeactivation(Action onAnimationFinishedCallback);
    }
}