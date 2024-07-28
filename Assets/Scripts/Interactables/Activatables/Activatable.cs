using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : Interactable
{
    public bool ActivatedByChat;

    private void Awake()
    {
        if(ActivatedByChat)
        {

        }
    }

    public override void Interact()
    {
        if(!ActivatedByChat)
        {
            Activate();
        }
        
    }

    public virtual void Activate(){}
}
