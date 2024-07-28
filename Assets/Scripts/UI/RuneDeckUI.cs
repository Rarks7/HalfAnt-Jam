using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneDeckUI : MonoBehaviour
{
    public static RuneDeckUI Instance { get; private set; }


    public List<RuneHolder> runeHandUI;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FillRuneDeckUI(List<Rune> _runeHand)
    {

        for (int i = 0; i < _runeHand.Count; i++)
        {

            runeHandUI[i].IngestRune(_runeHand[i]);

        }


    }

}
