using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] int playerIndex;
    [SerializeField] float velocity = 1.0f;
    PlayerControls playerControls;
    bool shouldMove = false;
    Vector2 movement = new Vector2();

    void Awake() {
        playerControls = GetComponent<PlayerControls>();
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
            transform.Translate(new Vector3(movement.x, 0, movement.y) * Time.deltaTime * velocity);
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