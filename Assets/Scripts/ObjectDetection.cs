using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectDetection<T> where T: MonoBehaviour, IInteractable {

    public T selectedObject { get; private set; }
    Action onStartCheck;
    Action onStopCheck;
    Transform transform;
    List<T> possibleObjects;
    float currDistToObject;
    bool shouldCheckDistance = false;
    float checkRefreshTime = 0.1f;

    public ObjectDetection(Transform transform, Action onStartCheck, Action onStopCheck) {
        this.transform = transform;
        this.onStartCheck = onStartCheck;
        this.onStopCheck = onStopCheck;
        possibleObjects = new List<T>();
    }

    void selectObject(T obj) {
        selectedObject?.unhighlight();
        selectedObject = obj;
        selectedObject?.highlight();
        if (selectedObject == null) {
            currDistToObject = float.MaxValue;
        }
    }

    void selectIfCloser(T obj) {
        float dist = Vector3.Distance(transform.position, obj.transform.position);
        if (obj == selectedObject) {
            currDistToObject = dist;
        }
        if (currDistToObject > dist) {
            currDistToObject = dist;
            selectObject(obj);
        }
    }

    public bool hasPossibleObjects() {
        return possibleObjects.Any();
    }

    public void addPossibleObject(GameObject newObj) {
        T obj = newObj.GetComponent<T>();
        if (obj == null) return;

        possibleObjects.Add(obj);
        if (selectedObject == null) {
            currDistToObject = Vector3.Distance(transform.position, obj.transform.position);
            selectObject(obj);
        }
        else {
            selectIfCloser(obj);
        }
    }

    public void removePossibleObject(GameObject newObj) {
        T obj = newObj.GetComponent<T>();
        if (obj == null) return;

        possibleObjects.Remove(obj);
        if (selectedObject == obj) {
            selectObject(possibleObjects.FirstOrDefault());
        }
    }

    public void setDistanceCheck(bool value) {
        if (shouldCheckDistance == value) return;
        shouldCheckDistance = value;
        if (value) {
            onStartCheck?.Invoke();
        }
        else {
            onStopCheck?.Invoke();
        }
    }

    public IEnumerator checkObjectsDistance() {
        while (shouldCheckDistance) {
            foreach (var obj in possibleObjects) {
                selectIfCloser(obj);
            }
            yield return new WaitForSeconds(checkRefreshTime);
        }
    }
}