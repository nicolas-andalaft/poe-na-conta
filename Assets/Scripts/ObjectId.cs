using UnityEngine;

public class ObjectId : MonoBehaviour {

    public static int objectIdCount { get; private set; } = 0;
    public int id { get; private set; }

    [SerializeField] bool locked = true;

    void Awake() {
        id = objectIdCount;
        objectIdCount++;
    }

    public void setObjectId(int id) {
        if (!locked) this.id = id;
    }
}