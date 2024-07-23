using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXModule : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    Color originalColor;
    Color damageColor = Color.red;


    float damageFlashDuration = 0.1f;

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


    public void SetColor(RuneType _color)
    {
        switch (_color)
        {
            case RuneType.Empty:
                spriteRenderer.color = Color.white;
                
                break;
            case RuneType.Fire:
                spriteRenderer.color = Color.red;

                break;
            case RuneType.Ice:
                spriteRenderer.color = Color.cyan;

                break;
            case RuneType.Lightning:
                spriteRenderer.color = Color.magenta;

                break;
            default:
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

}
