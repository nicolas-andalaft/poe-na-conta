using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] int playerIndex;
    [SerializeField] float velocity = 1.0f;
    [SerializeField] PlayerControls playerControls;
    CharacterController characterController;
    bool shouldMove = false;
    Vector2 movement = new Vector2();

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    void OnEnable() {
        if (playerControls == null) {
            enabled = false;
        }
    }

    void Start() {
        EventManager.current.onMovePerformed += startMovement;
        EventManager.current.onMoveCanceled += stopMovement;
    }

    void OnDestroy() {
        EventManager.current.onMovePerformed -= startMovement;
        EventManager.current.onMoveCanceled -= stopMovement;
    }

    void Update() {
        if (shouldMove) {
            movement = playerControls.getMoveAction().ReadValue<Vector2>();
            characterController.Move(new Vector3(movement.x, 0, movement.y) * Time.deltaTime * velocity);
        }
    }

    public void startMovement(int playerIndex) {
        if (playerIndex == this.playerIndex) {
            shouldMove = true;
        }
    }

    public void stopMovement(int playerIndex) {
        if (playerIndex == this.playerIndex) {
            shouldMove = false;
        }
    }
}