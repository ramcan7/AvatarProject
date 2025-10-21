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
        Debug.Log(newState);
        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }
    
    public bool Is(PlayerState state) => CurrentState == state;
}
