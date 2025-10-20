using System;
using UnityEngine;

public enum PlayerState { Idle, Running, Attacking }

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    public event Action<PlayerState> OnStateChanged;

    public void SetState(PlayerState newState)
    {
        if (CurrentState == newState) return;
        Debug.Log(CurrentState);
        CurrentState = newState;
        // alert about change
        OnStateChanged?.Invoke(CurrentState);
    }
    
    public bool Is(PlayerState state) => CurrentState == state;

    public bool CanTransitionTo(PlayerState state)
    {
        var canTransition = state switch
        {
            PlayerState.Idle => CurrentState == PlayerState.Running,
            PlayerState.Running => CurrentState == PlayerState.Idle,
            PlayerState.Attacking => true,
            _ => false
        };
        return canTransition;
    } 
}
