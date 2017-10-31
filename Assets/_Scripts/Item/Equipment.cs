using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {
    #region Singleton
    public static Equipment instance;

    private void Awake()
    {
        instance = this;
    }
#endregion 
    public EquipmentSlot equipSlot;
    //public GameObject itemObject;
    public string bone;

    public int armorModifier;
    public int damageModifier;
    public int blockModifier;
    public int bonusModifier;
    public int lifeOfWeapon;

    int returnedValue;
    public override void Use() {
        base.Use();
        Debug.Log("In Equipment");
        EquipmentManager.instance.Equip(this);
    }

    public override void Remove() {
        base.Remove();

        //EquipmentManager.instance.Unequip((int)equipSlot);
        EquipmentManager.instance.Unequip((int)this.equipSlot);
    }

    public override bool IsEquipped(){
        bool isEquippedBool = EquipmentManager.instance.IsEquipped(this);
        if (isEquippedBool){
            return true;
        }
        else{
            return false;
        }
    }

    public override int GetStats(int Modifier) {
        switch (Modifier) {
            case 0:
                returnedValue = armorModifier;
                break;
            case 1:
                returnedValue = damageModifier;
                break;
            case 2:
                returnedValue = bonusModifier;
                break;
        }
        return returnedValue;
    }
}

public enum EquipmentSlot {
    HEAD,
    CHEST,
    FEET,
    WEAPON,
    SHIELD
}


