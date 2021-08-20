using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    public WeaponSlot activeWeapon;
    public WeaponSlot[] weapons;
    public RectTransform ammoBar;
    [SerializeField]int activeWeaponIndex = 0;

    float fireRateTimer = 0;
    public bool canFire = true;

    GameObject weaponHolder;
    PlayerHealth health;
    AudioSource source;
    Animation weaponAnim;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponHolder = GameObject.Find("weapon_holder");

        ChangeActiveWeapon();

        foreach (WeaponSlot weapon in weapons)
        {
            weapon.currAmmo = weapon.weapon.maxAmmo;
        }

        UpdateUI(ammoBar, (float) activeWeapon.currAmmo / activeWeapon.weapon.maxAmmoReserve);  //need to call again to update the UI at the start
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused && !health.isDead)
        {
            if (activeWeapon.weapon.weaponType == Weapon.WeaponType.Auto)
            {
                if (canFire)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        Fire();
                        if (weaponAnim.isPlaying)   //restart the animation if we start firing again before it finishes
                            weaponAnim.Rewind();
                        weaponAnim.Play();
                        source.Play();
                        canFire = false;
                    }
                }
                else
                {
                    fireRateTimer += Time.deltaTime;

                    if (fireRateTimer >= (60f / activeWeapon.weapon.rpm))
                    {
                        fireRateTimer = 0;
                        canFire = true;
                    }
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    source.Stop();
                }
            } else if (activeWeapon.weapon.weaponType == Weapon.WeaponType.Semi)
            {
                if (canFire)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Fire();
                        if (weaponAnim.isPlaying)
                            weaponAnim.Rewind();
                        weaponAnim.Play();
                        source.PlayOneShot(source.clip);
                        canFire = false;
                    }
                }
                else
                {
                    fireRateTimer += Time.deltaTime;

                    if (fireRateTimer >= (60f / activeWeapon.weapon.rpm))
                    {
                        fireRateTimer = 0;
                        canFire = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                activeWeaponIndex = NextAvailableWeapon(true);
                ChangeActiveWeapon();
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                activeWeaponIndex = NextAvailableWeapon(false);
                ChangeActiveWeapon();
            }

            //bad code but it works, need to replace later
            /*if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                activeWeaponIndex = 0;
                ChangeActiveWeapon();
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                activeWeaponIndex = 1;
                ChangeActiveWeapon();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                activeWeaponIndex = 2;
                ChangeActiveWeapon();
            }*/
        }
    }

    public void ChangeActiveWeapon()
    {
        if (weapons[activeWeaponIndex].active)
        {
            activeWeapon = weapons[activeWeaponIndex];
            UpdateUI(ammoBar, (float)activeWeapon.currAmmo / activeWeapon.weapon.maxAmmoReserve);

            if (weaponHolder.transform.childCount > 0)
                Destroy(weaponHolder.transform.GetChild(0).gameObject);

            GameObject activeWeaponInst = Instantiate(activeWeapon.weapon.viewModel, weaponHolder.transform);
            weaponAnim = activeWeaponInst.GetComponent<Animation>();
            source.clip = activeWeapon.weapon.gunfire;
        }
    }

    public void Fire()
    {
        if (weapons[activeWeaponIndex].currAmmo > 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f, 0));
            Debug.DrawRay(ray.origin, ray.direction, Color.black, 10f);

            if (activeWeapon.weapon.projectileType == Weapon.ProjectileType.Raycast)
            {
                if (Physics.SphereCast(ray, 0.25f, out hit, 100f))
                {
                    Health health = hit.transform.GetComponent<Health>();

                    if (health)
                    {
                        health.Hurt(activeWeapon.weapon.damage);
                    }
                }
            } else if (activeWeapon.weapon.projectileType == Weapon.ProjectileType.Projectile)
            {
                GameObject projectileInst = Instantiate(activeWeapon.weapon.projectile, ray.origin + activeWeapon.weapon.muzzlePos, transform.rotation);
                projectileInst.GetComponent<Bullet>().damage = activeWeapon.weapon.damage;
                projectileInst.GetComponent<Rigidbody>().velocity = ray.direction.normalized * activeWeapon.weapon.projectileSpeed;
            }

            weapons[activeWeaponIndex].currAmmo--;

            UpdateUI(ammoBar, (float)weapons[activeWeaponIndex].currAmmo / activeWeapon.weapon.maxAmmoReserve);
        }
    }

    public void PickUpAmmo(int newAmmo)
    {
        if (weapons[activeWeaponIndex].currAmmo + newAmmo < activeWeapon.weapon.maxAmmoReserve)
        {
            weapons[activeWeaponIndex].currAmmo += newAmmo;
        }
        else
        {
            weapons[activeWeaponIndex].currAmmo = activeWeapon.weapon.maxAmmoReserve;
        }

        UpdateUI(ammoBar, (float)weapons[activeWeaponIndex].currAmmo / activeWeapon.weapon.maxAmmoReserve);
    }

    public void PickUpWeapon(string newWeapon)
    {
        for (int i = 0; i < weapons.Length; i ++)
        {
            if (weapons[i].weapon.weaponName == newWeapon)
            {
                if (!weapons[i].active)
                {
                    weapons[i].active = true;
                    //after picking up weapon
                    activeWeaponIndex = i;
                    ChangeActiveWeapon();
                }
            }
        }
    }

    void UpdateUI(RectTransform bar, float percentFill)
    {
        bar.localScale = new Vector3(percentFill, 1, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="increase">True = increase weapon index, False = decrease weapon index</param>
    /// <returns></returns>
    public int NextAvailableWeapon(bool increase)
    {
        if (weapons.Count(x => x.active == true) > 1)
        {
            if (increase)
            {
                for (int i = activeWeaponIndex + 1; i < weapons.Length; i++)
                {
                    if (weapons[i].active)
                        return i;

                    //if we reach the end of the list and have not found an item, then we loop back around to the start
                    if (i > weapons.Length - 1)
                        i = 0;
                }
            }
            else
            {
                for (int i = activeWeaponIndex - 1; i >= -1; i--)
                {
                    //if we reach the beginning of the list and have not found an item, then we loop back around to the top
                    if (i < 0)
                        i = weapons.Length - 1;

                    if (weapons[i].active)
                        return i;
                }
            }
        }

        return 0;
    }
}

[System.Serializable]
public class WeaponSlot
{
    public Weapon weapon;
    public bool active = false;
    public float currAmmo;
}
