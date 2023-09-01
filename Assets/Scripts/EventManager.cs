using System;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager global { get; private set; }
    static PlayerEvents[] playerEvents;

    void Awake() {
        global = this;
        playerEvents = new PlayerEvents[4];
    }

    public static PlayerEvents player(int playerIndex) {
        return playerEvents[playerIndex];
    }

    public static void addPlayerEvent() {
        playerEvents[Player.playerCount-1] = new PlayerEvents();
    }

    public event Action<int> onGamepadAdded;
    public void TriggerOnGamepadAdded(int gamepadIndex) {
        onGamepadAdded?.Invoke(gamepadIndex);
    }

    public event Action<int> onGamepadRemoved;
    public void TriggerOnGamepadRemoved(int gamepadIndex) {
        onGamepadRemoved?.Invoke(gamepadIndex);
    }
}