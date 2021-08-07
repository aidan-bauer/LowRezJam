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


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        weapons = GetComponent<EnemyWeapons>();
        weapons.movement = this;
    }

    /*private void OnEnable()
    {
        if (target)
        {
            agent.SetDestination(target.position);
        }
    }*/

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
            }
            
            if (agent.remainingDistance <= stopDistance)
            {
                agent.isStopped = true;
                weapons.canAttack = true;
            }
        } else //if (PauseManager.IsPaused || health.isDead)
        {
            agent.isStopped = true;
        }
    }
}
