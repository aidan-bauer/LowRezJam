using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] string weaponName = "a";
    [SerializeField] int pickupAmmo = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            PlayerWeapons weapons = other.GetComponent<PlayerWeapons>();

            if (weapons)
            {
                if (weapons.activeWeapon.weaponName == weaponName && weapons.activeWeapon.maxAmmo != weapons.activeWeapon.maxAmmoReserve)
                {
                    weapons.PickUpAmmo(pickupAmmo);
                    Destroy(gameObject);
                }
            }
        }
    }
}
