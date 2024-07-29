using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : Interactable
{
    [SerializeField] private bool ActivatedByChat;
    [SerializeField] private bool ShouldBePrimed;

    [NonSerialized] private bool Primed;

    private void Awake()
    {
        if(ActivatedByChat && !ShouldBePrimed)
        {
            EventManager.OnChatActivateTag += Activate;
        }
    }


    public override void Interact()
    {
        if(!ActivatedByChat)
        {
            if(ShouldBePrimed && Primed) { Activate(); }
            
            if(!ShouldBePrimed) { Activate(); }
        }
        
    }

    public virtual void Activate(){}

    public void Prime()
    {
        Primed = true;
        if(ActivatedByChat)
        {
            EventManager.OnChatActivateTag += Activate;
        }
    }
}
