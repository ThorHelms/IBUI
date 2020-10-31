using Assets.ThunderRice.IBUI.Scripts.UnityUI;
using UnityEditor;

namespace Assets.Editor.ThunderRice.IBUI.UnityUI
{
    [CustomEditor(typeof(ChangeRouteButton))]
    public class ChangeRouteButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var changeRouteButtonTarget = (ChangeRouteButton) target;
            var desiredState =
                changeRouteButtonTarget.DesiredRouteAction == ChangeRouteButton.RouteAction.Pop
                    ? ""
                    : changeRouteButtonTarget.DesiredState;
            var desiredName = $"IBUI Change route button: [{changeRouteButtonTarget.DesiredRouteAction}] {desiredState}";
            if (changeRouteButtonTarget.AutoRenameInEditor && changeRouteButtonTarget.transform.name != desiredName)
            {
                changeRouteButtonTarget.transform.name = desiredName;
            }
        }   
    }
}