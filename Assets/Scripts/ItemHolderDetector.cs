using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemHolderDetector : MonoBehaviour {

    [SerializeField] private ItemHolder playerItemHolder;
    private List<ItemHolder> selectedItemHolders;

    private void Awake() {
        selectedItemHolders = new List<ItemHolder>();
    }

    private void OnTriggerEnter(Collider other) {
        ItemHolder holder = other.GetComponent<ItemHolder>();
        if (holder != null) {
            selectedItemHolders.Add(holder);
            holder.highlightObject();
        }
    }

    private void OnTriggerExit(Collider other) {
        ItemHolder holder = other.GetComponent<ItemHolder>();
        if (holder != null) {
            selectedItemHolders.Remove(holder);
            holder.unhighlightObject();
        }
    }

    public void onInteract() {
        if (selectedItemHolders.Count > 0) {
            if (playerItemHolder.isEmpty()) {
                playerItemHolder.placeItem(selectedItemHolders[0].takeOutItem());
            }
            else {
                selectedItemHolders[0].placeItem(playerItemHolder.takeOutItem());
            }
        }
    }
}