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


public class Rune : MonoBehaviour
{

    ElementType runeElementType = ElementType.Empty;
    CombatType runeCombatType = CombatType.Empty;
    RuneSpecial runeSpecial = RuneSpecial.Empty;

    [SerializeField] SpriteRenderer elementSpriteRenderer;
    [SerializeField] SpriteRenderer combatSpriteRenderer;


    [SerializeField] RuneData runeData;



    // Update is called once per frame
    void Update()
    {
        
    }


    public void Activate()
    {

        gameObject.SetActive(true);

    }

    public void Deactivate()
    {

        gameObject.SetActive(false);


    }

    public void SetRuneElementType(ElementType _type)
    {

        runeElementType = _type;

        elementSpriteRenderer.sprite = runeData.GetElementSprite(_type);

    }

    public void SetRuneCombatType(CombatType _type)
    {

        runeCombatType = _type;

        combatSpriteRenderer.sprite = runeData.GetCombatSprite(_type);

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
