using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeAndChat_Interactable : Chat_Interactable
{
    [SerializeField] protected Activatable ToBePrimed;

    [SerializeField] protected DownPointerArrow pointerArrow;
    private void OnEnable()
    {
        if(isInteractable)
        {
            pointerArrow.gameObject.SetActive(true);
        }
        else
        {
            pointerArrow.gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        ToBePrimed.Prime();
        base.Interact();
        isInteractable = false;
        pointerArrow.gameObject.SetActive(false);
    }
}
