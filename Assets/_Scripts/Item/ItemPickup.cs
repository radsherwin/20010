using UnityEngine;

public class ItemPickup : Interactables {

    public Item item;

    public override void Interact()
    {
        base.Interact();
        
        PickUp();
    }

    void PickUp()
    {

        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
