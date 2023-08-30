using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

    PlayerInputActions playerInputActions;
    InputAction moveAction;
    InputAction fireAction;

    void OnEnable() {
        moveAction = playerInputActions.Player.Move;
        moveAction.Enable();
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

        fireAction = playerInputActions.Player.Fire;
        fireAction.Enable();
        fireAction.performed += OnFirePerformed;
    }

    void OnDisable() {
        moveAction.Disable();
        fireAction.Disable();
    }

    void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    void OnMovePerformed(InputAction.CallbackContext context) {
        EventManager.current.TriggerOnMovePerformed();
    }

    void OnMoveCanceled(InputAction.CallbackContext context) {
        EventManager.current.TriggerOnMoveCanceled();
    }

    void OnFirePerformed(InputAction.CallbackContext context) {
        EventManager.current.TriggerOnFirePerformed();
    }

    public InputAction getMoveAction() {
        return moveAction;
    }
}
