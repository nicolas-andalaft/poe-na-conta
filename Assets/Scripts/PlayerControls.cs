using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

    [SerializeField] InputActionAsset inputActions;
    [SerializeField] int playerIndex;
    InputActionMap actionMap;
    InputAction moveAction;
    InputAction fireAction;

    void Awake() {
        if (inputActions == null) {
            enabled = false;
            return;
        }
        actionMap = inputActions.FindActionMap("Player").Clone();
        actionMap.bindingMask = new InputBinding {groups = playerIndex == 0 ? "Keyboard&Mouse;Gamepad" : "Gamepad"};
        actionMap.ApplyBindingOverridesOnMatchingControls(Gamepad.all[playerIndex]);
    }

    void OnEnable() {
        moveAction = actionMap.FindAction("Move");
        moveAction.Enable();
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

        fireAction = actionMap.FindAction("Fire");
        fireAction.Enable();
        fireAction.performed += OnFirePerformed;
    }

    void OnDisable() {
        moveAction.Disable();
        fireAction.Disable();
    }

    void OnMovePerformed(InputAction.CallbackContext context) {
        EventManager.player(playerIndex).TriggerOnMovePerformed();
    }

    void OnMoveCanceled(InputAction.CallbackContext context) {
        EventManager.player(playerIndex).TriggerOnMoveCanceled();
    }

    void OnFirePerformed(InputAction.CallbackContext context) {
        EventManager.player(playerIndex).TriggerOnFirePerformed();
    }

    public InputAction getMoveAction() {
        return moveAction;
    }
}
