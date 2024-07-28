using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckModule : MonoBehaviour
{


    public List<Rune> runeHand;

    public List<Rune> runeDeck;

    public List<Rune> activeRunes;


    int runeHandSize = 5;

    int runeDeckSize = 20;


    private void Awake()
    {
        runeDeck = new List<Rune>();
        runeHand = new List<Rune>();
        activeRunes = new List<Rune>();


        RandomFillRuneDeck();
        RandomFillRuneHand();


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        RuneDeckUI.Instance.FillRuneDeckUI(runeHand);

    }

    public void CastRunes()
    {





    }


}
