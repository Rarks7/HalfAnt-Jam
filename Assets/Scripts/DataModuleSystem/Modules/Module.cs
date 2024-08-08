using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace EDAM
{
    namespace Module
    {
        public abstract class Module<T> where T : Data.Data
        {
            public string name = "";

            public Module()
            {
                name = GetType().Name;
            }

            public abstract void Execute(ref T data);
        }
    }
}
