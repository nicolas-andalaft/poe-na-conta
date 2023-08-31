using UnityEngine;

public class Slot : MonoBehaviour {

    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject item;
    [SerializeField] GameObject destroyOnPickUp;

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
