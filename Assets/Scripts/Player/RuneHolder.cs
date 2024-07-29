using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneHolder : MonoBehaviour
{

    public Rune rune;

    [SerializeField] SpriteRenderer bgSpriteRenderer;
    [SerializeField] SpriteRenderer elementSpriteRenderer;
    [SerializeField] SpriteRenderer combatSpriteRenderer;

    [SerializeField] RuneData runeData;


    public bool selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }


    public void IngestRune(Rune _rune)
    {
        rune = _rune;
        elementSpriteRenderer.sprite = runeData.GetElementSprite(_rune.GetRuneElementType());
        combatSpriteRenderer.sprite = runeData.GetCombatSprite(_rune.GetRuneCombatType());
        Activate();

    }

    public void Select()
    {

        if (!selected)
        {
            bgSpriteRenderer.color = new Color(bgSpriteRenderer.color.r, bgSpriteRenderer.color.g, bgSpriteRenderer.color.b, 0.5f);
            elementSpriteRenderer.color = new Color(elementSpriteRenderer.color.r, elementSpriteRenderer.color.g, elementSpriteRenderer.color.b, 0.5f);
            combatSpriteRenderer.color = new Color(combatSpriteRenderer.color.r, combatSpriteRenderer.color.g, combatSpriteRenderer.color.b, 0.5f);
            selected = true;
        }
        

    }

    public void StartCantSelect()
    {

        StartCoroutine(CantSelect());


    }


    IEnumerator CantSelect()
    {
        Color bgR = bgSpriteRenderer.color;
        Color eR = elementSpriteRenderer.color;
        Color cR = combatSpriteRenderer.color;

        bgSpriteRenderer.color = new Color(bgSpriteRenderer.color.r, 0, 0, 0.5f);
        elementSpriteRenderer.color = new Color(elementSpriteRenderer.color.r, 0, 0, 0.5f);
        combatSpriteRenderer.color = new Color(combatSpriteRenderer.color.r, 0, 0, 0.5f);


        yield return new WaitForSeconds(0.1f);

        bgSpriteRenderer.color = bgR;
        elementSpriteRenderer.color = eR;
        combatSpriteRenderer.color = cR;
    }

    public void Deselect()
    {
        bgSpriteRenderer.color = new Color(bgSpriteRenderer.color.r, bgSpriteRenderer.color.g, bgSpriteRenderer.color.b, 1f);
        elementSpriteRenderer.color = new Color(elementSpriteRenderer.color.r, elementSpriteRenderer.color.g, elementSpriteRenderer.color.b, 1f);
        combatSpriteRenderer.color = new Color(combatSpriteRenderer.color.r, combatSpriteRenderer.color.g, combatSpriteRenderer.color.b, 1f);
        selected = false;
    }


    public void Activate()
    {

        gameObject.SetActive(true);

    }

    public void Deactivate()
    {

        gameObject.SetActive(false);


    }
}
