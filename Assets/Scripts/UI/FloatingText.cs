using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TextType
{
    Damage,
    Heal,
    Buff,
    Debuff,
    Shield,
    Stun,
    Block



}

public class FloatingText : MonoBehaviour
{


    TMP_Text textMesh;
    SpriteRenderer spriteRenderer;
    float yIncrement;


    [SerializeField] Sprite attackSprite;
    [SerializeField] Sprite healSprite;
    [SerializeField] Sprite buffSprite;
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite stunSprite;
    [SerializeField] Sprite blockSprite;


    private void Awake()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Destroy(gameObject, 2.0f);
        yIncrement = transform.position.y;

    }
    private void Update()
    {
        
        yIncrement += Time.deltaTime;

        transform.position = new Vector3(transform.position.x, yIncrement, transform.position.z);

        textMesh.fontSize -= Time.deltaTime;
        textMesh.alpha -= Time.deltaTime;


        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, textMesh.alpha);


    }

    public void SetText(string _string, TextType _type)
    {

        switch (_type)
        {
            case TextType.Damage:
                textMesh.text = _string;
                textMesh.color = Color.red;
                spriteRenderer.sprite = attackSprite;

                break;
            case TextType.Heal:
                textMesh.text = _string;
                textMesh.color = Color.green;
                spriteRenderer.sprite = healSprite;


                break;
            case TextType.Buff:
                textMesh.text = "ATK UP";
                textMesh.color = Color.green;
                spriteRenderer.sprite = buffSprite;

                break;
            case TextType.Debuff:
                textMesh.text = "ATK DOWN";
                textMesh.color = Color.red;
                spriteRenderer.sprite = buffSprite;

                break;
            case TextType.Shield:
                textMesh.text = "SHLD UP";
                textMesh.color = Color.green;
                spriteRenderer.sprite = shieldSprite;

                break;
            case TextType.Stun:
                textMesh.text = "STUN";
                textMesh.color = Color.red;
                spriteRenderer.sprite = stunSprite;

                break;
            case TextType.Block:
                textMesh.text = "Block";
                textMesh.color = Color.cyan;
                spriteRenderer.sprite = blockSprite;

                break;
            default:
                break;
        }


    }


}
