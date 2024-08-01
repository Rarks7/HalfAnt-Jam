using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Pathfinding;
using Pathfinding.Examples;

public class Summon : AICharacter
{

    public List<Rune> runes;

    protected override void Awake()
    {
        base.Awake();
        runes = new List<Rune>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void SetRunes(List<Rune> _runes) 
    {
        foreach (var rune in _runes)
        {
            runes.Add(rune);
        }
        
    }

    public override void Die()
    {

        foreach (var rune in runes)
        {

            vfxModule.CreateReturnRuneVFX();

        }

        base.Die();

    }


}
