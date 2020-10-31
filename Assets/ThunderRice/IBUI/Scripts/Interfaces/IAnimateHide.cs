using System;

namespace Assets.ThunderRice.IBUI.Scripts.Interfaces
{
    public interface IAnimateHide
    {
        void AnimateHiding(Action onAnimationFinishedCallback, string newState);
    }
}