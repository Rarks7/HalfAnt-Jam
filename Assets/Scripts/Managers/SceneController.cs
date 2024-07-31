using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] protected SceneName ThisScene;

    [SerializeField] protected List<Door_Interactable> Doors;

    [SerializeField] protected Transform DefaultSpawn;

    [SerializeField] protected List<GameObject> MagicUIElements;

    protected bool openingScene;
    
    protected virtual void Awake()
    {
        openingScene = (Instance == null);
        
        Instance = this;
    }

    protected virtual void Start()
    {
        GameManager.Instance.SetOpeningScene(ThisScene);
    }

    public Vector3 GetSpawnPoint()
    {
        SceneName LastScene = GameManager.Instance.PreviousScene;
        Debug.Log("Last Scene = " + LastScene);
        

        if (LastScene == SceneName.None)
        {
            
            return DefaultSpawn.position;
        }
            

        Door_Interactable door = Doors.Find(_x => _x.GetDestinationScene() == LastScene);

        if(door == null)
        {
            Debug.LogError("No Door Found matching Last scene of " + LastScene);
            return DefaultSpawn.position;
        }

        

        return door.GetSpawnPoint().transform.position;
    }
}
