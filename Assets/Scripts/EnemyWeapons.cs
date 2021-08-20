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
    AudioSource source;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        source = GetComponent<AudioSource>(); 
    }

    private void Start()
    {
        source.clip = weapon.gunfire;
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
                        source.PlayOneShot(source.clip);

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
        Ray ray = new Ray(transform.position + weapon.muzzlePos, transform.forward);
        ray.direction = new Vector3(ray.direction.x, 0, ray.direction.z);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f);

        if (weapon.projectileType == Weapon.ProjectileType.Raycast)
        {
            if (Physics.SphereCast(ray, 0.25f, out hit, 100f))
            {
                PlayerHealth health = hit.transform.GetComponent<PlayerHealth>();

                if (health)
                {
                    health.Hurt(weapon.damage);
                }
            }
        } else if (weapon.projectileType == Weapon.ProjectileType.Projectile)
        {
            GameObject projectileInst = Instantiate(weapon.projectile, ray.origin, transform.rotation);
            projectileInst.GetComponent<Bullet>().damage = weapon.damage;
            projectileInst.GetComponent<Rigidbody>().velocity = ray.direction.normalized * weapon.projectileSpeed;
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
