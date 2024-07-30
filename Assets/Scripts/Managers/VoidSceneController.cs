using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidSceneController : SceneController
{
    public static int RoomCount = 0;
    
    protected override void Awake()
    {
        base.Awake();

        EventManager.OnCombatCompleted += RoomIsComplete;
        EventManager.OnPlayerCharacterDied += PlayerDied;
        RoomCount++;
    }

    protected override void Start()
    {
        base.Start();
        CombatBegin();
    }

    private void OnDestroy()
    {
        EventManager.OnCombatCompleted -= RoomIsComplete;
        EventManager.OnPlayerCharacterDied -= PlayerDied;
    }

    [Button]
    public void RoomIsComplete()
    {
        EventManager.ActivateVoidDoor();
    }

    [Button]
    public void CombatBegin()
    {
        EventManager.StartCombat(RoomCount);
    }

    private void PlayerDied()
    {
        RoomCount = 0;
        GameManager.Instance.GoToScene(Constants.SceneName.PlayersRoom);
    }
}
