using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckModule : MonoBehaviour
{

    [SerializeField]
    public List<Rune> runeHand;

    [SerializeField]

    public List<Rune> runeDeck;

    [SerializeField]
    public List<Rune> activeRunes;



    [SerializeField]
    DeckData deckdata;

    [SerializeField]
    DeckData starterDeckdata;



    int runeHandSize = 5;

    int runeDeckSize = 20;


    private void Awake()
    {
        runeDeck = new List<Rune>();
        runeHand = new List<Rune>();
        activeRunes = new List<Rune>();
    }
    // Start is called before the first frame update
    void Start()
    {
        FillDeck();
        FillHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillDeck()
    {

        foreach (var rune in deckdata.playerDeck) 
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

        RuneDeckUI.Instance.FillRuneDeckUI(runeHand);
    }

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


    public void RandomFillRuneHand()
    {

        for (int i = 0; i < runeHandSize; i++)
        {

            runeHand.Add(runeDeck[i]);
            
            runeDeck.RemoveAt(i);



        }
        if(RuneDeckUI.Instance == null) { return; }

        RuneDeckUI.Instance.FillRuneDeckUI(runeHand);

    }

    public void Cast(List<RuneHolder> _runeHolderHand)
    {

        Deal();

        foreach (var rune in _runeHolderHand)
        {

            if (!rune.selected)
            {


                AddToDeck(rune.rune);


            }

        }
        
    }

    public void AddToDeck(Rune _rune) 
    {
        if (_rune.GetRuneElementType() != ElementType.Empty)
        {
            runeDeck.Add(_rune);

        }

    }


    public void Deal()
    {
        int trueHandSize = 0;

        runeHand.Clear();


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
                emptyRune.SetRuneElementType(0);
                emptyRune.SetRuneCombatType(0);


                runeHand.Add(emptyRune);

            }


        }

        runeDeck.RemoveRange(0, trueHandSize);

        RuneDeckUI.Instance.FillRuneDeckUI(runeHand);

    }

}
