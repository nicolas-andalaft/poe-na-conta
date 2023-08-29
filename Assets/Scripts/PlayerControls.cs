using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerControls : MonoBehaviour {

    private PlayerMovement playerMovement;
    private PlayerInputActions playerInputActions;
    private InputAction moveAction;
    private InputAction fireAction;

    void OnEnable() {
        moveAction = playerInputActions.Player.Move;
        moveAction.Enable();
        moveAction.performed += _ => playerMovement.startMovement();
        moveAction.canceled += _ => playerMovement.stopMovement();

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
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnFirePerformed(InputAction.CallbackContext context) {
        Debug.Log("fire");
    }

    public InputAction getMoveAction() {
        return moveAction;
    }
}
