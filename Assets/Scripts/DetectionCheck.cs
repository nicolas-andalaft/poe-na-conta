using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DetectionCheck : MonoBehaviour {

    [SerializeField] int playerIndex;
    [SerializeField] Slot playerSlot;
    ObjectDetection<Slot> slotDetection;

    void Awake() {
        slotDetection = new ObjectDetection<Slot>(transform, onStartSlotCheck, onStopSlotCheck);
    }

    void Start() {
        EventManager.current.onFirePerformed += onInteract;
        EventManager.current.onMovePerformed += startSlotCheck;
        EventManager.current.onMoveCanceled += stopSlotCheck;
    }

    void OnDestroy() {
        StopAllCoroutines();
        EventManager.current.onFirePerformed -= onInteract;
        EventManager.current.onMovePerformed -= startSlotCheck;
        EventManager.current.onMoveCanceled -= stopSlotCheck;
    }

    void OnTriggerEnter(Collider other) {
        slotDetection.addPossibleObject(other.gameObject);
    }

    void OnTriggerExit(Collider other) {
        slotDetection.removePossibleObject(other.gameObject);
    }

    void startSlotCheck(int playerIndex) {
        if (playerIndex != this.playerIndex) return;
        slotDetection.setDistanceCheck(true);
    }

    void stopSlotCheck(int playerIndex) {
        if (playerIndex != this.playerIndex) return;
        slotDetection.setDistanceCheck(false);
    }

    void onStartSlotCheck() {
        StartCoroutine(slotDetection.checkObjectsDistance());
    }

    void onStopSlotCheck() {
        StopCoroutine(slotDetection.checkObjectsDistance());
    }

    void onInteract(int playerIndex) {
        if (playerIndex != this.playerIndex || !slotDetection.hasPossibleObjects()) return;

        if (!playerSlot.hasItem() && slotDetection.selectedObject.hasItem()) {
            playerSlot.setItem(slotDetection.selectedObject.takeItem());
        }
        else if (playerSlot.hasItem() && !slotDetection.selectedObject.hasItem()) {
            slotDetection.selectedObject.setItem(playerSlot.takeItem());
        }
    }
}