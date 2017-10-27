using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    EnemyAttack enemyAttack;
    Animator anim;

    Transform target;
    NavMeshAgent agent;

    int moveForwardHash = Animator.StringToHash("forward");
    //int attackNumbHash = Animator.StringToHash("attackNumber");
    //int isAttackingHash = Animator.StringToHash("isAttacking");

    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            anim.SetFloat(moveForwardHash, 1);

            if(distance <= agent.stoppingDistance)
            {
                anim.SetFloat(moveForwardHash, 0);
                enemyAttack.Attack();
                FaceTarget();
            }
        }
	}

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void FootR()
    {

    }

    void FootL()
    {

    }
}
