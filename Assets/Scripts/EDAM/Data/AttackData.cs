
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDAM
{
    namespace Data
    {
        public class AttackData : Data
        {
            public int NumberOfAttacks = 0;
            public List<ElementType> AttackElements = new() { ElementType.Empty };
            public float AttackDelay = 1.0f; //Reverse of an attack rate, the delay between attacks

            public string AttackPrefabName = "Bullet";
        }
    }

}
    
