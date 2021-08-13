using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{

    public enum AttackType { Melee, Ranged }
    public AttackType attackType;

    public Weapon weapon;

    public bool canAttack;

    public bool canFire = true; //set this to false with melee enemies
    public float fireRateTimer = 0;

    public int meleeAttackDamage = 10;
    public float meleeAttackTime = 3f;

    [HideInInspector] public EnemyMovement movement;
    EnemyHealth health;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused && !health.isDead)
        {
            if (canAttack)
            {
                if (attackType == AttackType.Ranged)
                {
                    if (canFire)
                    {
                        Fire();
                        canFire = false;
                    }
                    else
                    {
                        fireRateTimer += Time.deltaTime;

                        if (fireRateTimer >= (60f / weapon.rpm))
                        {
                            fireRateTimer = 0;
                            canFire = true;
                        }
                    }
                } else if (attackType == AttackType.Melee)
                {
                    if (canFire)
                    {
                        MeleeAttack();
                        canFire = false;
                    } else
                    {
                        fireRateTimer += Time.deltaTime;

                        if (fireRateTimer >= meleeAttackTime)
                        {
                            fireRateTimer = 0;
                            canFire = true;
                        }
                    }
                }
            } else
            {

            }
        }
    }

    public void Fire()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.SphereCast(ray, 0.25f, out hit, 100f))
        {
            PlayerHealth health = hit.transform.GetComponent<PlayerHealth>();

            if (health)
            {
                health.Hurt(weapon.damage);
            }
        }
    }

    void MeleeAttack()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, movement.stopDistance))
        {
            PlayerHealth health = hit.transform.GetComponent<PlayerHealth>();

            if (health)
            {
                health.Hurt(meleeAttackDamage);
            }
        }
    }
}
