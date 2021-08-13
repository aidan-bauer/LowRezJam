using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class ExplosiveBarrel : MonoBehaviour
{

    public int damage = 30;
    public float range = 5f;

    public void Explode()
    {
        Collider[] collateral = Physics.OverlapSphere(transform.position, 5f, 1 << 6);

        foreach (Collider col in collateral)
        {
            Health health = col.GetComponent<Health>();

            if (health)
            {
                health.Hurt(damage);
            }
        }

        Destroy(gameObject);
    }
}
