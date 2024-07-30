using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonModule : MonoBehaviour
{

    [SerializeField] GameObject summon;

    [SerializeField] GameObject recall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void CreateSummon(List<Rune> _runes, int _fire, int _ice, int _lightning, int _melee, int _range, int _mage)
    {
        GameObject newSummon = Instantiate(summon, transform.position, Quaternion.identity);

        SetSummonElement(newSummon, _fire, _ice,_lightning);
        SetSummonCombatType(newSummon, _melee, _range, _mage);
        SetSummonStats(newSummon, _fire, _ice, _lightning);

        newSummon.GetComponent<Summon>().SetRunes(_runes);

        AIManager.Instance.activeSummons.Add(newSummon.GetComponent<Summon>());
    }

    public void RecallSummon()
    {


        GameObject newRecall = Instantiate(recall, transform.position, Quaternion.identity);


    }





    public void SetSummonCombatType(GameObject _newSummon, int _melee, int _range, int _mage)
    {

        if (_melee == 3)
        {


            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Fighter);


        }
        else if (_range == 3)
        {


            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Ranger);


        }
        else if (_mage == 3)
        {
            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Mage);



        }

        else if (_melee == 2 && _range == 1)
        {
            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Tank);



        }
        else if (_melee == 2 && _mage == 1)
        {
            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Enchanter);



        }
        else if (_range == 2 && _melee == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Tank);



        }
        else if (_range == 2 && _mage == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Thief);



        }
        else if (_mage == 2 && _melee == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Enchanter);



        }
        else if (_mage == 2 && _range == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Thief);



        }
        else if (_melee == 1 && _range == 1 && _mage == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Healer);



        }


        else if (_melee == 2)
        {


            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Fighter);



        }
        else if (_range == 2)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Ranger);



        }
        else if (_mage == 2)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Mage);



        }

        else if (_melee == 1 && _range == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Tank);



        }
        else if (_range == 1 && _mage == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Thief);



        }
        else if (_mage == 1 && _melee == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Enchanter);



        }

        else if (_melee == 1)
        {
            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Fighter);



        }
        else if (_range == 1)
        {


            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Ranger);



        }
        else if (_mage == 1)
        {

            _newSummon.GetComponent<Summon>().SetCombatType(CombatType.Mage);



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


            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Fire);
            

        }
        else if (_ice == 3)
        {


            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Ice);

        }
        else if (_lightning == 3)
        {
            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Lightning);


        }

        else if (_fire == 2 && _ice == 1)
        {
            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Crystal);


        }
        else if (_fire == 2 && _lightning == 1)
        {
            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Earth);


        }
        else if (_ice == 2 && _fire == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Crystal);


        }
        else if (_ice == 2 && _lightning == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Steel);


        }
        else if (_lightning == 2 && _fire == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Earth);


        }
        else if (_lightning == 2 && _ice == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Steel);


        }
        else if (_fire == 1 && _ice == 1 && _lightning == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Shadow);


        }


        else if (_fire == 2)
        {


            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Fire);


        }
        else if (_ice == 2)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Ice);


        }
        else if (_lightning == 2)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Lightning);


        }

        else if (_fire == 1 && _ice == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Crystal);


        }
        else if (_ice == 1 && _lightning == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Steel);


        }
        else if (_lightning == 1 && _fire == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Earth);


        }

        else if (_fire == 1)
        { 
            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Fire);


        }
        else if (_ice == 1)
        {


            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Ice);


        }
        else if (_lightning == 1)
        {

            _newSummon.GetComponent<Summon>().SetElementType(ElementType.Lightning);


        }


    }
}
