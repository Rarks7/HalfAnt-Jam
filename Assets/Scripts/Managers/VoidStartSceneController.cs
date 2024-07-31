using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidStartSceneController : SceneController
{
    [SerializeField] List<Animator> animators;
    [SerializeField] List<GameObject> OtherThingsToTurnOn;

    private const string ForceStateString = "ForceFinalState";
    
    protected override void Start()
    {
        base.Start();
        CheckIfVisited();
    }

    protected void CheckIfVisited()
    {
        if(!GameManager.Instance.Flag_VoidRoomActivated)
        {
            return;
        }
        Debug.Log("Void Room Has been activated before ");
        
        foreach (var item in animators)
        {
            item.SetTrigger(ForceStateString);
        }

        foreach (var item in OtherThingsToTurnOn)
        {
            item.SetActive(true);
        }

    }
}
