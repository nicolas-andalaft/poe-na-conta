public interface IInteractable {

    public void setItem(Item newItem);

    public Item takeItem();

    public bool hasItem();

    public void highlight();

    public void unhighlight();
}
