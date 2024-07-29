using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using System;

public class VoidManager : MonoBehaviour
{
    [NonSerialized] public bool HasEnteredTheVoidBefore;

    [SerializeField] private int NumberOfLevelsInARun;

    [SerializeField] private List<SceneName> VoidLevelsFirstRun;
    [SerializeField] private List<SceneName> AllVoidLevels;

    private const SceneName IntermediateLevel = SceneName.VoidHallway;

    private List<SceneName> VoidLevelsRandomRun;
    


    private void GenerateRandomRun()
    {
        VoidLevelsRandomRun = new List<SceneName>();

        for (int i = 0; i < NumberOfLevelsInARun; i++)
        {
            int index = UnityEngine.Random.Range(0, AllVoidLevels.Count);
            VoidLevelsRandomRun.Add(AllVoidLevels[index]);
            VoidLevelsRandomRun.Add(IntermediateLevel);
        }
    }

    public SceneName GetVoidLevel()
    {
        if(HasEnteredTheVoidBefore)
        {
            if(VoidLevelsRandomRun.Count <= 0)
            {
                GenerateRandomRun();
            }

            SceneName Level = VoidLevelsRandomRun[0];
            VoidLevelsRandomRun.RemoveAt(0);

            return Level;
        }
        else
        {
            SceneName Level = VoidLevelsFirstRun[0];
            VoidLevelsFirstRun.RemoveAt(0);
            return Level;
        }
    }


}
