using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidSceneController : SceneController
{
    
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    [Button]
    public void RoomIsComplete()
    {
        EventManager.ActivateVoidDoor();
    }

    [Button]
    public void CombatBegin()
    {

    }
}
