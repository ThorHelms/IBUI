namespace Assets.IBUI.Interfaces
{
    public interface IActivatable
    {
        void ActivateImmediately();
        void Activate();
        void DeactivateImmediately();
        void Deactivate();
    }
}