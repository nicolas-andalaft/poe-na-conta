using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] PlayerControls playerControls;
    [SerializeField] float velocity = 1.0f;
    [SerializeField] int playerIndex;
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
        EventManager.player(playerIndex).onMovePerformed += startMovement;
        EventManager.player(playerIndex).onMoveCanceled += stopMovement;
    }

    void OnDestroy() {
        EventManager.player(playerIndex).onMovePerformed -= startMovement;
        EventManager.player(playerIndex).onMoveCanceled -= stopMovement;
    }

    void Update() {
        if (shouldMove) {
            movement = playerControls.getMoveAction().ReadValue<Vector2>();
            characterController.Move(new Vector3(movement.x, 0, movement.y) * Time.deltaTime * velocity);
        }
    }

    public void startMovement() {
        shouldMove = true;
    }

    public void stopMovement() {
        shouldMove = false;
    }
}