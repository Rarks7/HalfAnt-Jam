using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonModule : MonoBehaviour
{

    [SerializeField] GameObject summon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSummon(int _runeAmount,int _fire, int _ice, int _lightning)
    {
        GameObject newSummon = Instantiate(summon, transform.position, Quaternion.identity);

        SetSummonElement(newSummon, _fire, _ice,_lightning);
        SetSummonCombatType(newSummon, _runeAmount);
        SetSummonStats(newSummon, _fire, _ice, _lightning);

    }

    public void SetSummonCombatType(GameObject _newSummon, int _runeAmount)
    {

        switch (_runeAmount)
        {
            case 1:
                _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Melee);
                break;
            case 2:
                _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Range);

                break;
            case 3:
                _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Mage);

                break;
            default:
                _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Empty);

                break;
        }


    }
    public void SetSummonStats(GameObject _newSummon, int _fire, int _ice, int _lightning)
    {


        _fire *= 3;// Replace with Damage Multiplier
        _ice *= 7;// Replace with Health Mulitplier

        _newSummon.GetComponent<Summon>().SetStats(_fire, _ice, _lightning);


    }

    public void SetSummonElement(GameObject _newSummon, int _fire, int _ice, int _lightning)
    {


        if (_fire == 3)
        {
            Debug.Log("Fire, Fire & Fire");

            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);
            

        }
        else if (_ice == 3)
        {
            Debug.Log("Ice, Ice & Ice");

            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Ice);

        }
        else if (_lightning == 3)
        {
            Debug.Log("Lightning, Lightning & Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Lightning);


        }

        else if (_fire == 2 && _ice == 1)
        {
            Debug.Log("Fire, Fire & Ice");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);


        }
        else if (_fire == 2 && _lightning == 1)
        {
            Debug.Log("Fire, Fire & Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);


        }
        else if (_ice == 2 && _fire == 1)
        {

            Debug.Log("Ice, Ice & Fire");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Ice);


        }
        else if (_ice == 2 && _lightning == 1)
        {

            Debug.Log("Ice, Ice & Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Ice);


        }
        else if (_lightning == 2 && _fire == 1)
        {

            Debug.Log("Lightning,Lightning & Fire");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Lightning);


        }
        else if (_lightning == 2 && _ice == 1)
        {

            Debug.Log("Lightning,Lightning & Ice");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Lightning);


        }
        else if (_fire == 1 && _ice == 1 && _lightning == 1)
        {

            Debug.Log("Fire,Ice & Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);


        }


        else if (_fire == 2)
        {
            Debug.Log("Fire & Fire");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);


        }
        else if (_ice == 2)
        {

            Debug.Log("Ice & Ice");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Ice);


        }
        else if (_lightning == 2)
        {

            Debug.Log("Lightning & Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Lightning);


        }

        else if (_fire == 1 && _ice == 1)
        {
            Debug.Log("Fire & Ice");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);


        }
        else if (_ice == 1 && _lightning == 1)
        {

            Debug.Log("Ice & Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Ice);


        }
        else if (_lightning == 1 && _fire == 1)
        {

            Debug.Log("Lightning & Fire");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Lightning);


        }

        else if (_fire == 1)
        {
            Debug.Log("Fire");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Fire);


        }
        else if (_ice == 1)
        {

            Debug.Log("Ice");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Ice);


        }
        else if (_lightning == 1)
        {

            Debug.Log("Lightning");
            _newSummon.GetComponent<Summon>().SetElementType(RuneType.Lightning);


        }


    }
}
