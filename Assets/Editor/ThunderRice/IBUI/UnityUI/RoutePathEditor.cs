using Assets.ThunderRice.IBUI.Scripts.UnityUI;
using UnityEditor;

namespace Assets.Editor.ThunderRice.IBUI.UnityUI
{
    [CustomEditor(typeof(RoutePath))]
    public class RoutePathEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var routePathTarget = (RoutePath) target;
            var desiredName = $"IBUI Route path: {routePathTarget.Path}";
            if (routePathTarget.AutoRenameInEditor && routePathTarget.transform.name != desiredName)
            {
                routePathTarget.transform.name = desiredName;
            }
        }   
    }
}