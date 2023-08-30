using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ItemHolderDetector))]
public class PlayerControls : MonoBehaviour {

    private PlayerMovement playerMovement;
    private ItemHolderDetector itemHolderDetector;
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
        fireAction.performed += _ => itemHolderDetector.onInteract();
    }

    void OnDisable() {
        moveAction.Disable();
        fireAction.Disable();
    }

    void Awake() {
        playerInputActions = new PlayerInputActions();
        playerMovement = GetComponent<PlayerMovement>();
        itemHolderDetector = GetComponent<ItemHolderDetector>();
    }

    public InputAction getMoveAction() {
        return moveAction;
    }
}
