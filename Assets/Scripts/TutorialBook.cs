using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBook : Interactable
{
    [SerializeField] private GameObject tutorialBox;
    [SerializeField] private DownPointerArrow arrow;

    private void OnEnable()
    {
        if(isInteractable)
        {
            arrow.gameObject.SetActive(true);
        }
    }
    public override void Interact()
    {
        tutorialBox.SetActive(true);
        isInteractable = false;

        EventManager.OnPlayerPressedInteract += CloseTutorialBox;
        arrow.gameObject.SetActive(false);
    }

    private void CloseTutorialBox()
    {
        tutorialBox.SetActive(false);
        Debug.Log("Closing Tutorial Box");


        EventManager.OnPlayerPressedInteract -= CloseTutorialBox;
        

        EventManager.LaunchTutorialCombat();
    }
}
