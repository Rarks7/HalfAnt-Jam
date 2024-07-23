
using Constants;
using NaughtyAttributes;
using UnityEngine;


public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager Instance;
    [SerializeField] private MainChatBox chatBox;

    [Multiline] [SerializeField] private string TestMainText;

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
        chatBox.gameObject.SetActive(false);
    }

    public void PlayStorySection(StorySection _section)
    {

    }

    [Button]
    public void TestMainTextDisplay()
    {
        OpenChatBox();

        chatBox.PlayEntry(new ChatEntry()
        {
            MainText = TestMainText,
            OptionTextOne = "Yes",
            OneSelected = true,
            OptionTextTwo = "No",
            TwoSelected = false,
        });
        EventManager.OnPlayerMadeOptionSelection += ReadPlayerOptionSelection;
    }

    private void OpenChatBox()
    {
        chatBox.gameObject.SetActive(true);
        EventManager.ChangeGameState(GameState.Chatting);
    }

    private void CloseChatBox()
    {
        chatBox.gameObject.SetActive(false);
        EventManager.RevertToPreviousGameState();

    }

    private void ReadPlayerOptionSelection(int selection)
    {
        EventManager.OnPlayerMadeOptionSelection -= ReadPlayerOptionSelection;

        Debug.Log("Player Option Select: " + selection);

        if(selection == 1)
        {
            CloseChatBox();
        }
        else if(selection == 2)
        {
            chatBox.PlayEntry(new ChatEntry()
            {
                MainText = "No? You dont speak Latin?",
                OptionTextOne = "Yes",
                OneSelected = true,
                OptionTextTwo = "No",
                TwoSelected = false,
            });
        }
    }
}
