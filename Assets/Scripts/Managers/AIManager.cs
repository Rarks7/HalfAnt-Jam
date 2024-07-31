using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }


    public List<Enemy> activeEnemies = new List<Enemy>();
    public List<Summon> activeSummons = new List<Summon>();



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        EventManager.OnCombatCompleted += KillAllSlimes;
        EventManager.OnPlayerCharacterDied += KillAllSlimes;
    }
    private void OnDestroy()
    {
        EventManager.OnCombatCompleted -= KillAllSlimes;
        EventManager.OnPlayerCharacterDied -= KillAllSlimes;
    }
    private void KillAllSlimes()
    {
        foreach (var item in activeSummons)
        {
            if(item != null)
                item.Die();
        }

        activeSummons.Clear();
    }
}
