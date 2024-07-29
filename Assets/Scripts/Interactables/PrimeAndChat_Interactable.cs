using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeAndChat_Interactable : Chat_Interactable
{
    [SerializeField] protected Activatable ToBePrimed;
    
    public override void Interact()
    {
        ToBePrimed.Prime();
        base.Interact();
    }
}
