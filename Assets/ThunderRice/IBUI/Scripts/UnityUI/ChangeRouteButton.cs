using System;
using Assets.IBUI.Interfaces;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.IBUI.UnityUI
{
    public class ChangeRouteButton : MonoBehaviour
    {
        public string DesiredState = String.Empty;

        public GameObject Router = null;

        public enum RouteAction
        {
            Push,
            Change,
            Set,
            Pop,
        }

        public RouteAction DesiredRouteAction = RouteAction.Set;

        private Button _button = null;

        private IRouter _router = null;

        void Start()
        {
            _button = GetComponent<Button>();
            if (_button != null)
            {
                _button.onClick.AddListener(InvokeRouteChange);
            }

            if (Router != null)
            {
                _router = Router.GetComponent<IRouter>();
            }

            if (_router == null)
            {
                _router = GetComponentInParent<IRouter>();
            }
        }

        public void InvokeRouteChange()
        {
            if (_router == null)
            {
                return;
            }

            switch (DesiredRouteAction)
            {
                case RouteAction.Push:
                    _router.PushState(DesiredState);
                    break;
                case RouteAction.Change:
                    _router.ChangeState(DesiredState);
                    break;
                case RouteAction.Set:
                    _router.SetState(DesiredState);
                    break;
                case RouteAction.Pop:
                    _router.PopState();
                    break;
            }
        }

        [MenuItem("GameObject/UBUI/Change Route button", false, 10)]
        public static void CreateRouterCanvas(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("UBUI Change Route button");
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30);
            go.AddComponent<CanvasRenderer>();
            var image = go.AddComponent<Image>();
            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            image.type = Image.Type.Sliced;
            go.AddComponent<Button>();
            go.AddComponent<ChangeRouteButton>();

            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

            var textObject = new GameObject("Text (TMP)");
            var textRectTransform = textObject.AddComponent<RectTransform>();
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.offsetMin = Vector2.zero;
            textRectTransform.offsetMax = Vector2.zero;
            var text = textObject.AddComponent<TextMeshProUGUI>();
            text.text = "Change Route button";
            text.fontSize = 24;
            text.color = Color.black;
            text.alignment = TextAlignmentOptions.Center;
            GameObjectUtility.SetParentAndAlign(textObject, go);

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}