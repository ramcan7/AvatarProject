using UnityEngine;

public enum PlayerState { Idle, Moving, Attacking }

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState) return;
        Debug.Log(CurrentState);
        CurrentState = newState;
    }
}
