using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {


    public Image icon;


    Item item;
    Equipment equipment;

    InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = InventoryUI.instance;
    }


    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

    public void OnHover()
    {   
        if(ChestUI.instance.isInChest != true)
        {
            if (Chest.instance != null)
            {
                if (Chest.instance.hasChestInteracted == false)
                {
                    if (item != null)
                    {
                        Stats();
                        inventoryUI.inventoryItemInfoName.text = item.name;
                        inventoryUI.inventoryItemInfoDescription.text = item.description;
                        inventoryUI.inventoryItemInfoIcon.sprite = item.icon;
                    }

                    inventoryUI.inventoryItemInfoUI.SetActive(!inventoryUI.inventoryItemInfoUI.activeSelf);

                }
            }
            else
            {
                if (item != null)
                {
                    Stats();
                    inventoryUI.inventoryItemInfoName.text = item.name;
                    inventoryUI.inventoryItemInfoDescription.text = item.description;
                    inventoryUI.inventoryItemInfoIcon.sprite = item.icon;
                }

                inventoryUI.inventoryItemInfoUI.SetActive(!inventoryUI.inventoryItemInfoUI.activeSelf);

            }
        }
        
           
    }
    public void Stats()
    {
        /*
        foreach(Text statText in inventoryItemInfoStats.GetComponentsInChildren<Text>())
        {
            Debug.Log(statText.text);
        }
        */
        
        Transform gameObjectSlot;
        int damageStat;
        int armorStat;
        int bonusStat;

        
        damageStat = item.GetStats(0);
        armorStat = item.GetStats(1);
        bonusStat = item.GetStats(2);

        //There exists a damage value
        if(damageStat > 0)
        {
            //Damage value is slot 0
            gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(0);
            gameObjectSlot.GetComponentInChildren<Text>().text = damageStat.ToString();
            gameObjectSlot.gameObject.SetActive(true);
            //Icon for child(0) is the damage icon....

            //There exists an armor value
            if (armorStat > 0)
            {
                //Armor value is slot 1
                gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(1);
                gameObjectSlot.GetComponentInChildren<Text>().text = armorStat.ToString();
                gameObjectSlot.gameObject.SetActive(true);

                //There exists a bonus stat
                if (bonusStat  > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(2);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();
                    gameObjectSlot.gameObject.SetActive(true);
                }

                
            }
            //If no armor value exists
            else
            {
                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(1);
                    gameObjectSlot.gameObject.SetActive(true);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();

                    //Only 2 stats
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
            }



        }
        //There is no damage value
        else
        {
            //There exists an armor value
            if (armorStat > 0)
            {
                //Armor value is slot 0
                gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(0);
                gameObjectSlot.gameObject.SetActive(true);
                gameObjectSlot.GetComponentInChildren<Text>().text = armorStat.ToString();

                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(1);
                    gameObjectSlot.gameObject.SetActive(true);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();

                    inventoryUI.inventoryItemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }


            }
            //There doesn't exist an armor value
            else
            {
                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = inventoryUI.inventoryItemInfoStats.transform.GetChild(0);
                    gameObjectSlot.gameObject.SetActive(true);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(0).gameObject.SetActive(false);
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    inventoryUI.inventoryItemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        
            
        
    }

}
