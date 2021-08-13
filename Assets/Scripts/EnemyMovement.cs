using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    public float stopDistance = 5;

    NavMeshAgent agent;
    EnemyHealth health;
    EnemyWeapons weapons;
    Animator anim;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        weapons = GetComponent<EnemyWeapons>();
        anim = GetComponentInChildren<Animator>();
        weapons.movement = this;
    }

    private void OnEnable()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused && !health.isDead)
        {
            if (target)
            {
                agent.SetDestination(target.position);
            }

            if (agent.remainingDistance > stopDistance)
            {
                agent.isStopped = false;
                anim.SetBool("inRange", false);
            }
            
            if (agent.remainingDistance <= stopDistance)
            {
                agent.isStopped = true;
                weapons.canAttack = true;
                anim.SetBool("inRange", true);

                if (weapons.attackType == EnemyWeapons.AttackType.Ranged)
                    transform.LookAt(target);
            }
        } else
        {
            agent.isStopped = true;
        }
    }
}
