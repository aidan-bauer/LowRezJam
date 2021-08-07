using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponInspector : Editor
{
    Weapon weapon;

    bool raycastWeapon, projectileWeapon;

    SerializedProperty weaponName;
    SerializedProperty weaponType;
    SerializedProperty damage;
    SerializedProperty maxAmmo;
    SerializedProperty maxAmmoReserve;
    SerializedProperty rpm;
    SerializedProperty projectileType;
    SerializedProperty shotRadius;
    SerializedProperty projectile;
    SerializedProperty projectileSpeed;

    SerializedProperty viewModel;
    SerializedProperty worldModel;
    SerializedProperty muzzleFlash;
    SerializedProperty gunfire;

    private void OnEnable()
    {
        weapon = target as Weapon;

        weaponName = serializedObject.FindProperty("weaponName");
        weaponType = serializedObject.FindProperty("weaponType");
        damage = serializedObject.FindProperty("damage");
        maxAmmo = serializedObject.FindProperty("maxAmmo");
        maxAmmoReserve = serializedObject.FindProperty("maxAmmoReserve");
        rpm = serializedObject.FindProperty("rpm");
        projectileType = serializedObject.FindProperty("projectileType");
        shotRadius = serializedObject.FindProperty("shotRadius");
        projectile = serializedObject.FindProperty("projectile");
        projectileSpeed = serializedObject.FindProperty("projectileSpeed");
        viewModel = serializedObject.FindProperty("viewModel");
        worldModel = serializedObject.FindProperty("worldModel");
        muzzleFlash = serializedObject.FindProperty("muzzleFlash");
        gunfire = serializedObject.FindProperty("gunfire");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(weaponName, new GUIContent("Weapon Name"));
        EditorGUILayout.PropertyField(weaponType, new GUIContent("Type of Weapon"));
        EditorGUILayout.PropertyField(damage, new GUIContent("Damage"));
        EditorGUILayout.PropertyField(maxAmmo, new GUIContent("Max Ammo"));
        EditorGUILayout.PropertyField(maxAmmoReserve, new GUIContent("Max Reserve Ammo"));
        EditorGUILayout.PropertyField(rpm, new GUIContent("Rounds Per Minute"));
        EditorGUILayout.PropertyField(projectileType, new GUIContent("Projectile Type"));

        if (weapon.projectileType == Weapon.ProjectileType.Raycast)
        {
            EditorGUILayout.PropertyField(shotRadius, new GUIContent("Shot Radius"));
        } else if (weapon.projectileType == Weapon.ProjectileType.Projectile)
        {
            EditorGUILayout.PropertyField(projectile, new GUIContent("Projectile"));
            EditorGUILayout.PropertyField(projectileSpeed, new GUIContent("Projectile Speed"));
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(viewModel, new GUIContent("Viewmodel"));
        EditorGUILayout.PropertyField(worldModel, new GUIContent("Worldmodel"));
        EditorGUILayout.PropertyField(muzzleFlash, new GUIContent("Muzzle Flash"));
        EditorGUILayout.PropertyField(gunfire, new GUIContent("Gunfire FX"));

        serializedObject.ApplyModifiedProperties();
    }
}
