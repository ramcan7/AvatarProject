using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Running, Attacking, Jumping }

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    public event Action<PlayerState> OnStateChanged;

    private Dictionary<PlayerState, HashSet<PlayerState>> validTransitions;

    private void Awake()
    {
        InitializeTransitions();
    }

    private void InitializeTransitions()
    {
        validTransitions = new Dictionary<PlayerState, HashSet<PlayerState>>
        {
            // Idle can go to any animation
            { PlayerState.Idle, new HashSet<PlayerState>
                { PlayerState.Running, PlayerState.Attacking }
            },
            { PlayerState.Running, new HashSet<PlayerState>
                { PlayerState.Idle, PlayerState.Attacking }
            },
            // Attacking can go only to idle
            { PlayerState.Attacking, new HashSet<PlayerState>
                { PlayerState.Idle }
            }
        };
    }

    public bool TrySetState(PlayerState newState)
    {
        if (CurrentState == newState) return true;

        if (!IsTransitionValid(CurrentState, newState))
        {
            Debug.Log($"Invalid transition: {CurrentState} -> {newState}");
            return false;
        }

        Debug.Log($"State transition: {CurrentState} -> {newState}");
        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
        return true;
    }

    private bool IsTransitionValid(PlayerState from, PlayerState to)
    {
        return validTransitions[from].Contains(to);
    }

    public bool Is(PlayerState state) => CurrentState == state;

    public bool CanTrasitionTo(PlayerState state) => IsTransitionValid(CurrentState, state); 
}
