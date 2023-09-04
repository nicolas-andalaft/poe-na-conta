using UnityEngine;
using UnityEngine.Events;

public enum SlotPackageType { Item, Client, Any }

public class Slot : MonoBehaviour {

    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject package;
    [SerializeField] GameObject destroyOnPickUp;
    [SerializeField] SlotPackageType packageType;
    [SerializeField] UnityEvent onPackageDelivered;
    [SerializeField] bool input = true;
    [SerializeField] bool output = true;

    public void transferTo(Slot destination) {
        if (!output || !destination.input || !canBeTransferedTo(destination)) return;

        destination.setPackage(package);
        package = null;

        if (destroyOnPickUp != null) {
            Destroy(destroyOnPickUp, 0.1f);
        }
    }

    public void setPackage(GameObject newPackage) {
        package = newPackage;
        package.transform.SetParent(transform, false);
        onPackageDelivered.Invoke();
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
