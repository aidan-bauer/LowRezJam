using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    public AudioClip deathSound;

    public override void Hurt(int damage)
    {
        if (currHealth - damage > 0)
        {
            currHealth -= damage;
        }
        else
        {
            currHealth = 0;
            OnDeath();
        }
    }

    public override void Heal(int healing)
    {
        if (currHealth + healing < maxHealth)
            currHealth += healing;
        else
            currHealth = maxHealth;
    }

    public void DeathStuff()
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }
}
