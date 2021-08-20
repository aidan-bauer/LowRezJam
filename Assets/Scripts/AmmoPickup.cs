using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] string weaponName = "a";
    [SerializeField] int pickupAmmo = 5;
    public UnityEvent onPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            PlayerWeapons weapons = other.GetComponent<PlayerWeapons>();

            if (weapons)
            {
                foreach (WeaponSlot weapon in weapons.weapons)
                {
                    if (weapon.weapon.weaponName == weaponName && weapon.active)
                    {
                        weapons.PickUpAmmo(pickupAmmo);
                        onPickup.Invoke();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
