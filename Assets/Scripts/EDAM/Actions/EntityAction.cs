using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EDAM
{
    namespace Action
    {
        [Serializable]
        public abstract class EntityAction<T, U> where U : Data.Data, new() where T : Module.Module<U>
        {
            [SerializeReference] public List<T> Modules = new ();
            protected Entity.Entity Owner;

            public float ActivationDelay = 0;
            
            public EntityAction(Entity.Entity owner)
            {
                Owner = owner;
            }

            public void AddModule(T _module)
            {
                Modules.Add(_module);
                ValidateModules();
            }

            public void RemoveModule(T _module)
            {
                Modules.Remove(_module);
                ValidateModules();
            }
            /// <summary>
            /// Validate the order and collection of modules to ensure there are no errors or inconsistencies
            /// e.g. Elemental modules should have only one instance.
            /// </summary>
            protected abstract void ValidateModules();

            /// <summary>
            /// Initialize the action, set any data points needed, do anything needed before modules are processed
            /// </summary>
            /// /// <param name="data">The data, not yet module processed</param>
            protected abstract void ActionAwake(ref U data);
            /// <summary>
            /// Perform the Actions function with the module processed data
            /// </summary>
            /// <param name="data">The module modified data</param>
            protected abstract void ActionExecute(ref U data);

            /// <summary>
            /// Wrap up the action, do anything needed to finish up the action
            /// </summary>
            protected abstract void ActionComplete(ref U data);

            /// <summary>
            /// Launch the given action
            /// </summary>
            public void ActivateAction()
            {              
                U data = new();
                ValidateModules(); // Ensure all the modules are correct
                ActionAwake(ref data);//Do any initilization needed
                ProcessModules(ref data); //Pass the data through the modules

                if(data.DontExecute) //Check to make sure none of the modules set DontExecute
                {
                    return;
                }

                if (ActivationDelay > 0)
                {
                    ActivationDelay -= Time.deltaTime;
                    if (ActivationDelay < 0)
                    {
                        ActivationDelay = 0;
                    }
                    return;
                }

                ActionExecute(ref data); //Actually do the action

                ActionComplete(ref data); //Wrap up the action
            }

            protected void ProcessModules(ref U data)
            {
                foreach (var item in Modules)
                {
                    item.Execute(ref data);
                }
            }
        }
    }
}
