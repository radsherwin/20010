using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

#region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    
    public GameObject[] playerBones;
    public Equipment[] curEquipment;
    GameObject[] curItemObjects;

    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start(){
        //Get length of enum Equipment Slot
        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        curEquipment = new Equipment[numOfSlots];
        curItemObjects = new GameObject[numOfSlots];
        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem){
        if (inventory.allItems.Contains(newItem)){
            //Index number of enum EquipmentSlot
            int slotIndex = (int)newItem.equipSlot;
            Equipment oldItem = null;

            if (curEquipment[slotIndex] != null){
                oldItem = curEquipment[slotIndex];
                Unequip(slotIndex);
            }

            if (onEquipmentChanged != null){
                onEquipmentChanged.Invoke(newItem, oldItem);
            }
            curEquipment[slotIndex] = newItem;
            if(newItem.itemObject != null){
                GameObject newItemObject = Instantiate<GameObject>(newItem.itemObject);
                foreach (GameObject bone in playerBones){
                    if (bone.name == newItem.bone){
                        newItemObject.transform.parent = bone.transform;
                        newItemObject.transform.localPosition = new Vector3(0,0,0f);
                        newItemObject.transform.localRotation = new Quaternion(0,0,0,0);
                        curItemObjects[slotIndex] = newItemObject;
                    }
                }
            }                
        }
    }

    public void Unequip(int slotIndex){
        if(curEquipment[slotIndex] != null){
            Debug.Log(curItemObjects[slotIndex]);
            if (curItemObjects[slotIndex] != null){
                Debug.Log("destroying");
                Destroy(curItemObjects[slotIndex].gameObject);
                
                //curItemObjects[slotIndex] = null;
            }
            Equipment oldItem = curEquipment[slotIndex];
            curEquipment[slotIndex] = null;


            if (onEquipmentChanged != null){
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public bool IsEquipped(Equipment item){
        if(curEquipment[(int)item.equipSlot]){
            return true;
        }
        else{
            return false;
        }
    }
}
