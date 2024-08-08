using EDAM.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDAM
{
    namespace Module
    {
        [Serializable]
        public abstract class AttackModifierModule : Module<AttackData>
        {
            public override abstract void Execute(ref AttackData data);

        }

        [Serializable]
        public class SingleAttackModule : AttackModifierModule
        {
            public override void Execute(ref AttackData data)
            {
                data.NumberOfAttacks += 1;
            }
        }

        [Serializable]
        public class MultiAttackModule : AttackModifierModule
        {
            public int NumberOfAttacks = 0;

            public MultiAttackModule(int numberOfAttacks)
            {
                NumberOfAttacks = numberOfAttacks;
            }

            public override void Execute(ref AttackData data)
            {
                data.NumberOfAttacks += NumberOfAttacks;
            }
        }

        [Serializable]
        public class ElementAttackModule : AttackModifierModule
        {
            public ElementType AttackElement;

            public ElementAttackModule(ElementType attackElement)
            {
                AttackElement = attackElement;
            }

            public override void Execute(ref AttackData data)
            {
                if (data.AttackElements[0] == ElementType.Empty)
                {
                    data.AttackElements[0] = AttackElement;
                }
                else
                {
                    data.AttackElements.Add(AttackElement);
                }
            }
        }

    }

}


