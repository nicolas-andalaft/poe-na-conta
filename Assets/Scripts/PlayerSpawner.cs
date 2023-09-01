using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnpoint;
    List<Player> players;
    int playerCount = 1;

    void Awake() {
        players = new List<Player>();
    }

    void OnEnable() {
        InputSystem.onDeviceChange += onDeviceChange;
    }

    void OnDisable() {
        InputSystem.onDeviceChange -= onDeviceChange;
    }

    void Start() {
        spawnPlayer();
    }

    void onDeviceChange(InputDevice device, InputDeviceChange change) {
        switch (change) {
            case InputDeviceChange.Added:
                onGamepadAdded();
                break;
            case InputDeviceChange.Removed:
                onGamepadRemoved();
                break;
        }
    }

    void onGamepadAdded() {
        int gamepadCount = Gamepad.all.Count;
        if (gamepadCount > 1) {
            playerCount++;
            spawnPlayer();

        }
        EventManager.global.TriggerOnGamepadAdded(gamepadCount-1);
    }

    void onGamepadRemoved() {
        despawnPlayer();
        if (playerCount > 1) playerCount--;
        EventManager.global.TriggerOnGamepadRemoved(Gamepad.all.Count-1);
    }

    void spawnPlayer() {
        GameObject newObj = Instantiate(playerPrefab, playerSpawnpoint.position, Quaternion.identity);
        Player newPlayer = newObj.GetComponent<Player>();
        players.Add(newPlayer);
    }

    void despawnPlayer() {
        Destroy(players[playerCount - 1].gameObject);
    }
}