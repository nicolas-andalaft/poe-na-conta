using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ItemHolder))]
public class ItemHolderDetector : MonoBehaviour {

    [SerializeField] int playerIndex;
    ItemHolder playerItemHolder;
    List<ItemHolder> selectedItemHolders;

    void Awake() {
        selectedItemHolders = new List<ItemHolder>();
        playerItemHolder = GetComponent<ItemHolder>();
    }

    void Start() {
        EventManager.current.onFirePerformed += onInteract;
    }

    void OnDestroy() {
        EventManager.current.onFirePerformed -= onInteract;
    }

    void OnTriggerEnter(Collider other) {
        ItemHolder holder = other.GetComponent<ItemHolder>();
        if (holder != null) {
            if (holder.gameObject.tag != "Player" || !holder.isEmpty()) {
                selectedItemHolders.Add(holder);
                holder.highlightObject();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        ItemHolder holder = other.GetComponent<ItemHolder>();
        if (holder != null) {
            selectedItemHolders.Remove(holder);
            holder.unhighlightObject();
        }
    }

    public void onInteract(int playerIndex) {
        if (playerIndex == this.playerIndex && selectedItemHolders.Any()) {
            if (playerItemHolder.isEmpty()) {
                playerItemHolder.placeItem(selectedItemHolders[0].takeOutItem());
            }
            else {
                selectedItemHolders[0].placeItem(playerItemHolder.takeOutItem());
            }
        }
    }
}