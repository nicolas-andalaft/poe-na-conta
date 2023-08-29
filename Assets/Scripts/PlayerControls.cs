using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {
    [SerializeField] private float velocity = 1.0f;
    private Vector2 movement = new Vector2();
    private PlayerInputActions playerInputActions;
    private InputAction moveAction;
    private InputAction fireAction;

    void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    void Update() {
        movement = moveAction.ReadValue<Vector2>();
        if (movement != Vector2.zero) {
            transform.Translate(new Vector3(movement.x, 0, movement.y) * Time.deltaTime * velocity);
        }
    }

    void OnEnable() {
        moveAction = playerInputActions.Player.Move;
        moveAction.Enable();

        fireAction = playerInputActions.Player.Fire;
        fireAction.Enable();
        fireAction.performed += OnFire;
    }

    void OnDisable() {
        moveAction.Disable();
        fireAction.Disable();
    }

    void OnFire(InputAction.CallbackContext context) {
        Debug.Log("fire");
    }
}
