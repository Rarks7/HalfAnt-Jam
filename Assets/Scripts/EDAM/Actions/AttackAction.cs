using EDAM.Data;
using EDAM.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDAM
{
    namespace Action
    {

        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        public class AttackAction : EntityAction<AttackModifierModule, AttackData>
        {
            public AttackAction(Entity.Entity owner) : base(owner){}


            protected override void ActionAwake(ref AttackData data)
            {
               
            }


            protected override void ActionExecute(ref AttackData data)
            {

            }

            protected override void ActionComplete(ref AttackData data)
            {
                ActivationDelay = data.AttackDelay;
            }

            protected override void ValidateModules()
            {
                
            }
        }
    }
}

