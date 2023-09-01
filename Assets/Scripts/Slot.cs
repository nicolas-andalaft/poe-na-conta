using UnityEngine;

public enum SlotContentType { Item, Client, Any }

public class Slot : MonoBehaviour {

    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject item;
    [SerializeField] GameObject destroyOnPickUp;
    [SerializeField] SlotContentType contentType;

    public void setItem(GameObject newItem) {
        item = newItem;
        item.transform.SetParent(transform, false);
    }

    public GameObject takeItem() {
        GameObject aux = item;
        item = null;
        if (destroyOnPickUp != null) {
            Destroy(destroyOnPickUp, 0.1f);
        }
        return aux;
    }

    public bool hasItem() {
        return item != null;
    }

    public bool canTransferTo(Slot destination) {
        bool isValid = false;

        switch (destination.contentType) {
            case SlotContentType.Item:
                isValid = item.CompareTag("Item");
                break;
            case SlotContentType.Client:
                isValid = item.CompareTag("Client");
                break;
            case SlotContentType.Any:
                isValid = true;
                break;
        }
        return isValid;
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
