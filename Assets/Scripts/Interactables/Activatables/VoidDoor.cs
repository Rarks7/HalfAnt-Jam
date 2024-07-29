using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDoor : Activatable
{
    private Animator doorAnimator;
    [SerializeField] private Animator circleAnimator;


    private const string ActivateCircleString = "Activate";
    private const string AppearDoorString = "Appear";
    private const string TransformDoorString = "Transform";

    [SerializeField] private GameObject DoorPoint;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        DoorPoint.SetActive(false);

        EventManager.OnActivateVoidDoor += Activate;
    }

    private void OnDestroy()
    {
        EventManager.OnActivateVoidDoor -= Activate;
    }

    public override void Activate()
    {
        StartCoroutine(DoorAppears());   
    }

    private IEnumerator DoorAppears()
    {
        circleAnimator.SetBool(ActivateCircleString, true);

        yield return new WaitForSeconds(2.0f);

        doorAnimator.SetTrigger(AppearDoorString);

        yield return new WaitForSeconds(2.0f);

        doorAnimator.SetTrigger(TransformDoorString);

        DoorPoint.SetActive(true);
    }


}
