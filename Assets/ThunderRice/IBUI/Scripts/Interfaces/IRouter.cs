using System;
using System.Collections.Generic;

namespace Assets.IBUI.Interfaces
{
    public interface IRouter
    {
        /// <summary>
        /// Set the current state to be exactly the given state, while clearing the history.
        /// </summary>
        /// <param name="newState">The new state.</param>
        void SetState(string newState);

        /// <summary>
        /// Get the current state, i.e. the last element of the history.
        /// </summary>
        /// <returns>The current state.</returns>
        string GetState();

        /// <summary>
        /// Get the complete state history.
        /// </summary>
        /// <returns>List of states, first being the oldest and last being the current state.</returns>
        IEnumerable<string> GetHistory();

        /// <summary>
        /// Remove the top-most state of the history, essentially going back in the state history.
        /// </summary>
        void PopState();

        /// <summary>
        /// Add a new state on top of the history, and change to that state.
        /// </summary>
        /// <param name="newState"></param>
        void PushState(string newState);

        /// <summary>
        /// Change the top-most state, keeping the history the same length.
        /// </summary>
        /// <param name="newState"></param>
        void ChangeState(string newState);

        /// <summary>
        /// Add a listener, that will be invoked whenever the state changes.
        /// The first parameter of the listener is the new state, and the second parameter is the old state.
        /// </summary>
        /// <param name="listener"></param>
        void AddStateListener(Action<string, string> listener);

        /// <summary>
        /// Remove a lister. See @AddStateListener.
        /// </summary>
        /// <param name="listener"></param>
        void RemoveStateListener(Action<string, string> listener);

        /// <summary>
        /// Force-refresh the paths underneath the router.
        /// </summary>
        void UpdatePaths();
    }
}