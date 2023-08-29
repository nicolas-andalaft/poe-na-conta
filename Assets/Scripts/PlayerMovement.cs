using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float velocity = 1.0f;
    private PlayerControls playerControls;
    private bool shouldMove = false;
    private Vector2 movement = new Vector2();

    void Start() {
        playerControls = GetComponent<PlayerControls>();
    }

    void Update() {
        if (shouldMove) {
            movement = playerControls.getMoveAction().ReadValue<Vector2>();
            transform.Translate(new Vector3(movement.x, 0, movement.y) * Time.deltaTime * velocity);
        }
    }

    public void startMovement() {
        shouldMove = true;
    }

    public void stopMovement() {
        shouldMove = false;
    }
}