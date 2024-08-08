using EDAM.Action;
using EDAM.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EDAM
{
    namespace Entity
    {
        public class Entity : MonoBehaviour
        {
            [SerializeReference] public AttackAction AttackAction;

            private void Awake()
            {
                AttackAction = new AttackAction();
                
                AttackAction.AddModule(new SingleAttackModule());
                AttackAction.AddModule(new MultiAttackModule(2));
                AttackAction.AddModule(new ElementAttackModule(ElementType.Fire));
            }
        }

    }
}


