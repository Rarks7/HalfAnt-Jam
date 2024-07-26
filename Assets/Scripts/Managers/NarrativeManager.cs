
using Constants;
using Ink.Runtime;
using NaughtyAttributes;
using System.Collections;
using System.Linq;
using UnityEngine;


public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager Instance;
    private MainChatBox chatBox;

    [SerializeField] private TextAsset inkJSONAsset = null;
    public Story story;

    private bool SectionHasEnded = false;

    private StorySection blockSection = StorySection.None;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }    
    }

    private void Start()
    {
        story = new Story(inkJSONAsset.text);
    }

    public void SetChatBox(MainChatBox _chatBox)
    {
        chatBox = _chatBox;   
        chatBox.gameObject.SetActive(false);
    }

    public void PlayStorySection(StorySection _section)
    {
        if (blockSection == _section)
            return;
        
        story.ChoosePathString(_section.ToString());
        blockSection = _section;
        PlayCurrentStorySection();
    }

    private void PlayCurrentStorySection()
    {
        ChatEntry entry = new ChatEntry();
        while (story.canContinue)
        {
            entry.MainText += story.Continue().Trim();
            ProcessCurrentStoryTags();
        }

        if (SectionHasEnded)
        {
            SectionHasEnded = false; //Reset variable for next time

            if(entry.MainText.Length > 0)
            {
                OpenChatBox();
                chatBox.PlayEntry(entry);
            }
            else
            {
                CloseChatBox();
            }    
            return;
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; ++i)
            {
                Choice choice = story.currentChoices[i];

                if (i == 0)
                {
                    entry.OptionTextOne = choice.text.Trim();
                    entry.OneSelected = true;
                }

                if (i == 1)
                {
                    entry.OptionTextTwo = choice.text.Trim();
                    entry.TwoSelected = false;
                }
            }

            EventManager.OnPlayerMadeOptionSelection += ReadPlayerOptionSelection;
        }

        OpenChatBox();
        chatBox.PlayEntry(entry);
    }

    [Button]
    public void TestMainTextDisplay()
    {
        
        PlayStorySection(StorySection.trash_bin);
        
        
    }

    private void OpenChatBox()
    {
        chatBox.gameObject.SetActive(true);
        EventManager.ChangeGameState(GameState.Chatting);
    }

    public void CloseChatBox()
    {
        chatBox.gameObject.SetActive(false);
        StartCoroutine(HoldOnClearingSectionBlock());
        EventManager.RevertToPreviousGameState();
    }

    private void ReadPlayerOptionSelection(int selection)
    {
        EventManager.OnPlayerMadeOptionSelection -= ReadPlayerOptionSelection;

        Debug.Log("Player Option Select: " + selection);

        story.ChooseChoiceIndex(selection - 1);

        PlayCurrentStorySection();
        
    }

    private void ProcessCurrentStoryTags()
    {
        if (story.currentTags.Contains(InkTags.End))
        {
  
            SectionHasEnded = true;
            return;
        }
    }

    private IEnumerator HoldOnClearingSectionBlock()
    {
        yield return new WaitForSeconds(0.1f);

        blockSection = StorySection.None;
    }
}
