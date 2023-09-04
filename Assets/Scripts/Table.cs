using System.Collections;
using UnityEngine;

public class Table : MonoBehaviour {

    public static int tableCount { get; private set; } = 0;
    public int id { get; private set; }
    [SerializeField] Slot slot;
    [SerializeField] GameObject orderPrefab;
    float ORDER_TIME = 5;

    void Awake() {
        id = tableCount;
        tableCount++;
    }

    public void startOrderTimer() {
        StartCoroutine(orderTimer());
    }

    void makeOrder() {
        GameObject newOrder = Instantiate(orderPrefab);
        slot.setPackage(newOrder);
    }

    IEnumerator orderTimer() {
        yield return new WaitForSeconds(ORDER_TIME);
        makeOrder();
    }
}