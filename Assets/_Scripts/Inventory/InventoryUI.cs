using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    #region Singleton
    public static InventoryUI instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject inventoryUI;
    public GameObject inventoryItemInfoUI;
    public Text inventoryItemInfoName;
    public Text inventoryItemInfoDescription;
    public Image inventoryItemInfoIcon;
    public GameObject inventoryItemInfoStats;
    public GameObject inventoryOptionsMenu, unequipOptionsButton, dropOptionsButton, cancelOptionsButton;

    public GameObject weaponInventory, armorInventory, itemInventory;

    public Item uiItem;

    [HideInInspector]
    public bool canOpenInventory;
    Inventory inventory;
    bool pauseGame;

    InventorySlot[] weaponSlots;
    InventorySlot[] armorSlots;
    InventorySlot[] itemSlots;

    // Use this for initialization
    void Start () {
        inventory = Inventory.instance;
        canOpenInventory = true;
        //UpdateUI called whenever onItemChangedCallback is called. Which is called whenever an Item is added or removed from the inventory
        inventory.onItemChangedCallback += UpdateUI;

        weaponSlots = weaponInventory.GetComponentsInChildren<InventorySlot>();
        armorSlots = armorInventory.GetComponentsInChildren<InventorySlot>();
        itemSlots = itemInventory.GetComponentsInChildren<InventorySlot>();
    }
	
	// Update is called once per frame
	void Update () {
        if (canOpenInventory) {
            if (Input.GetButtonDown("Inventory")) {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                if(inventoryUI.activeSelf == false) {
                    if (inventoryItemInfoUI.activeSelf == true) {
                        inventoryItemInfoUI.SetActive(false);
                    }
                }
                
                pauseGame = TogglePause();
                
                
            }
        }
        
	}

    bool TogglePause() {
        if (Time.timeScale == 0f){
            Time.timeScale = 1f;
            return (false);
        }
        else{
            Time.timeScale = 0f;
            return (true);
        }
    }

    public void UnequipOption(){
        if(uiItem!= null){
            uiItem.Remove();
            inventoryOptionsMenu.SetActive(!inventoryOptionsMenu.activeSelf);
        }
    }

    public void DropOption(){
        if(uiItem!= null){
            inventory.RemoveItem(uiItem);
            inventoryOptionsMenu.SetActive(!inventoryOptionsMenu.activeSelf);
        }

    }

    public void CancelOption(){
        inventoryOptionsMenu.SetActive(!inventoryOptionsMenu.activeSelf);
    }

    void UpdateUI(){
        for (int i = 0; i < weaponSlots.Length; i++){
            if(i < inventory.weapons.Count){
                weaponSlots[i].AddItem(inventory.weapons[i]);
            }
            else{
                weaponSlots[i].ClearSlot();
            }
        }

        for (int i = 0; i < armorSlots.Length; i++){
            if (i < inventory.armor.Count){
                armorSlots[i].AddItem(inventory.armor[i]);
            }
            else{
                armorSlots[i].ClearSlot();
            }
        }

        for (int i = 0; i < itemSlots.Length; i++){
            if (i < inventory.items.Count){
                itemSlots[i].AddItem(inventory.items[i]);
            }
            else{
                itemSlots[i].ClearSlot();
            }
        }
    }
}
