using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ItemHolder))]
public class ItemHolderDetector : MonoBehaviour {

    [SerializeField] int playerIndex;
    ItemHolder playerItemHolder;
    List<ItemHolder> possibleHolders;
    ItemHolder selectedHolder;
    float currDistToHolder;
    bool shouldCheckDistance = false;
    float checkRefreshTime = 0.1f;

    void Awake() {
        possibleHolders = new List<ItemHolder>();
        playerItemHolder = GetComponent<ItemHolder>();
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
        ItemHolder holder = other.GetComponent<ItemHolder>();
        if (holder == null) return;

        possibleHolders.Add(holder);
        if (selectedHolder == null) {
            currDistToHolder = Vector3.Distance(transform.position, holder.transform.position);
            selectHolder(holder);
        }
        else {
            selectIfCloser(holder);
        }
    }

    void OnTriggerExit(Collider other) {
        ItemHolder holder = other.GetComponent<ItemHolder>();
        if (holder == null) return;

        possibleHolders.Remove(holder);
        if (selectedHolder == holder) {
            selectHolder(possibleHolders.FirstOrDefault());
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
            StartCoroutine(checkHoldersDistance());
        }
        else {
            StopCoroutine(checkHoldersDistance());
        }
    }

    IEnumerator checkHoldersDistance() {
        while (shouldCheckDistance) {
            foreach (var holder in possibleHolders) {
                selectIfCloser(holder);
            }
            yield return new WaitForSeconds(checkRefreshTime);
        }
    }

    void selectHolder(ItemHolder holder) {
        selectedHolder?.unhighlightObject();
        selectedHolder = holder;
        selectedHolder?.highlightObject();
        if (selectedHolder == null) {
            currDistToHolder = float.MaxValue;
        }
    }

    void selectIfCloser(ItemHolder holder) {
        float dist = Vector3.Distance(transform.position, holder.transform.position);
        if (holder == selectedHolder) {
            currDistToHolder = dist;
        }
        if (currDistToHolder > dist) {
            currDistToHolder = dist;
            selectHolder(holder);
        }
    }

    void onInteract(int playerIndex) {
        if (playerIndex == this.playerIndex && possibleHolders.Any()) {
            if (playerItemHolder.isEmpty() && !selectedHolder.isEmpty()) {
                playerItemHolder.placeItem(selectedHolder.takeOutItem());
            }
            else if (!playerItemHolder.isEmpty() && selectedHolder.isEmpty()) {
                selectedHolder.placeItem(playerItemHolder.takeOutItem());
            }
        }
    }
}