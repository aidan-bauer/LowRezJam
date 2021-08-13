using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] string weaponName = "a";
    public UnityEvent onPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (!PauseManager.IsPaused)
        {
            PlayerWeapons weapons = other.GetComponent<PlayerWeapons>();

            if (weapons)
            {
                for (int i = 0; i < weapons.weapons.Length; i++)
                {
                    if (weapons.weapons[i].weapon.weaponName == weaponName)
                    {
                        weapons.PickUpWeapon(weaponName);
                        onPickup.Invoke();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
