using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuneDeckModule : MonoBehaviour
{

    Player player;

    [SerializeField]
    public List<Rune> runeDeck;

    [SerializeField]
    public List<Rune> runeHand;

    [SerializeField]
    public List<Rune> activeRunes;

    [SerializeField]
    public List<Rune> summonedRunes;


    int runeHandSize = 5;

    int runeDeckSize = 20;

    CastCombo combo = CastCombo.First;

    [SerializeField] RuneHolder rune1;
    [SerializeField] RuneHolder rune2;
    [SerializeField] RuneHolder rune3;

    private float runeResetTimerLimit = 3;
    private float runeResetTimer;
    private bool runeResetTimerIsRunning;

    RuneDeckUI runeHandUI;


    private void Awake()
    {
        runeDeck = new List<Rune>();
        runeHand = new List<Rune>();
        activeRunes = new List<Rune>();
        summonedRunes = new List<Rune>();

    }


    void Start()
    {

        player = GetComponent<Player>();

        activeRunes = new List<Rune>();

        runeHandUI = FindAnyObjectByType<RuneDeckUI>();

        FillDeck();
        FillHand();

    }


    void Update()
    {

        RuneResetTimer();

    }



    public void Cast(Rune _rune, int _handPosition)
    {
        if (!runeHandUI.runeHandUI[_handPosition].selected && activeRunes.Count < 3)
        {
            runeResetTimerIsRunning = true;
            runeResetTimer = 0;
            AudioManager.instance.Play("CastRune");

            switch (combo)
            {
                case CastCombo.First:


                    rune1.IngestRune(_rune);
                    activeRunes.Add(_rune);


                    break;
                case CastCombo.Second:
                    rune2.IngestRune(_rune);
                    activeRunes.Add(_rune);


                    break;
                case CastCombo.Third:
                    rune3.IngestRune(_rune);
                    activeRunes.Add(_rune);


                    break;
                default:
                    break;
            }

            combo++;
            runeHandUI.runeHandUI[_handPosition].Select();

        }
        else
        {
            runeHandUI.runeHandUI[_handPosition].StartCantSelect();


        }

        

    }

    public void Summon()
    {

        foreach (var rune in activeRunes)
        {
            if (rune.GetRuneElementType() == ElementType.Empty)
            {
                return;
            }
        }

        if (activeRunes.Count > 0)  
        {

            ReturnHandToDeck(runeHandUI.runeHandUI);

            if (activeRunes.Count == 0)
            {
                return;
            }
            AudioManager.instance.Play("Summon");
            ResolveActiveRunes();
            ClearActiveRunes();
        }
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

            summonedRunes.Add(rune);

        }




        player.summonModule.CreateSummon(activeRunes, fireCounter, iceCounter, lightningCounter, meleeCounter, rangeCounter, mageCounter);
    }



    public void ClearActiveRunes()
    {
        runeResetTimerIsRunning = false;
        combo = CastCombo.First;
        rune1.Deactivate();
        rune2.Deactivate();
        rune3.Deactivate();

        activeRunes.Clear();
        for (int i = 0; i < runeHandUI.runeHandUI.Count; i++)
        {
            runeHandUI.runeHandUI[i].Deselect();

        }

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


    void FillDeck()
    {

        foreach (var rune in GameManager.Instance.deckdata.playerDeck) 
        { 
        
            AddToDeck(rune);
            runeDeck = runeDeck.OrderBy(_y => Guid.NewGuid()).ToList();
        }

    }

    void FillHand()
    {
        for (int i = 0; i < runeHandSize; i++)
        {
            runeHand.Add(runeDeck[i]);

            runeDeck.RemoveAt(i);
        }

        runeHandUI.FillRuneDeckUI(runeHand);
    }

 

    public void ReturnHandToDeck(List<RuneHolder> _runeHolderHand)
    {
        foreach (var runeHolder in _runeHolderHand)
        {

            if (!runeHolder.selected)
            {

                AddToDeck(runeHolder.rune);

            }

            runeHolder.Clear();
        }

        runeHand.Clear();
        
        
        DealPlayerHand();

    }

    public void AddToDeck(Rune _rune) 
    {
        if (_rune.GetRuneElementType() != ElementType.Empty)
        {
            runeDeck.Add(_rune);

        }

    }


    public void DealPlayerHand()
    {
        int trueHandSize = 0;

        for (int i = 0; i < runeHandSize; i++)
        {
            if (runeDeck.Count > i)
            {
                trueHandSize++;

                runeHand.Add(runeDeck[i]);
            }
            else
            {

                Rune emptyRune = new Rune();
                emptyRune.SetRuneElementType(ElementType.Empty);
                emptyRune.SetRuneCombatType(CombatType.Empty);

                runeHand.Add(emptyRune);

            }
        }

        runeDeck.RemoveRange(0, trueHandSize);

        runeHandUI.FillRuneDeckUI(runeHand);

    }


    //Testing Function
    public void RandomFillRuneDeck()
    {

        for (int i = 0; i < runeDeckSize; i++)
        {


            Rune newRune = new Rune();

            int randomElementRoll = UnityEngine.Random.Range(1, 4);
            ElementType elementType = (ElementType)randomElementRoll;


            int randomCombatRoll = UnityEngine.Random.Range(1, 4);
            CombatType combatType = (CombatType)randomCombatRoll;


            newRune.SetRuneElementType(elementType);
            newRune.SetRuneCombatType(combatType);

            runeDeck.Add(newRune);


        }

    }

    //Testing Function
    public void RandomFillRuneHand()
    {

        for (int i = 0; i < runeHandSize; i++)
        {

            runeHand.Add(runeDeck[i]);

            runeDeck.RemoveAt(i);



        }
        if (runeHandUI == null) { return; }

        runeHandUI.FillRuneDeckUI(runeHand);

    }
}
