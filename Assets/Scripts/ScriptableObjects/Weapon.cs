using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Create New Weapon")]
public class Weapon : ScriptableObject
{

    public enum WeaponType { Semi, Auto }
    public enum ProjectileType { Raycast, Projectile }

    public string weaponName = "new weapon";
    public WeaponType weaponType;
    public int damage = 10;
    public int maxAmmo = 10;
    public int maxAmmoReserve = 100;
    public int rpm = 60;
    public ProjectileType projectileType;
    public float shotRadius = 0.25f;
    public GameObject projectile;
    public float projectileSpeed = 10f;

    public GameObject viewModel;
    public GameObject muzzleFlash;
    public AudioClip gunfire;

}
