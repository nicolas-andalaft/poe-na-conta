using System;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager current { get; private set; }

    void Awake() {
        current = this;
    }

    public event Action<int> onMovePerformed;
    public void TriggerOnMovePerformed(int param = 0) {
        onMovePerformed?.Invoke(param);
    }

    public event Action<int> onMoveCanceled;
    public void TriggerOnMoveCanceled(int param = 0) {
        onMoveCanceled?.Invoke(param);
    }

    public event Action<int> onFirePerformed;
    public void TriggerOnFirePerformed(int param = 0) {
        onFirePerformed?.Invoke(param);
    }
}