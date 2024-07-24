using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ChatEntry
{
    public string MainText;

    public string OptionTextOne;
    public bool OneSelected;

    public string OptionTextTwo;
    public bool TwoSelected;

    public ChatEntry()
    {
        MainText = string.Empty;
    
    }

}

public enum ChatBoxState
{
    PlayingText,
    DownAvailable,
    OptionsAvailable,
}

public class MainChatBox : MonoBehaviour
{
    [SerializeField] private TextScroller Out_MainText;

    private ChatEntry CurrentEntry;

    [SerializeField] private Option OptionOne;
    [SerializeField] private Option OptionTwo;

    [SerializeField] private Image DownIndicator;

    private int NumberOfOptions = 0;
    private int CurrentSelected = 1;
    private string RemainingEntry = "";


    private const int CharacterLimit = 100;

    private void OnEnable()
    {
        Out_MainText.Clear();
        OptionOne.Clear();
        OptionTwo.Clear();

        DownIndicator.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Out_MainText.OnScrollComplete -= DisplayDownIndicator;
        Out_MainText.OnScrollComplete -= DisplayEntryOptions;

        EventManager.OnPlayerPressedDown -= PlayNextText;
        EventManager.OnPlayerPressedInteract -= PlayNextText;

        EventManager.OnPlayerPressedLeft -= MoveSelected;
        EventManager.OnPlayerPressedRight -= MoveSelected;

        EventManager.OnPlayerPressedInteract -= MakeSelection;

        EventManager.OnPlayerPressedInteract -= CloseMainText;
    }

    public void PlayEntry(ChatEntry entry)
    {
        CurrentEntry = entry;
        NumberOfOptions = 0;
        CurrentSelected = 1;
        RemainingEntry = entry.MainText;
        DownIndicator.gameObject.SetActive(false);

        PlayMainText();
    }

    private void PlayMainText()
    {
        DownIndicator.gameObject.SetActive(false);
        OptionOne.Clear();
        OptionTwo.Clear();

        EventManager.OnPlayerPressedInteract += SkipTextScrolling;
        
        if (RemainingEntry.Length > CharacterLimit)
        {
            string playThisTime = string.Join("", RemainingEntry.Take(CharacterLimit));
            RemainingEntry = RemainingEntry.Remove(0, CharacterLimit);

            Out_MainText.OnScrollComplete += DisplayDownIndicator;
            Out_MainText.PlayString(playThisTime);
        }
        else
        {
            Out_MainText.OnScrollComplete += DisplayEntryOptions;
            
            Out_MainText.PlayString(RemainingEntry);
        }
    }

    private void SkipTextScrolling()
    {
        Out_MainText.SkipScroll();
    }

    private void DisplayDownIndicator()
    {
        EventManager.OnPlayerPressedInteract -= SkipTextScrolling;

        Out_MainText.OnScrollComplete -= DisplayDownIndicator;

        DownIndicator.gameObject.SetActive(true);
        EventManager.OnPlayerPressedDown += PlayNextText;
        EventManager.OnPlayerPressedInteract += PlayNextText;
        Debug.Log("Display Down Indicator");
    }

    private void DisplayEntryOptions()
    {
        EventManager.OnPlayerPressedInteract -= SkipTextScrolling;
        Out_MainText.OnScrollComplete -= DisplayEntryOptions;

        if (CurrentEntry.OptionTextOne == null && CurrentEntry.OptionTextTwo == null)
        {
            EventManager.OnPlayerPressedInteract += CloseMainText;
            return;
        }


        OptionOne.SetOption(CurrentEntry.OptionTextOne, CurrentEntry.OneSelected);
        if (CurrentEntry.OptionTextOne != "")
        {
            NumberOfOptions++;
        }


        OptionTwo.SetOption(CurrentEntry.OptionTextTwo, CurrentEntry.TwoSelected);
        if (CurrentEntry.OptionTextTwo != "")
        {
            NumberOfOptions++;
        }


        EventManager.OnPlayerPressedLeft += MoveSelected;
        EventManager.OnPlayerPressedRight += MoveSelected;

        EventManager.OnPlayerPressedInteract += MakeSelection;
    }


    private void PlayNextText()
    {
        EventManager.OnPlayerPressedDown -= PlayNextText;
        EventManager.OnPlayerPressedInteract -= PlayNextText;

        Debug.Log("Play Next Text");

        PlayMainText();
    }


    private void MoveSelected()
    {
        if(NumberOfOptions < 2)
        {
            return;
        }

        if(CurrentSelected == 1)
        {
            CurrentSelected = 2;

            OptionOne.SetSelected(false);
            OptionTwo.SetSelected(true);
        }
        else if(CurrentSelected == 2)
        {
            CurrentSelected = 1;

            OptionOne.SetSelected(true);
            OptionTwo.SetSelected(false);
        }
    }

    private void MakeSelection()
    {
        EventManager.PlayerMadeOptionSelection(CurrentSelected);

        EventManager.OnPlayerPressedLeft -= MoveSelected;
        EventManager.OnPlayerPressedRight -= MoveSelected;

        EventManager.OnPlayerPressedInteract -= MakeSelection;
    }

    public void CloseMainText()
    {
        EventManager.OnPlayerPressedInteract -= CloseMainText;
        NarrativeManager.Instance.CloseChatBox();
    }
}
