using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Interactable : Interactable
{
    [SerializeField] private Constants.SceneName DestinationScene;

    public override void Interact()
    {
        if(DestinationScene == Constants.SceneName.None)
        {
            Debug.LogWarning("Door_Interactable destination is set to None");
            return;
        }
        
        GameManager.Instance.GoToScene(DestinationScene);
    }
}
