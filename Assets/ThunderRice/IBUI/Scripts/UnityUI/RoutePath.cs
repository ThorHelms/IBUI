using System;
using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using Assets.ThunderRice.IBUI.Scripts.UnityUI.DOTweenAnimations;
using UnityEditor;
using UnityEngine;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI
{
    public class RoutePath : MonoBehaviour, IRoutePath
    {
        public string Path = String.Empty;

        public bool AutoRenameInEditor = true;

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

        public bool MatchesState(string state)
        {
            if (Path.EndsWith("*"))
            {
                return state != null && state.StartsWith(Path.Substring(0, Path.Length - 1));
            }
            return state == Path;
        }

        public void Show(string newState, string oldState)
        {
            Init();
            if (_showable != null)
            {
                _showable.Show(oldState);
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

        public void Hide(string newState)
        {
            Init();
            if (_showable != null)
            {
                _showable.Hide(newState);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        [MenuItem("GameObject/UBUI/Route path", false, 10)]
        public static void CreateRouterCanvas(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("UBUI Route Path");
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            go.AddComponent<RoutePath>();
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