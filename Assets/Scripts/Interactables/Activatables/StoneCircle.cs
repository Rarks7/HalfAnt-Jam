using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCircle : Activatable
{
    Animator ani;

    [SerializeField] private GameObject TurnOnInteractable;

    public const string ActivateString = "Activate";

    private void Awake()
    {
        ani = GetComponent<Animator>();
        TurnOnInteractable.SetActive(false);
    }

    public override void Activate()
    {
        ani.SetBool(ActivateString, true);
        TurnOnInteractable.SetActive(true);
    }
}
