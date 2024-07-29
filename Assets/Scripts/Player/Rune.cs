using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;


public enum ElementType
{
    Empty,
    Fire,
    Ice,
    Lightning,
    Earth,
    Steel,
    Crystal,
    Shadow

}

public enum RuneSpecial
{
    Empty,
    Bigger,
    Split,
    Rapid,



}

[Serializable]
public class Rune
{

    ElementType runeElementType = ElementType.Empty;
    CombatType runeCombatType = CombatType.Empty;
    RuneSpecial runeSpecial = RuneSpecial.Empty;

    





    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetRuneElementType(ElementType _type)
    {

        runeElementType = _type;



    }

    public void SetRuneCombatType(CombatType _type)
    {

        runeCombatType = _type;


    }

    public ElementType GetRuneElementType()
    {

        return runeElementType;

    }

    public CombatType GetRuneCombatType()
    {

        return runeCombatType;

    }
}
