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



    public void Cast(ElementType _element, CombatType _combatType)
    {
        runeResetTimerIsRunning = true;
        runeResetTimer = 0;

        switch (combo)
        {
            case CastCombo.First:
                rune1.Activate();
                activeRunes.Add(rune1);
                rune1.SetRuneElementType(_element);
                rune1.SetRuneCombatType(_combatType);


                break;
            case CastCombo.Second:
                rune2.Activate();
                activeRunes.Add(rune2);
                rune2.SetRuneElementType(_element);
                rune2.SetRuneCombatType(_combatType);

                break;
            case CastCombo.Third:
                rune3.Activate();
                activeRunes.Add(rune3);
                rune3.SetRuneElementType(_element);
                rune3.SetRuneCombatType(_combatType);

                break;
            default:
                break;
        }

        combo++;

    }

    public void Summon()
    {
        if (activeRunes.Count == 0)
        { 
            return; 
        }
        ResolveActiveRunes();
        ClearActiveRunes();
    }

    public void ResolveActiveRunes()
    {

        int fireCounter = 0;
        int iceCounter = 0;
        int lightningCounter = 0;
        int meleeCounter = 0;
        int rangeCounter = 0;
        int mageCounter = 0;

        foreach (Rune rune in activeRunes) 
        {

            switch (rune.GetRuneElementType())
            {
                case ElementType.Empty:

                    break;
                case ElementType.Fire:
                    fireCounter++;

                    break;
                case ElementType.Ice:
                    iceCounter++;

                    break;
                case ElementType.Lightning:
                    lightningCounter++;

                    break;
                default:
                    break;
            }


            switch (rune.GetRuneCombatType())
            {
                case CombatType.Empty:
                    break;
                case CombatType.Fighter:
                    meleeCounter++;
                    break;
                case CombatType.Ranger:
                    rangeCounter++;
                    break;
                case CombatType.Mage:
                    mageCounter++;
                    break;
                default:
                    break;
            }

        }




        player.summonModule.CreateSummon(fireCounter, iceCounter, lightningCounter, meleeCounter, rangeCounter, mageCounter);
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
