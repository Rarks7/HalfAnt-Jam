using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Overworld,
    Combat,
    Chatting,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState {  get; private set; }
    private GameState LastState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        EventManager.OnChangeGameState += StateChange;
        EventManager.OnRevertToPreviousGameState += RevertState;
    }

    private void OnDestroy()
    {
        EventManager.OnChangeGameState -= StateChange;
        EventManager.OnRevertToPreviousGameState -= RevertState;
    }

    private void StateChange(GameState newState)
    {
        LastState = CurrentState;
        CurrentState = newState;
    }

    private void RevertState()
    {
        StateChange(GameState.Overworld);
    }


}
