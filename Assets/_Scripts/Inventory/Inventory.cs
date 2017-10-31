using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

#region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public bool canEquipfromChest;
    public int inventorySpace = 8;

    //Items in the players inventory
    //public List<Item> items = new List<Item>();
    public List<Item> allItems = new List<Item>();

    public List<Item> weapons = new List<Item>();
    public List<Item> armor = new List<Item>();
    public List<Item> items = new List<Item>();
    Item[] curItem;

    public Item equipmentItem;



    public bool AddItem (Item item) {
        
        if (item != null){
            int slotIndex = (int)item.itemType;
            if (!item.isDefaultItem) { 
                //If a weapon
                if(slotIndex == 0){
                    if(weapons.Count >= inventorySpace){
                        Debug.Log("Not enough room in Inventory!");
                        return false;
                    }

                    weapons.Add(item);
                }
                //Armor
                else if(slotIndex == 1){
                    if (armor.Count >= inventorySpace){
                        Debug.Log("Not enough room in Inventory!");
                        return false;
                    }

                    armor.Add(item);
                }
                //Item
                else if (slotIndex == 2){
                    if (items.Count >= inventorySpace){
                        Debug.Log("Not enough room in Inventory!");
                        return false;
                    }

                    items.Add(item);
                }

                allItems.Add(item);
                
                //Call event AKA delegate. So everytime a change is made in the inventory this event is called. 
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
        }
        else if(item == null)
        {
            
            return false;
        }
       

        return true;
        
    }

    

    public void RemoveItem(Item item)
    {
        item.Remove();
        int slotIndex = (int)item.itemType;
        allItems.Remove(item);
        switch (slotIndex){
            case 0:
                weapons.Remove(item);
                break;
            case 1:
                armor.Remove(item);
                break;
            case 2:
                items.Remove(item);
                break;
        }
        //Drop item to ground
        GameObject newItemObject = Instantiate<GameObject>(item.itemObject);
        newItemObject.transform.parent = null;
        newItemObject.transform.position = PlayerManager.instance.player.transform.position;



        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
