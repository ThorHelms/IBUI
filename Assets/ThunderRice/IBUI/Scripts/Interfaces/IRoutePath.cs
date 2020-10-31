namespace Assets.ThunderRice.IBUI.Scripts.Interfaces
{
    public interface IRoutePath
    {
        bool MatchesState(string state);
        void Show(string newState, string oldState);
        void Hide(string newState);
    }
}