using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlotDetector : MonoBehaviour {

    [SerializeField] int playerIndex;
    [SerializeField] Slot playerSlot;
    [SerializeField] List<Slot> possibleSlots;
    [SerializeField] Slot selectedSlot;
    float currDistToSlot;
    bool shouldCheckDistance = false;
    float checkRefreshTime = 0.1f;

    void Awake() {
        possibleSlots = new List<Slot>();
    }

    void OnEnable() {
        if (playerSlot == null) {
            enabled = false;
        }
    }

    void Start() {
        EventManager.current.onFirePerformed += onInteract;
        EventManager.current.onMovePerformed += startCheck;
        EventManager.current.onMoveCanceled += stopCheck;
    }

    void OnDestroy() {
        StopAllCoroutines();
        EventManager.current.onFirePerformed -= onInteract;
        EventManager.current.onMovePerformed -= startCheck;
        EventManager.current.onMoveCanceled -= stopCheck;
    }

    void OnTriggerEnter(Collider other) {
        Slot slot = other.GetComponent<Slot>();
        if (slot == null) return;

        possibleSlots.Add(slot);
        if (selectedSlot == null) {
            currDistToSlot = Vector3.Distance(transform.position, other.transform.position);
            selectSlot(slot);
        }
        else {
            selectIfCloser(slot);
        }
    }

    void OnTriggerExit(Collider other) {
        Slot slot = other.GetComponent<Slot>();
        if (slot == null) return;

        possibleSlots.Remove(slot);
        if (selectedSlot == slot) {
            selectSlot(possibleSlots.FirstOrDefault());
        }
    }

    void startCheck(int playerIndex) {
        if (playerIndex != this.playerIndex) return;
        setDistanceCheck(true);
    }

    void stopCheck(int playerIndex) {
        if (playerIndex != this.playerIndex) return;
        setDistanceCheck(false);
    }

    void setDistanceCheck(bool value) {
        if (shouldCheckDistance == value) return;
        shouldCheckDistance = value;
        if (value) {
            StartCoroutine(checkObjectsDistance());
        }
        else {
            StopCoroutine(checkObjectsDistance());
        }
    }

    void selectSlot(Slot slot) {
        selectedSlot?.unhighlight();
        selectedSlot = slot;
        selectedSlot?.highlight();
        if (selectedSlot == null) {
            currDistToSlot = float.MaxValue;
        }
    }

    void selectIfCloser(Slot slot) {
        float dist = Vector3.Distance(transform.position, slot.transform.position);
        if (slot == selectedSlot) {
            currDistToSlot = dist;
        }
        if (currDistToSlot > dist) {
            currDistToSlot = dist;
            selectSlot(slot);
        }
    }

    IEnumerator checkObjectsDistance() {
        while (shouldCheckDistance) {
            for (int i = 0; i < possibleSlots.Count; i++) {
                if (possibleSlots[i] == null) {
                    possibleSlots.RemoveAt(i);
                }
                else {
                    selectIfCloser(possibleSlots[i]);
                }
            }
            yield return new WaitForSeconds(checkRefreshTime);
        }
    }

    void onInteract(int playerIndex) {
        if (playerIndex != this.playerIndex || !possibleSlots.Any()) return;

        if (playerSlot.hasItem() && !selectedSlot.hasItem()) {
            selectedSlot.setItem(playerSlot.takeItem());
        }
        else if (!playerSlot.hasItem() && selectedSlot.hasItem()) {
            playerSlot.setItem(selectedSlot.takeItem());
        }
    }
}