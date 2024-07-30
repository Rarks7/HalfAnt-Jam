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


    public MasterStatData masterStatData;
    

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

        CurrentScene = sceneName;
        Debug.Log("Set Current Scene to " + CurrentScene);

        if (sceneName == SceneName.VoidLevel)
        {
            GoToVoidLevel();
            return;
        }

        SceneManager.LoadScene(sceneName.GetSceneNameString());
    }


    private void GoToVoidLevel()
    {
        SceneManager.LoadScene(VoidManager.Instance.GetVoidLevel().GetSceneNameString());
    }

    public void SetOpeningScene(SceneName sceneName)
    {
        if (CurrentScene != SceneName.None)
            return;
        CurrentScene = sceneName;
        Debug.Log("Set Current Scene to " + CurrentScene);
        //PreviousScene = SceneName.None;
    }

    public void SaveBool(string _saveKey, bool _bool)
    {
        PlayerPrefs.SetInt(_saveKey, _bool ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetSavedBool(string _saveKey)
    {
        if(!PlayerPrefs.HasKey(_saveKey))
        {
            PlayerPrefs.SetInt(_saveKey, 0);
            return false;
        }
        
        int savedBool = PlayerPrefs.GetInt(_saveKey);

        return savedBool != 0;
    }

    public void SaveFloat(string _saveKey, float _float)
    {
        PlayerPrefs.SetFloat(_saveKey, _float);
    }

    public float GetSavedFloat(string _saveKey)
    {
        return PlayerPrefs.GetFloat(_saveKey);
    }

    public void SaveString(string _saveKey, string _string)
    {
        PlayerPrefs.SetString(_saveKey, _string);
    }

    public string GetSavedString(string _saveKey)
    {
        return PlayerPrefs.GetString(_saveKey);
    }

}
