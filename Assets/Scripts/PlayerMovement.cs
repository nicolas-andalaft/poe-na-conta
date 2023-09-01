using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] PlayerControls playerControls;
    [SerializeField] float velocity = 1.0f;
    Player player;
    CharacterController characterController;
    bool shouldMove = false;
    Vector2 movement = new Vector2();

    void Awake() {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
    }

    void OnEnable() {
        if (playerControls == null) {
            enabled = false;
        }
    }

    void Start() {
        EventManager.player(player.index).onMovePerformed += startMovement;
        EventManager.player(player.index).onMoveCanceled += stopMovement;
    }

    void OnDestroy() {
        EventManager.player(player.index).onMovePerformed -= startMovement;
        EventManager.player(player.index).onMoveCanceled -= stopMovement;
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