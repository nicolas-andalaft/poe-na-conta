using UnityEngine;

public class EventManager : MonoBehaviour {

    [SerializeField] int playerCount;

    public static EventManager global { get; private set; }
    public static PlayerEvents[] playerEvents { get; private set; }

    void Awake() {
        global = this;
        if (playerCount <= 0) playerCount = 1;
        playerEvents = new PlayerEvents[playerCount];
        for (int i = 0; i < playerCount; i++) {
            playerEvents[i] = new PlayerEvents();
        }
    }

    public static PlayerEvents player(int playerIndex) {
        return playerEvents[playerIndex];
    }
}