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

    int attackTrigHash = Animator.StringToHash("attack");
    int attackNumbHash = Animator.StringToHash("attackNumber");
    int isAttackingHash = Animator.StringToHash("isAttacking");
    int blockingHash = Animator.StringToHash("block");

 

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        myStats = GetComponent<CharacterStats>();
	}
	
	// Update is called once per frame
	void Update () {
        buttonCoolDown -= Time.deltaTime;

        canAttack = IsPaused();
        
        if (canAttack)
            if(EquipmentManager.instance.curEquipment[3] != null)
                Controls();
         
	}

    void Controls()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log(EquipmentManager.instance.curEquipment[3]);
            Attack();
        }

        Blocking();
    }

    bool IsPaused()
    {
        if (Time.timeScale == 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Blocking()
    {
        if (Input.GetButton("Block"))
        {
            CharacterStats.instance.blocking = true;
            anim.SetBool(blockingHash, true);
        }

        else if (Input.GetButtonUp("Block"))
        {
            CharacterStats.instance.blocking = false;
            anim.SetBool(blockingHash, false);
        }

        else if (Input.GetAxisRaw("Trigger") > 0)
        {
            CharacterStats.instance.blocking = true;
            anim.SetBool(blockingHash, true);
        }

        else if (Input.GetAxisRaw("Trigger") == 0)
        {
            CharacterStats.instance.blocking = false;
            anim.SetBool(blockingHash, false);
        }
    }

    public void Attack()
    {


        if (anim.GetFloat(isAttackingHash) <= 0 ) {

            
            switch (attackNumber)
            {
                case 0:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 0);
                    if(enemyStats != null)
                        enemyStats.TakeDamage(myStats.damage.GetValue());
                    break;
                case 1:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 1);
                    if (enemyStats != null)
                        enemyStats.TakeDamage(myStats.damage.GetValue());
                    break;
                case 2:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 2);
                    if (enemyStats != null)
                        enemyStats.TakeDamage(myStats.damage.GetValue());
                    break;
                case 3:
                    anim.SetTrigger(attackTrigHash);
                    anim.SetInteger(attackNumbHash, 3);
                    if (enemyStats != null)
                        enemyStats.TakeDamage(myStats.damage.GetValue());
                    break;
            }
            //enemyStats = null;
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats eStats =  other.gameObject.GetComponent<CharacterStats>();
        if(other.gameObject.tag == "Enemy")
        {
            enemyStats = eStats;
            //if(isAttacking)
        }
        else
        {
            enemyStats = null;
        }
    }



    void Hit()
    {
        attackNumber++;
        if (attackNumber > 3)
            attackNumber = 0;

    }

}
