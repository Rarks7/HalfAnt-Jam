using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class ResourceLoader
    {
        public static Dictionary<string, GameObject> Prefabs = new();
        public static readonly string PrefabPath = "Prefabs/";

        public static GameObject LoadPrefab(string name)
        {
            if (Prefabs.ContainsKey(name))
            {
                Prefabs.Add(name, Resources.Load<GameObject>(name));
            }

            return Prefabs[name];
        }

        public static GameObject LoadAndInstantiate(string name, Vector3 position)
        {
            GameObject loadable = LoadPrefab(name);

            return Object.Instantiate(loadable, position, Quaternion.identity);
        }

        public static T LoadAndInstantiate<T>(string name, Vector3 position) where T : MonoBehaviour
        {
            GameObject spawned = LoadAndInstantiate(name, position);
            return spawned.GetComponent<T>();
        }
    }

}


