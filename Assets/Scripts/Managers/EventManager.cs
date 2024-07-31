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

    public static Action<int> OnPlayerMadeOptionSelection;
    public static void PlayerMadeOptionSelection(int _option)
    {
        OnPlayerMadeOptionSelection?.Invoke(_option);
    }

    public static Action<GameState> OnChangeGameState;
    public static void ChangeGameState(GameState _gameState)
    {
        OnChangeGameState?.Invoke(_gameState);
    }

    public static Action OnRevertToPreviousGameState;
    public static void RevertToPreviousGameState()
    {
        OnRevertToPreviousGameState?.Invoke();
    }

    public static Action OnChatActivateTag;

    public static void ChatActivateTag()
    {
        OnChatActivateTag?.Invoke();
    }

    public static Action OnActivateVoidDoor;
    public static void ActivateVoidDoor()
    {
        OnActivateVoidDoor?.Invoke();
    }

    public static Action<int> OnStartCombat;
    public static void StartCombat(int _roomNumber)
    {
        OnStartCombat?.Invoke(_roomNumber);
    }

    public static Action OnCombatCompleted;
    public static void CombatCompleted()
    {
        OnCombatCompleted?.Invoke();
    }

    public static Action OnPlayerCharacterDied;
    public static void PlayerCharacterDied()
    {
        OnPlayerCharacterDied?.Invoke();
    }

    public static Action<Rune> OnAddRuneToDeck;
    public static void AddRuneToDeck(Rune _rune)
    {
        OnAddRuneToDeck?.Invoke(_rune);
    }
}
