using System.Collections;
using UnityEngine;

public class ClientLineManager : MonoBehaviour {

    [SerializeField] Transform clientsContainer;
    [SerializeField] GameObject clientPrefab;
    [SerializeField] float spawnInterval;
    [SerializeField] int spawnLimit;
    [SerializeField] bool isRunning = true;
    float clientMargin = 1.5f;

    void Start() {
        StartCoroutine(startSpawnCycle());
    }

    void OnDestroy() {
        StopAllCoroutines();
    }

    void createClient() {
        Vector3 position = clientsContainer.position +
            (-clientsContainer.forward * clientMargin * clientsContainer.childCount);
        Instantiate(clientPrefab, position, Quaternion.identity, clientsContainer);

        if (clientsContainer.childCount >= spawnLimit) {
            isRunning = false;
        }
    }

    IEnumerator startSpawnCycle() {
        while (isRunning) {
            createClient();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}