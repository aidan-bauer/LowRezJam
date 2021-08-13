using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] int healing = 10;
    public UnityEvent onPickup;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health)
        {
            if (health.currHealth != health.maxHealth)
            {
                health.Heal(healing);
                onPickup.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
