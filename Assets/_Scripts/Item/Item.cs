using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item ")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public string description = "Description";
    public ItemType itemType;
    public GameObject itemObject;

    public virtual void Use()
    {
        //Use Item

        Debug.Log("Using: " + name);
    }

    public virtual void Remove()
    {
        // Remove Item
    }

    public virtual int GetStats(int j)
    {
        //Get Equipment Stats

        return j;
    }

    public enum ItemType
    {
        WEAPON,
        ARMOR,
        ITEM

    }

}
