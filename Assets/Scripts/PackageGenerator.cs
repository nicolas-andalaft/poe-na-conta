using System.Collections;
using UnityEngine;

public class PackageGenerator : MonoBehaviour {

    [SerializeField] Slot slot;
    [SerializeField] GameObject packagePrefab;
    [SerializeField] float generationTime;
    [SerializeField] ObjectId packageId;

    public void startPackageTimer() {
        StartCoroutine(packageTimer());
    }

    void generatePackage() {
        GameObject newObject = Instantiate(packagePrefab);

        if (packageId != null) {
            setPackageId(newObject);
        }

        slot.setPackage(newObject);
    }

    IEnumerator packageTimer() {
        slot.setInputLock(false);
        yield return new WaitForSeconds(generationTime);
        generatePackage();
        slot.setInputLock(true);
    }

    void setPackageId(GameObject package) {
        package.GetComponent<ObjectId>().setObjectId(packageId.id);
    }
}