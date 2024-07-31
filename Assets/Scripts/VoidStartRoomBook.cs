using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidStartRoomBook : PrimeAndChat_Interactable
{
    private void Awake()
    {
        if(GameManager.Instance.Flag_VoidRoomActivated)
        {
            isInteractable = false;
        }
    }


    public override void Interact()
    {
        base.Interact();

    }
}
