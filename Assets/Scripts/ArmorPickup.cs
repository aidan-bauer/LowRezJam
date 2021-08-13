using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmorPickup : MonoBehaviour
{
    [SerializeField] int healing = 10;
    public UnityEvent onPickup;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health)
        {
            if (health.armor != health.maxArmor)
            {
                health.AddArmor(healing);
                onPickup.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
