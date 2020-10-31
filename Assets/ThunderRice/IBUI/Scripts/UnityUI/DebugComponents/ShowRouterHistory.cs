using System;
using Assets.ThunderRice.IBUI.Scripts.Interfaces;
using TMPro;
using UnityEngine;

namespace Assets.ThunderRice.IBUI.Scripts.UnityUI.DebugComponents
{
    public class ShowRouterHistory : MonoBehaviour
    {
        private IRouter Router { get; set; }
        private TMP_Text Text { get; set; }

        void Start()
        {
            Router = GetComponentInParent<IRouter>();
            Text = GetComponent<TMP_Text>();
            if (Router != null)
            {
                Router.AddStateListener(UpdateHistory);
            }
        }

        private void UpdateHistory(string newState, string oldState)
        {
            if (Text == null || Router == null)
            {
                return;
            }

            var history = Router.GetHistory();

            var historyString = String.Join("\n", history);
            Text.text = historyString;
        }

        void OnDestroy()
        {
            if (Router != null)
            {
                Router.RemoveStateListener(UpdateHistory);
            }
        }
    }
}