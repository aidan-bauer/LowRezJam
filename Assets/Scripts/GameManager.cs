using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject levelCompleteScreen;
    public Text enemyText, secretText, pickUpText;

    public int enemies;
    public int secrets;
    public int pickups;

    private int totalEnemies;
    private int totalSecrets;
    private int totalPickups;

    private void Awake()
    {
        totalEnemies = FindObjectsOfType<EnemyHealth>().Length;
        totalSecrets = GameObject.FindGameObjectsWithTag("Secret").Length;
        totalPickups = FindObjectsOfType<AmmoPickup>().Length + FindObjectsOfType<HealthPickup>().Length + FindObjectsOfType<ArmorPickup>().Length + FindObjectsOfType<WeaponPickup>().Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*totalEnemies = FindObjectsOfType<EnemyHealth>().Length;
        totalSecrets = GameObject.FindGameObjectsWithTag("Secret").Length;
        totalPickups = FindObjectsOfType<AmmoPickup>().Length + FindObjectsOfType<HealthPickup>().Length + FindObjectsOfType<ArmorPickup>().Length + FindObjectsOfType<WeaponPickup>().Length;*/
    }

    public void EnemyKilled()
    {
        enemies++;
    }

    public void SecretFound()
    {
        secrets++;
    }

    public void PickupCollected()
    {
        pickups++;
    }

    public void AddEnemies(int enemiesToAdd)
    {
        totalEnemies += enemiesToAdd;
    }

    public void LevelComplete()
    {
        float enemyKilledPercent = (float)enemies / (float)totalEnemies;
        float secrestFoundercent = (float)secrets / (float)totalSecrets;
        float pickupCollectedPercent = (float)pickups / (float)totalPickups;

        enemyText.text = enemyKilledPercent.ToString("P1");
        secretText.text = secrestFoundercent.ToString("P1");
        pickUpText.text = pickupCollectedPercent.ToString("P1");

        levelCompleteScreen.SetActive(true);
    }
}
