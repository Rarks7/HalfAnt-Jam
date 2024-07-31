using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDeckData", menuName = "Data/DeckData")]

public class DeckData : ScriptableObject
{

    [SerializeField] public List<Rune> playerDeck;

}
