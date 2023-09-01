using UnityEngine;

public enum SlotPackageType { Item, Client, Any }

public class Slot : MonoBehaviour {

    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject package;
    [SerializeField] GameObject destroyOnPickUp;
    [SerializeField] SlotPackageType packageType;

    public void setPackage(GameObject newPackage) {
        package = newPackage;
        package.transform.SetParent(transform, false);
    }

    public GameObject takePackage() {
        GameObject aux = package;
        package = null;
        if (destroyOnPickUp != null) {
            Destroy(destroyOnPickUp, 0.1f);
        }
        return aux;
    }

    public bool hasPackage() {
        return package != null;
    }

    public bool canBeTransferedTo(Slot destination) {
        bool isValid = false;

        switch (destination.packageType) {
            case SlotPackageType.Item:
                isValid = package.CompareTag("Item");
                break;
            case SlotPackageType.Client:
                isValid = package.CompareTag("Client");
                break;
            case SlotPackageType.Any:
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
