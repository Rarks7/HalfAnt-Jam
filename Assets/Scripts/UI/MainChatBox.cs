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

    public void SetEntry(ChatEntry entry)
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
        if (RemainingEntry.Length > CharacterLimit)
        {
            string playThisTime = RemainingEntry.Take(CharacterLimit).ToString();
            RemainingEntry = RemainingEntry.Remove(0, CharacterLimit);

            Out_MainText.OnScrollComplete += DisplayDownIndicator;
            Out_MainText.PlayString(playThisTime);
        }
        else
        {
            Out_MainText.OnScrollComplete += DisplayEntryOptions;
            
            Out_MainText.PlayString(CurrentEntry.MainText);
        }
    }

    private void DisplayDownIndicator()
    {
        DownIndicator.gameObject.SetActive(true);
    }

    private void PlayNextText()
    {
        PlayMainText();
    }

    private void DisplayEntryOptions()
    {
        OptionOne.SetOption(CurrentEntry.OptionTextOne, CurrentEntry.OneSelected);
        if(CurrentEntry.OptionTextOne != "")
        {
            NumberOfOptions++;
        }


        OptionTwo.SetOption(CurrentEntry.OptionTextTwo, CurrentEntry.TwoSelected);
        if(CurrentEntry.OptionTextTwo != "")
        {
            NumberOfOptions++;
        }
    }

    public void MoveSelected()
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

    public int GetSelected()
    {
        return CurrentSelected;
    }


}
