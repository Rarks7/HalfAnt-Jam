using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonModule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateSummon(int _fire, int _ice, int _lightning)
    {

        if (_fire == 3)
        {
            Debug.Log("Fire, Fire & Fire");
            

        }
        else if (_ice == 3)
        {
            Debug.Log("Ice, Ice & Ice");


        }
        else if (_lightning == 3)
        {
            Debug.Log("Lightning, Lightning & Lightning");


        }

        else if (_fire == 2 && _ice == 1)
        {
            Debug.Log("Fire, Fire & Ice");


        }
        else if (_fire == 2 && _lightning == 1)
        {
            Debug.Log("Fire, Fire & Lightning");


        }
        else if (_ice == 2 && _fire == 1)
        {

            Debug.Log("Ice, Ice & Fire");


        }
        else if (_ice == 2 && _lightning == 1)
        {

            Debug.Log("Ice, Ice & Lightning");


        }
        else if (_lightning == 2 && _fire == 1)
        {

            Debug.Log("Lightning,Lightning & Fire");


        }
        else if (_lightning == 2 && _ice == 1)
        {

            Debug.Log("Lightning,Lightning & Ice");


        }
        else if (_fire == 1 && _ice == 1 && _lightning == 1)
        {

            Debug.Log("Fire,Ice & Lightning");


        }


        else if (_fire == 2)
        {
            Debug.Log("Fire & Fire");


        }
        else if (_ice == 2)
        {

            Debug.Log("Ice & Ice");


        }
        else if (_lightning == 2)
        {

            Debug.Log("Lightning & Lightning");


        }

        else if (_fire == 1 && _ice == 1)
        {
            Debug.Log("Fire & Ice");


        }
        else if (_ice == 1 && _lightning == 1)
        {

            Debug.Log("Ice & Lightning");


        }
        else if (_lightning == 1 && _fire == 1)
        {

            Debug.Log("Lightning & Fire");


        }

        else if (_fire == 1)
        {
            Debug.Log("Fire");


        }
        else if (_ice == 1)
        {

            Debug.Log("Ice");


        }
        else if (_lightning == 1)
        {

            Debug.Log("Lightning");


        }


    }
}
