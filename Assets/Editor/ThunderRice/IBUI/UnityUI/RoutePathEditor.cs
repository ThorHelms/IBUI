using Assets.IBUI.UnityUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.ThunderRice.IBUI.UnityUI
{
    [CustomEditor(typeof(RoutePath))]
    public class RoutePathEditor : UnityEditor.Editor
    {
        private bool AutoRename = true;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var routePathTarget = (RoutePath) target;
            var desiredName = $"IBUI Route path: {routePathTarget.Path}";
            AutoRename = GUILayout.Toggle(AutoRename, "Auto rename");
            if (AutoRename && routePathTarget.transform.name != desiredName)
            {
                routePathTarget.transform.name = desiredName;
            }
        }   
    }
}