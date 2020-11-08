using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using Assets.ThunderRice.IBUI.Scripts.UnityUI.DOTweenAnimations;
using UnityEditor;
using UnityEngine;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI
{
    public class Overlay : MonoBehaviour, IOverlay
    {
        private IActivatable _activatable;
        private IShowable _showable;
        private IFocusable _focusable;

        private bool _initiated = false;

        private void Init()
        {
            if (_initiated)
            {
                return;
            }

            _initiated = true;
            _showable = GetComponent<IShowable>();
            _focusable = GetComponent<IFocusable>();
        }

        void Start()
        {
            Init();
        }

        public void Show(IActivatable activatable)
        {
            Init();
            _activatable = activatable;
            _activatable?.Deactivate();
            if (_showable != null)
            {
                _showable.Show(null);
            }
            else
            {
                gameObject.SetActive(true);
                if (_focusable != null)
                {
                    _focusable.Focus();
                }
            }
        }

        public void Hide()
        {
            Init();
            if (_showable != null)
            {
                _showable.Hide(null, () =>
                {
                    _activatable?.Activate();
                    _activatable = null;
                });
            }
            else
            {
                _activatable?.Activate();
                _activatable = null;
                gameObject.SetActive(false);
            }
        }

        [MenuItem("GameObject/IBUI/Overlay/Overlay", false, 110)]
        public static void CreateRouterCanvas(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("IBUI Overlay");
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            go.AddComponent<Overlay>();
            go.AddComponent<Activatable>();
            go.AddComponent<Showable>();
            go.AddComponent<FocusFirstWithPriority>();
            go.AddComponent<SlideInOut>();
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }

    }
}