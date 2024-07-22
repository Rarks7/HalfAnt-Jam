
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
        chatBox.gameObject.SetActive(true);
        chatBox.PlayEntry(new ChatEntry()
        {
            MainText = TestMainText,

        });

    }
}
