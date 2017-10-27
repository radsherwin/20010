using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour {

    #region Singleton
    public static ChestUI instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject inventoryUI;
    public GameObject itemInfoUI;
    public Image iconInfoUI;
    public Text titleofWeapon;
    public Text descriptionText;
    public GameObject itemInfoStats;

    

    public Button equipButton;
    public bool isInChest;

    Inventory inventory;
    Chest chest;
    bool pauseGame;


    // Use this for initialization
    void Start()
    {
        isInChest = false;
        inventory = Inventory.instance;
        chest = Chest.instance;

        //UpdateUI called whenever onItemChangedCallback is called. Which is called whenever an Item is added or removed from the inventory
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (chest.chestEmpty)
        {
            //chest.isChestEmpty = true;
            //chest.item = null;
            iconInfoUI.enabled = false;
            titleofWeapon.text = "";
            equipButton.GetComponentInChildren<Text>().text = "Empty";
        }
        */
    }

   
}
