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
        VoidStartRoom,
        MiddleRoom,
        VoidLevel,

        Void_Apartment,
        Void_LogCabin,
        Void_Hallway,
        Void_LogCabin_Tutorial,
        
    }

    public static class SceneNameExtensions
    {
        public static string GetSceneNameString(this SceneName _name)
        {
            return _name.ToString();
        }
    }
}
