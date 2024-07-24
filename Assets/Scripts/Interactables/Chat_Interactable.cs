using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat_Interactable : Interactable
{
    [SerializeField] private StorySection storySection;
    
    public override void Interact()
    {
        NarrativeManager.Instance.PlayStorySection(storySection);
    }


}
