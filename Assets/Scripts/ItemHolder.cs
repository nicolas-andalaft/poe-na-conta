using UnityEngine;

public class ItemHolder : MonoBehaviour {

    [SerializeField] private Transform slotPosition;
    [SerializeField] private GameObject item;

    void OnEnable() {
        if (slotPosition == null) {
            enabled = false;
        }
    }

    public void placeItem(GameObject newItem) {
        item = newItem;
        item.transform.parent = slotPosition;
        item.transform.localPosition = Vector3.zero;
    }

    public GameObject takeItem() {
        GameObject aux = item;
        item = null;
        return aux;
    }
}
