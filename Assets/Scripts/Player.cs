using UnityEngine;

public class Player : MonoBehaviour {

    public static int playerCount { get; private set; } = 0;
    public int index { get; private set; }

    void Awake() {
        index = playerCount;
        playerCount++;
        EventManager.addPlayerEvent();
    }

    void OnDestroy() {
        playerCount--;
    }
}