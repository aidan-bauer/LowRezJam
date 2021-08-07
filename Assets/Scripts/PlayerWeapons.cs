using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{

    public Weapon activeWeapon;
    public WeaponSlot[] weapons;
    public RectTransform ammoBar;
    int activeWeaponIndex = 0;
    public int weaponUnlockLevel;

    public int currAmmo;
    public int currAmmoReserve;

    float fireRateTimer = 0;
    public bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        ChangeActiveWeapon(0);
        currAmmo = activeWeapon.maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused)
        {
            if (activeWeapon.weaponType == Weapon.WeaponType.Auto)
            {
                if (canFire)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Debug.Log("automatic fire");
                        Fire();
                        canFire = false;
                    }
                }
                else
                {
                    fireRateTimer += Time.deltaTime;

                    if (fireRateTimer >= (60f / activeWeapon.rpm))
                    {
                        fireRateTimer = 0;
                        canFire = true;
                    }
                }
            } else if (activeWeapon.weaponType == Weapon.WeaponType.Semi)
            {
                if (canFire)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Debug.Log("semiautomatic fire");
                        Fire();
                        canFire = false;
                    }
                }
                else
                {
                    fireRateTimer += Time.deltaTime;

                    if (fireRateTimer >= (60f / activeWeapon.rpm))
                    {
                        fireRateTimer = 0;
                        canFire = true;
                    }
                }
            }
        }
    }

    public void ChangeActiveWeapon(int index)
    {
        if (weapons[index].active)
        {
            activeWeapon = weapons[index].weapon;
            UpdateUI(ammoBar, (float) currAmmo / activeWeapon.maxAmmo);
        }
    }

    public void Fire()
    {
        if (currAmmo > 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f, 0));
            Debug.DrawRay(ray.origin, ray.direction, Color.black, 10f);

            if (Physics.SphereCast(ray, 0.25f, out hit, 100f))
            {
                Health health = hit.transform.GetComponent<Health>();

                if (health)
                {
                    health.Hurt(activeWeapon.damage);
                }
            }

            currAmmo--;

            UpdateUI(ammoBar, (float) currAmmo / activeWeapon.maxAmmo);
        }
    }

    public void PickUpAmmo(int newAmmo)
    {
        if (currAmmo < activeWeapon.maxAmmoReserve + newAmmo)
        {
            currAmmo += newAmmo;
        }
        else
        {
            currAmmo = activeWeapon.maxAmmoReserve;
        }
    }

    public void PickUpWeapon(string newWeapon)
    {
        for (int i = 0; i < weapons.Length; i ++)
        {
            if (weapons[i].weapon.weaponName == newWeapon)
                if (!weapons[i].active)
                    weapons[i].active = true;
        }
    }

    void UpdateUI(RectTransform bar, float percentFill)
    {
        bar.localScale = new Vector3(percentFill, 1, 1);
    }
}

[System.Serializable]
public class WeaponSlot
{
    public Weapon weapon;
    public bool active = false;
}
