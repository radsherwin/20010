using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AttackSystem : MonoBehaviour {

    Animator anim;
    private int attackNumber = 0;
    float buttonCoolDown;
    bool canAttack;

    CharacterStats myStats;
    CharacterStats enemyStats;
    Equipment[] curEquipment;

    int attackTrigHash = Animator.StringToHash("attack");
    int attackNumbHash = Animator.StringToHash("attackNumber");
    int isAttackingHash = Animator.StringToHash("isAttacking");
    int blockingHash = Animator.StringToHash("block");

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        myStats = GetComponent<CharacterStats>();

        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        curEquipment = new Equipment[numOfSlots];
	}
	
	// Update is called once per frame
	void Update () {
        buttonCoolDown -= Time.deltaTime;
       
        canAttack = IsPaused();
        
        if (canAttack)
            if(EquipmentManager.instance.curEquipment[3] != null)
                Controls();
         
	}

    void Controls() {
        if (Input.GetButtonDown("Fire1")) {
            //Debug.Log(EquipmentManager.instance.curEquipment[3]);
            Attack();
        }

        Blocking();
    }

    bool IsPaused() {
        if (Time.timeScale == 0f) {
            return false;
        }
        else {
            return true;
        }
    }

    void Blocking() {
        if (Input.GetButton("Block")) {
            CharacterStats.instance.blocking = true;
            anim.SetBool(blockingHash, true);
        }

        else if (Input.GetButtonUp("Block")) {
            CharacterStats.instance.blocking = false;
            anim.SetBool(blockingHash, false);
        }

        else if (Input.GetAxisRaw("Trigger") > 0) {
            CharacterStats.instance.blocking = true;
            anim.SetBool(blockingHash, true);
        }

        else if (Input.GetAxisRaw("Trigger") == 0) {
            CharacterStats.instance.blocking = false;
            anim.SetBool(blockingHash, false);
        }
    }

    public void Attack() {
        if (anim.GetFloat(isAttackingHash) <= 0 ) {
            switch (attackNumber) {
                case 0:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 0);
                    Attacking();
                    break;
                case 1:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 1);
                    Attacking();
                    break;
                case 2:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 2);
                    Attacking();
                    break;
                case 3:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 3);
                    Attacking();
                    break;
            }
            //enemyStats = null;
        }
    }

    void Attacking(){
        if (enemyStats != null) {
            enemyStats.TakeDamage(myStats.damage.GetValue());
            if(EquipmentManager.instance.curEquipment[3] != null){
                if (EquipmentManager.instance.curEquipment[3].lifeOfWeapon > 0) { 
                    EquipmentManager.instance.curEquipment[3].lifeOfWeapon -= enemyStats.armor.GetValue();
                }
                else if (EquipmentManager.instance.curEquipment[3].lifeOfWeapon <= 0) {
                    EquipmentManager.instance.curEquipment[3].lifeOfWeapon = 0;
                    Inventory.instance.RemoveItem(EquipmentManager.instance.curEquipment[3]);
                }
            }
            //enemyStats.armor.GetValue();

        }
    }


    private void OnTriggerEnter(Collider other) {
        CharacterStats eStats =  other.gameObject.GetComponent<CharacterStats>();
        if(other.gameObject.tag == "Enemy") {
            enemyStats = eStats;
        }
        else {
            enemyStats = null;
        }
    }

    void Hit() {
        attackNumber++;
        if (attackNumber > 3)
            attackNumber = 0;
    }

}
