using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using System;

public class VoidManager : MonoBehaviour
{
    public static VoidManager Instance;
    
    [SerializeField] private int NumberOfLevelsInARun;

    [SerializeField] private List<SceneName> VoidLevelsFirstRun;
    [SerializeField] private List<SceneName> AllVoidLevels;

    private const SceneName IntermediateLevel = SceneName.VoidHallway;

    private List<SceneName> VoidLevelsActiveRun;

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

        EventManager.OnPlayerCharacterDied += RunEnds;
    }

    private void Start()
    {
        VoidLevelsActiveRun = new List<SceneName>();
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerCharacterDied -= RunEnds;
    }

    private void RunEnds()
    {
        VoidLevelsActiveRun.Clear();
        GameManager.Instance.Flag_FirstRunCompleted = true;
    }

    private void GenerateRandomRun()
    {
        VoidLevelsActiveRun = new List<SceneName>();

        for (int i = 0; i < NumberOfLevelsInARun; i++)
        {
            int index = UnityEngine.Random.Range(0, AllVoidLevels.Count);
            VoidLevelsActiveRun.Add(AllVoidLevels[index]);
            VoidLevelsActiveRun.Add(IntermediateLevel);
        }
    }

    public SceneName GetVoidLevel()
    {
        if(GameManager.Instance.Flag_FirstRunCompleted)
        {
            if(VoidLevelsActiveRun.Count <= 0)
            {
                GenerateRandomRun();
            }
        }
        else
        {
            if(VoidLevelsActiveRun.Count <= 0)
            {
                VoidLevelsActiveRun = new List<SceneName>(VoidLevelsFirstRun);
            }
        }

        SceneName Level = VoidLevelsActiveRun[0];
        VoidLevelsActiveRun.RemoveAt(0);

        return Level;
    }

    


}
