using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Interactable : Interactable
{
    [SerializeField] private Constants.SceneName DestinationScene;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private bool Locked;

    public override void Interact()
    {

        Debug.Log("Door Interact Called");

        if (Locked)
            return;

        if(DestinationScene == Constants.SceneName.None)
        {
            Debug.LogWarning("Door_Interactable destination is set to None");
            return;
        }
        
        GameManager.Instance.GoToScene(DestinationScene);
    }

    public Constants.SceneName GetDestinationScene()
    {
        return DestinationScene;
    }

    public GameObject GetSpawnPoint()
    {

        return spawnPoint;
    }
}
