using System;
using UnityEngine;


[System.Serializable]

public struct RuneDataStruct
{


    public Sprite sprite;



}


[CreateAssetMenu(fileName = "NewRuneData", menuName = "Data/RuneData")]


public class RuneData : ScriptableObject
{


    [SerializeField] RuneDataStruct emptyRune;
    [SerializeField] RuneDataStruct fireRune;
    [SerializeField] RuneDataStruct iceRune;
    [SerializeField] RuneDataStruct lightningRune;


    public Sprite GetSprite(RuneType _type)
    {
        return _type switch
        {

            RuneType.Empty => emptyRune.sprite,
            RuneType.Fire => fireRune.sprite,
            RuneType.Ice => iceRune.sprite,
            RuneType.Lightning => lightningRune.sprite,

            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
    }




}
