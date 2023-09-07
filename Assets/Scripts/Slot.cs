using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public enum SlotPackageType { Item, Food, Order, Client, Any }

public class Slot : MonoBehaviour {

    static List<Material> highlightedMaterialList;

    [SerializeField] Material highlightMaterial;
    [SerializeField] Renderer highlightRenderer;
    [SerializeField] GameObject package;
    [SerializeField] SlotPackageType packageType;
    [SerializeField] UnityEvent onPackageDelivered;
    [SerializeField] UnityEvent onPackageArrived;
    [SerializeField] bool input = true;
    [SerializeField] bool output = true;

    public void transferTo(Slot destination) {
        if (!output || !destination.input || !canBeTransferedTo(destination)) return;

        destination.setPackage(package);
        package = null;

        onPackageDelivered.Invoke();
        destination.onPackageArrived.Invoke();
    }

    public void setPackage(GameObject newPackage) {
        if (newPackage == null) return;
        package = newPackage;
        package.transform.SetParent(transform, false);
    }

    public void destroyPackage() {
        if (package != null) {
            Destroy(package.gameObject);
        }
        package = null;
    }

    public void destroySelf() {
        Destroy(gameObject);
    }

    public bool hasPackage() {
        return package != null;
    }

    public bool canBeTransferedTo(Slot destination) {
        bool isValid = false;

        switch (destination.packageType) {
            case SlotPackageType.Food:
                isValid = package.CompareTag("Food");
                break;
            case SlotPackageType.Order:
                isValid = package.CompareTag("Order");
                break;
            case SlotPackageType.Client:
                isValid = package.CompareTag("Client");
                break;
            case SlotPackageType.Item:
                isValid = !package.CompareTag("Client");
                break;
            case SlotPackageType.Any:
                isValid = true;
                break;
        }
        return isValid;
    }

    public void setInputLock(bool value) {
        input = value;
    }

    public void setOutputLock(bool value) {
        output = value;
    }

    public void highlight() {
        if (highlightRenderer != null) {
            highlightRenderer.material = highlightMaterial;
        }
    }

    public void unhighlight() {
        if (highlightRenderer != null) {
            highlightRenderer.material = GraphicsSettings.defaultRenderPipeline.defaultMaterial;
        }
    }
}
