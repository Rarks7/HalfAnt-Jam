using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] TMP_Text startText;
    private Color originalColor;


    // Start is called before the first frame update
    void Start()
    {
        originalColor = startText.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        FlashText();
    }

    private void FlashText()
    {

            float alpha = Mathf.Abs(Mathf.Cos(Time.time * Mathf.PI / 1));
            startText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        
    }

    public void StartGame(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            SceneManager.LoadScene(SceneName.Lobby.GetSceneNameString());

        }


    }
}
