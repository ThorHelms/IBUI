using System;
using System.Collections.Generic;
using System.Linq;
using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI
{
    public class Router : MonoBehaviour, IRouter
    {
        public string StartingState = String.Empty;

        public bool IsSwitch = true;

        private List<string> _stateHistory = new List<string>();

        private event Action<string, string> StateListeners;

        private IRoutePath[] _routePaths = new IRoutePath[0];

        void Start()
        {
            UpdatePaths();
            foreach (var routePath in _routePaths)
            {
                var mb = routePath as MonoBehaviour;
                if (mb == null)
                {
                    continue;
                }
                mb.gameObject.SetActive(false);
            }
            SetState(StartingState);
        }

        private void ActivateState(string newState, string oldState)
        {
            var foundRoute = false;
            foreach (var path in _routePaths)
            {
                if ((!IsSwitch || !foundRoute) && path.MatchesState(newState))
                {
                    foundRoute = true;
                    path.Show(newState, oldState);
                }
                else if (path.MatchesState(oldState))
                {
                    path.Hide(newState);
                }
            }

            StateListeners?.Invoke(newState, oldState);
        }

        public void SetState(string newState)
        {
            var oldState = GetState();
            _stateHistory.Clear();
            _stateHistory.Add(newState);
            ActivateState(newState, oldState);
        }

        public string GetState()
        {
            return _stateHistory.LastOrDefault();
        }

        public IEnumerable<string> GetHistory()
        {
            return _stateHistory;
        }

        public void PopState()
        {
            if (_stateHistory.Count == 0)
            {
                return;
            }

            var oldState = GetState();
            _stateHistory.RemoveAt(_stateHistory.Count-1);
            var newState = GetState();
            ActivateState(newState, oldState);
        }

        public void PushState(string newState)
        {
            var oldState = GetState();
            _stateHistory.Add(newState);
            ActivateState(newState, oldState);
        }

        public void ChangeState(string newState)
        {
            if (_stateHistory.Count == 0)
            {
                PushState(newState);
                return;
            }
            var oldState = GetState();
            _stateHistory.RemoveAt(_stateHistory.Count-1);
            _stateHistory.Add(newState);
            ActivateState(newState, oldState);
        }

        public void AddStateListener(Action<string, string> listener)
        {
            StateListeners += listener;
        }

        public void RemoveStateListener(Action<string, string> listener)
        {
            StateListeners -= listener;
        }

        public void UpdatePaths()
        {
            _routePaths = GetComponentsInChildren<IRoutePath>(true);
        }

        [MenuItem("GameObject/IBUI/Router/Router (canvas)", false, 10)]
        public static void CreateRouterCanvas(MenuCommand menuCommand)
        {
            // Create a custom game object
            var go = new GameObject("IBUI Router (canvas)");
            go.AddComponent<RectTransform>();
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            go.AddComponent<Router>();
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}