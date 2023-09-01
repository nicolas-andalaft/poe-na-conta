using System;

public class PlayerEvents {

    public event Action onMovePerformed;
    public void TriggerOnMovePerformed() {
        onMovePerformed?.Invoke();
    }

    public event Action onMoveCanceled;
    public void TriggerOnMoveCanceled() {
        onMoveCanceled?.Invoke();
    }

    public event Action onFirePerformed;
    public void TriggerOnFirePerformed() {
        onFirePerformed?.Invoke();
    }

    public event Action<Slot> onItemPickedUp;
    public void TriggerOnItemPickedUp(Slot slotContainer) {
        onItemPickedUp?.Invoke(slotContainer);
    }
}