using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using System;

public class VoidManager : MonoBehaviour
{
    public static VoidManager Instance;
    
    
    [NonSerialized] public bool HasEnteredTheVoidBefore;
    private const string enteredTheVoidSaveString = "entered_the_void";

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
    }

    private void Start()
    {
        HasEnteredTheVoidBefore = GameManager.Instance.GetSavedBool(enteredTheVoidSaveString);
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
        if(HasEnteredTheVoidBefore)
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
                HasEnteredTheVoidBefore = true;
                GameManager.Instance.SaveBool(enteredTheVoidSaveString, true);
            }
        }

        SceneName Level = VoidLevelsActiveRun[0];
        VoidLevelsActiveRun.RemoveAt(0);

        return Level;
    }

    


}
