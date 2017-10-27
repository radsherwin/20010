using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyAttack : MonoBehaviour {

    Animator anim;
    CharacterStats targetStats;
    //My stats meaning the enemies stats
    CharacterStats myStats;
    private bool canAttack = false;

    public float attackSpeed = 2f;
    public float attackCooldown = 0f;

    int attackTrigHash = Animator.StringToHash("attack");
    //int attackNumbHash = Animator.StringToHash("attackNumber");
    int isAttackingHash = Animator.StringToHash("isAttacking");

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
      
        myStats = GetComponent<CharacterStats>();
        //targetStats = FindObjectOfType<PlayerStats>();
	}

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        if (anim.GetFloat(isAttackingHash) <= 0)
        {
        
            if (attackCooldown <= 0)
            {
                anim.SetTrigger(attackTrigHash);
                if (canAttack)
                {
                    Debug.Log("Adam attacking");
                    targetStats.TakeDamage(myStats.damage.GetValue());
                }

                attackCooldown = 1f / attackSpeed;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats eStats = other.gameObject.GetComponent<CharacterStats>();
        if (other.gameObject.tag == "Player")
        {
            canAttack = true;
            targetStats = eStats;
            
        }
        else
        {
            //targetStats = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canAttack = false;

        }
    }

    void Hit()
    {

    }
}
