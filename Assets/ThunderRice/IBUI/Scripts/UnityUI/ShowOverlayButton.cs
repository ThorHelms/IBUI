using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI
{
    public class ShowOverlayButton : MonoBehaviour
    {
        public GameObject Overlay = null;
        private IOverlay _overlay = null;

        public GameObject Activatable = null;
        private IActivatable _activatable = null;

        private Button _button = null;

        void Start()
        {
            if (Overlay != null)
            {
                _overlay = Overlay.GetComponent<IOverlay>();
            }

            if (Activatable != null)
            {
                _activatable = Activatable.GetComponent<IActivatable>();
            }

            if (_activatable == null)
            {
                _activatable = GetComponentInParent<IActivatable>();
            }

            _button = GetComponent<Button>();

            if (_button != null)
            {
                _button.onClick.AddListener(InvokeAction);
            }
        }

        public void InvokeAction()
        {
            if (_overlay == null)
            {
                return;
            }

            _overlay.Show(_activatable);
        }

        [MenuItem("GameObject/UBUI/Show Overlay button", false, 10)]
        public static void CreateRouterCanvas(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("UBUI Show Overlay button");
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 30);
            go.AddComponent<CanvasRenderer>();
            var image = go.AddComponent<Image>();
            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            image.type = Image.Type.Sliced;
            go.AddComponent<Button>();
            go.AddComponent<ShowOverlayButton>();

            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

            var textObject = new GameObject("Text (TMP)");
            var textRectTransform = textObject.AddComponent<RectTransform>();
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.offsetMin = Vector2.zero;
            textRectTransform.offsetMax = Vector2.zero;
            var text = textObject.AddComponent<TextMeshProUGUI>();
            text.text = "Show Overlay button";
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