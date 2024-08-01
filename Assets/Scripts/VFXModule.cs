using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXModule : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    Color originalColor;
    Color damageColor = Color.red;

    [SerializeField] Color plainColor = Color.white;
    [SerializeField] Color fireColor = Color.red;
    [SerializeField] Color iceColor = Color.cyan;
    [SerializeField] Color lightningColor = Color.magenta;
    [SerializeField] Color steelColor = Color.grey;
    [SerializeField] Color earthColor = Color.green;
    [SerializeField] Color crystalColor = Color.blue;
    [SerializeField] Color shadowColor = Color.black;


    float damageFlashDuration = 0.1f;


    [SerializeField] GameObject floatingText;
    [SerializeField] GameObject stunnedVFX;
    [SerializeField] GameObject shieldVFX;
    [SerializeField] GameObject runeReturnVFX;



    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetColor(ElementType _color)
    {

        switch (_color)
        {
            case ElementType.Empty:
                spriteRenderer.color = plainColor;

                break;
            case ElementType.Fire:
                spriteRenderer.color = fireColor;

                break;
            case ElementType.Ice:
                spriteRenderer.color = iceColor;

                break;
            case ElementType.Lightning:
                spriteRenderer.color = lightningColor;


                break;
            case ElementType.Earth:
                spriteRenderer.color = earthColor;

                break;
            case ElementType.Steel:
                spriteRenderer.color = steelColor;

                break;
            case ElementType.Crystal:
                spriteRenderer.color = crystalColor;

                break;
            case ElementType.Shadow:
                spriteRenderer.color = shadowColor;

                break;
            default:
                spriteRenderer.color = plainColor;

                break;
        }

        originalColor = spriteRenderer.color;
    }

    public void StartDamageFlash()
    {
        StartCoroutine(DamageFlash());

    }

    private IEnumerator DamageFlash()
    {

        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(damageFlashDuration);

        spriteRenderer.color = originalColor;
    }


    public void CreateFloatingText(Transform transform, string _string, TextType _type)
    {
        GameObject newFloatingText = Instantiate(floatingText, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);

        newFloatingText.GetComponentInChildren<FloatingText>().SetText(_string, _type);
        newFloatingText.transform.parent = transform;
    }


    public void CreateReturnRuneVFX()
    {
        float offset = Random.Range(-0.1f,0.1f);

        GameObject newruneReturnVFX = Instantiate(runeReturnVFX, transform.position, Quaternion.identity);

    
    }


    public void StunnedVFX(bool _show)
    {


        stunnedVFX.SetActive(_show);

    }

    public void ShieldVFX(bool _show)
    {


        shieldVFX.SetActive(_show);

    }
}
