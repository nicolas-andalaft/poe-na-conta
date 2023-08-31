using UnityEngine;

public class Slot : MonoBehaviour, IInteractable {

    [SerializeField] Transform slotPosition;
    [SerializeField] Renderer highlightRenderer;
    [SerializeField] Item item;

    void OnEnable() {
        if (slotPosition == null) {
            enabled = false;
        }
    }

    public void setItem(Item newItem) {
        item = newItem;
        item.transform.SetParent(slotPosition, false);
    }

    public Item takeItem() {
        Item aux = item;
        item = null;
        return aux;
    }

    public bool hasItem() {
        return item != null;
    }

    public void highlight() {
        if (highlightRenderer != null) {
            highlightRenderer.material.color = new Color(0f, 0f, 1f, 1f);
        }
    }

    public void unhighlight() {
        if (highlightRenderer != null) {
            highlightRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
