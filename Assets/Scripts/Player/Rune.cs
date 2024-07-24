using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;


public enum RuneType
{
    Empty,
    Fire,
    Ice,
    Lightning

}

public enum RuneSpecial
{

    HealingAura,
    DamageAura,


}


public class Rune : MonoBehaviour
{

    RuneType runeType = RuneType.Empty;

    [SerializeField] SpriteRenderer spriteRenderer;

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

    public void SetRuneType(RuneType _type)
    {

        runeType = _type;

        spriteRenderer.sprite = runeData.GetSprite(_type);

    }

    public RuneType GetRuneType()
    {

        return runeType;

    }
}
