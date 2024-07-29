using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public SceneName PreviousScene;
    public SceneName CurrentScene;

    

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

        CurrentState = GameState.Overworld;

        PreviousScene = SceneName.None;
        Debug.Log("Set Previous Scene to " + PreviousScene);

        CurrentScene = SceneName.None;
        Debug.Log("Set Current Scene to " + CurrentScene);

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

    public void GoToScene(SceneName sceneName)
    {
        PreviousScene = CurrentScene;
        Debug.Log("Set Previous Scene to " + PreviousScene);

        if(sceneName == SceneName.VoidLevel)
        {
            GoToVoidLevel();
            return;
        }


        CurrentScene = sceneName;
        Debug.Log("Set Current Scene to " + CurrentScene);

        SceneManager.LoadScene(sceneName.GetSceneNameString());
    }


    private void GoToVoidLevel()
    {

    }

    public void SetOpeningScene(SceneName sceneName)
    {
        if (CurrentScene != SceneName.None)
            return;
        CurrentScene = sceneName;
        Debug.Log("Set Current Scene to " + CurrentScene);
        //PreviousScene = SceneName.None;
    }



}
