using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] TMP_Text deckNumber;
    [SerializeField] TMP_Text healthNumber;


    [SerializeField] TMP_Text dashText;
    [SerializeField] Image dashImage;

    [SerializeField] TMP_Text shuffleText;
    [SerializeField] Image shuffleImage;

    [SerializeField] TMP_Text recallText;
    [SerializeField] Image recallImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetRemainingDeckNumber(int _deckNumber)
    {


        deckNumber.text = _deckNumber.ToString();

    }
    public void SetHealthText(float _health)
    {


        healthNumber.text = ((int)_health).ToString();

    }

    public void SetDashDulled(bool _isDulled)
    {
        if (_isDulled)
        {
            dashText.color = Color.gray;
            dashImage.color = Color.gray;
        }
        else 
        {

            dashText.color = Color.white;
            dashImage.color = Color.white;

        }

    }

    public void SetShuffleDulled(bool _isDulled)
    {
        if (_isDulled)
        {
            shuffleText.color = Color.gray;
            shuffleImage.color = Color.gray;
        }
        else
        {

            shuffleText.color = Color.white;
            shuffleImage.color = Color.white;

        }

    }

    public void SetRecallDulled(bool _isDulled)
    {
        if (_isDulled)
        {
            recallText.color = Color.gray;
            recallImage.color = Color.gray;
        }
        else
        {

            recallText.color = Color.white;
            recallImage.color = Color.white;

        }

    }

}
