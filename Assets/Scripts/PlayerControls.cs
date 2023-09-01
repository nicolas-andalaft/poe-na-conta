using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerControls : MonoBehaviour {

    [SerializeField] InputActionAsset inputActions;
    Player player;
    InputActionMap actionMap;
    InputAction moveAction;
    InputAction fireAction;

    void Awake() {
        if (inputActions == null) {
            enabled = false;
            return;
        }
        player = GetComponent<Player>();

        actionMap = inputActions.FindActionMap("Player").Clone();
        actionMap.bindingMask = new InputBinding {groups = player.index == 0 ? "Keyboard&Mouse;Gamepad" : "Gamepad"};
        if (Gamepad.all.Any()) {
            assignGamepadToPlayer(Gamepad.all.Count-1);
        }
    }

    void OnEnable() {
        EventManager.global.onGamepadAdded += onGamepadAdded;

        moveAction = actionMap.FindAction("Move");
        moveAction.Enable();
        moveAction.performed += onMovePerformed;
        moveAction.canceled += onMoveCanceled;

        fireAction = actionMap.FindAction("Fire");
        fireAction.Enable();
        fireAction.performed += onFirePerformed;
    }

    void OnDisable() {
        moveAction?.Disable();
        fireAction?.Disable();
    }

    void onGamepadAdded(int gamepadIndex) {
        if (gamepadIndex != player.index) return;

        assignGamepadToPlayer(gamepadIndex);
    }

    void onMovePerformed(InputAction.CallbackContext context) {
        EventManager.player(player.index).TriggerOnMovePerformed();
    }

    void onMoveCanceled(InputAction.CallbackContext context) {
        EventManager.player(player.index).TriggerOnMoveCanceled();
    }

    void onFirePerformed(InputAction.CallbackContext context) {
        EventManager.player(player.index).TriggerOnFirePerformed();
    }

    public InputAction getMoveAction() {
        return moveAction;
    }

    public void assignGamepadToPlayer(int gamepadIndex) {
        actionMap.ApplyBindingOverridesOnMatchingControls(Gamepad.all[gamepadIndex]);
    }
}
