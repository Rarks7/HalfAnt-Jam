using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] TMP_Text deckNumber;
    [SerializeField] TMP_Text healthNumber;


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

}
