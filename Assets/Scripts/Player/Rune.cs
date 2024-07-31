using System;


[Serializable]

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
[Serializable]
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

    public ElementType runeElementType = ElementType.Empty;
    public CombatType runeCombatType = CombatType.Empty;
    public RuneSpecial runeSpecial = RuneSpecial.Empty;

    





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
