using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidHallwayDoor : Door_Interactable
{
    [SerializeField] protected RuneHolder doorRune;
    private Rune assignedRune;

    private List<ElementType> PossibleElementTypes;
    private List<CombatType>  PossibleCombatTypes;

    private void Start()
    {
        PossibleCombatTypes = new List<CombatType>()
        {
            CombatType.Fighter,
            CombatType.Ranger,
            CombatType.Mage,
        };

        PossibleElementTypes = new List<ElementType>()
        {
            ElementType.Fire,
            ElementType.Ice,
            ElementType.Lightning,
        };
        
        
        assignedRune = new();




        assignedRune.SetRuneCombatType(PossibleCombatTypes[Random.Range(0, PossibleCombatTypes.Count)]);
        assignedRune.SetRuneElementType(PossibleElementTypes[Random.Range(0, PossibleElementTypes.Count)]);

        doorRune.IngestRune(assignedRune);

    }

    public override void Interact()
    {
        EventManager.AddRuneToDeck(assignedRune);
        base.Interact();
    }
}
