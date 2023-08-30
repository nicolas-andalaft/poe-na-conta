using UnityEngine;

public class ItemHolder : MonoBehaviour {

    [SerializeField] Transform slotPosition;
    [SerializeField] GameObject item;
    [SerializeField] Renderer highlightRenderer;

    void OnEnable() {
        if (slotPosition == null) {
            enabled = false;
        }
    }

    public bool isEmpty() {
        return item == null;
    }

    public void placeItem(GameObject newItem) {
        item = newItem;
        item.transform.parent = slotPosition;
        item.transform.localPosition = Vector3.zero;
    }

    public GameObject takeOutItem() {
        GameObject aux = item;
        item = null;
        return aux;
    }

    public void highlightObject() {
        if (highlightRenderer != null) {
            highlightRenderer.material.color = new Color(0f, 0f, 1f, 1f);
        }
    }

    public void unhighlightObject() {
        if (highlightRenderer != null) {
            highlightRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
