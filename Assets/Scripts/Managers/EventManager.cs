using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnPlayerPressedInteract;
    public static void PlayerPressedInteract()
    {
        OnPlayerPressedInteract?.Invoke();
    }

    public static Action OnPlayerPressedLeft;
    public static void PlayerPressedLeft()
    {
        OnPlayerPressedLeft?.Invoke();
    }

    public static Action OnPlayerPressedRight;
    public static void PlayerPressedRight()
    {
        OnPlayerPressedRight?.Invoke();
    }

    public static Action OnPlayerPressedUp;
    public static void PlayerPressedUp()
    {
        OnPlayerPressedUp?.Invoke();
    }

    public static Action OnPlayerPressedDown;
    public static void PlayerPressedDown()
    {
        OnPlayerPressedDown?.Invoke();
    }

}
