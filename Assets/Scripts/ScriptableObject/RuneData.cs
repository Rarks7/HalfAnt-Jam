using System;
using UnityEngine;


[System.Serializable]

public struct RuneDataStruct
{


    public Sprite elementSprite;
    public Sprite combatSprite;




}


[CreateAssetMenu(fileName = "NewRuneData", menuName = "Data/RuneData")]


public class RuneData : ScriptableObject
{


    [SerializeField] RuneDataStruct emptyRune;
    [SerializeField] RuneDataStruct fireRune;
    [SerializeField] RuneDataStruct iceRune;
    [SerializeField] RuneDataStruct lightningRune;





    public Sprite GetElementSprite(ElementType _type)
    {
        return _type switch
        {

            ElementType.Empty => emptyRune.elementSprite,
            ElementType.Fire => fireRune.elementSprite,
            ElementType.Ice => iceRune.elementSprite,
            ElementType.Lightning => lightningRune.elementSprite,

            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
    }

    public Sprite GetCombatSprite(CombatType _type)
    {
        return _type switch
        {

            CombatType.Empty => emptyRune.elementSprite,
            CombatType.Fighter => fireRune.combatSprite,
            CombatType.Ranger => iceRune.combatSprite,
            CombatType.Mage => lightningRune.combatSprite,

            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
    }

}
