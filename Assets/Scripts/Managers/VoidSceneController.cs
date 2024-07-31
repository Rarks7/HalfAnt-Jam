using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidSceneController : SceneController
{
    public static int RoomCount = 0;

    public GameObject GameOverScreen;

    private Coroutine EndgameCoro;

    public bool IsTutorialScene;
    
    protected override void Awake()
    {
        base.Awake();

        if(IsTutorialScene)
        {
            EventManager.OnLaunchTutorialCombat += CombatBegin;
        }

        EventManager.OnCombatCompleted += RoomIsComplete;
        EventManager.OnPlayerCharacterDied += PlayerDied;
        RoomCount++;
    }

    protected override void Start()
    {
        base.Start();

        if(!IsTutorialScene)
        {
            CombatBegin();
        }
        
    }

    private void OnDestroy()
    {
        EventManager.OnCombatCompleted -= RoomIsComplete;
        EventManager.OnPlayerCharacterDied -= PlayerDied;
        EventManager.OnLaunchTutorialCombat -= CombatBegin;
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
        EndgameCoro = StartCoroutine(PlayerDeathSequence());
    }

    

    private IEnumerator PlayerDeathSequence()
    {
        GameOverScreen.SetActive(true);

        EventManager.OnPlayerPressedInteract += SkipDeathScreen;

        yield return new WaitForSeconds(5.0f);
        EventManager.OnPlayerPressedInteract -= SkipDeathScreen;
        GameManager.Instance.GoToScene(Constants.SceneName.PlayersRoom);
        EndgameCoro = null;
    }

    private void SkipDeathScreen()
    {
        StopCoroutine(EndgameCoro);
        GameOverScreen.SetActive(false);
        EventManager.OnPlayerPressedInteract -= SkipDeathScreen;
        GameManager.Instance.GoToScene(Constants.SceneName.PlayersRoom);

    }
}
