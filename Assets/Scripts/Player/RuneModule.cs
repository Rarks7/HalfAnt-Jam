using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CastCombo
{
    First,
    Second,
    Third


}


public class RuneModule : MonoBehaviour
{

    Player player;

    CastCombo combo = CastCombo.First;
    
    [SerializeField] Rune rune1;
    [SerializeField] Rune rune2;
    [SerializeField] Rune rune3;

    List<Rune> activeRunes;


    private float runeResetTimerLimit = 3;
    private float runeResetTimer;
    private bool runeResetTimerIsRunning;


    

    // Start is called before the first frame update
    void Start()
    {

        player = GetComponent<Player>();

        activeRunes = new List<Rune>();
    }

    // Update is called once per frame
    void Update()
    {

        RuneResetTimer();

    }



    public void Cast(RuneType _type)
    {
        runeResetTimerIsRunning = true;
        runeResetTimer = 0;

        switch (combo)
        {
            case CastCombo.First:
                rune1.Activate();
                activeRunes.Add(rune1);
                rune1.SetRuneType(_type);

                break;
            case CastCombo.Second:
                rune2.Activate();
                activeRunes.Add(rune2);
                rune2.SetRuneType(_type);

                break;
            case CastCombo.Third:
                rune3.Activate();
                activeRunes.Add(rune3);
                rune3.SetRuneType(_type);

                break;
            default:
                break;
        }

        combo++;

    }

    public void Summon()
    {
        ResolveActiveRunes();
        ClearActiveRunes();
    }

    public void ResolveActiveRunes()
    {

        int fireCounter = 0;
        int iceCounter = 0;
        int lightningCounter = 0;

        foreach (Rune rune in activeRunes) 
        {

            switch (rune.GetRuneType())
            {
                case RuneType.Empty:

                    break;
                case RuneType.Fire:
                    fireCounter++;

                    break;
                case RuneType.Ice:
                    iceCounter++;

                    break;
                case RuneType.Lightning:
                    lightningCounter++;

                    break;
                default:
                    break;
            }


        }

        player.summonModule.CreateSummon(fireCounter, iceCounter, lightningCounter);
    }

    

    public void ClearActiveRunes()
    {
        runeResetTimerIsRunning = false;
        combo = CastCombo.First;
        rune1.Deactivate();
        rune2.Deactivate();
        rune3.Deactivate();

        activeRunes.Clear();

    }

    public void RuneResetTimer()
    {

        if (runeResetTimerIsRunning)
        {
            runeResetTimer += Time.deltaTime;

            if (runeResetTimer >= runeResetTimerLimit)
            {
                ClearActiveRunes();

            }

        }

    }
}
