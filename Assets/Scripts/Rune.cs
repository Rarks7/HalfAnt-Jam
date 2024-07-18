using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RuneType
{
    Empty,
    Fire,
    Ice,
    Lightning

}


public class Rune : MonoBehaviour
{

    RuneType runeType = RuneType.Fire;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RuneType GetRuneType()
    {

        return runeType;

    }
}
