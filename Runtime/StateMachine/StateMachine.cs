using Rossoforge.Utils.Logger;
using System;
using UnityEngine;

namespace Rossoforge.Utils.StateMachine
{
    public abstract class StateMachine<T> where T : IState
    {
        public T PreviousState { get; private set; }
        public T CurrentState { get; private set; }
        public bool IsTransitionInProgress { get; private set; }

        public void StartMachine(T state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            if (CurrentState != null)
            {
                RossoLogger.Warning("StateMachine already started.");
                return;
            }

            IsTransitionInProgress = false;
            PreviousState = default;
            CurrentState = state;
            state.Enter();
        }
        public virtual async Awaitable<bool> TransitionTo(T nextState)
        {
            if (nextState == null)
                return false;

            if (CurrentState == null)
            {
                RossoLogger.Warning("StateMachine has not been started.");
                return false;
            }

            if (IsTransitionInProgress)
            {
                if (nextState.Equals(CurrentState))
                    return false;

                while (IsTransitionInProgress)
                    await Awaitable.NextFrameAsync();
            }

            IsTransitionInProgress = true;
            CurrentState.Exit();
            PreviousState = CurrentState;
            CurrentState = nextState;
            await Awaitable.NextFrameAsync();

            CurrentState.Enter();
            IsTransitionInProgress = false;

            return true;
        }
        public async Awaitable<bool> TransitionToPreviousState()
        {
            if (PreviousState == null)
            {
                RossoLogger.Verbose("No previous state to transition to.");
                return false;
            }
            return await TransitionTo(PreviousState);
        }

        public Type GetPreviousStateType()
        {
            return PreviousState?.GetType();
        }

        public void Update()
        {
            if (!IsTransitionInProgress)
                CurrentState?.Update();
        }
    }
}