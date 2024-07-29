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
        Hallway,
        VoidHallway,
        PlayersRoom,
        NeighboursRoom,
        MiddleRoom,
        VoidLevel,
    }

    public static class SceneNameExtensions
    {
        public static string GetSceneNameString(this SceneName _name)
        {
            return _name.ToString();
        }
    }
}
