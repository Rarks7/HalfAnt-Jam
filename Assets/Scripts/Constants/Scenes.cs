using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum SceneName
    {
        None,
        Lobby,
        Overworld,
    }

    public static class SceneNameExtensions
    {
        public static string GetSceneNameString(this SceneName _name)
        {
            return _name switch
            {
                SceneName.Lobby => "Lobby",
                SceneName.Overworld => "Overworld",
                _ => "Error",
            };
        }
    }
}
