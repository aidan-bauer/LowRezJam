using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int enemies;
    public int secrets;
    public int pickups;

    private int totalEnemies;
    private int totalSecrets;
    private int totalPickups;

    // Start is called before the first frame update
    void Start()
    {

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
        float enemyKilledPercent = (float)enemies / totalEnemies;
        float secrestFoundercent = (float)secrets / totalSecrets;
        float pickupCollectedPercent = (float)pickups / totalPickups;
    }
}
