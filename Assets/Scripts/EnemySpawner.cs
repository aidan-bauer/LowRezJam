using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    public SpawnGroup[] waves;
    public int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        foreach (SpawnGroup wave in waves)
        {
            foreach (GameObject enemy in wave.enemies)
            {
                enemy.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.IsPaused)
        {
            if (waves[currentWave].enemies.Count(x => x.GetComponent<EnemyHealth>().isDead == false) == 0)
            {
                waves[currentWave].waveCompleteCallback.Invoke();
            }
        }
    }

    public void SpawnEnemies(int enemySpawnIndex)
    {
        foreach (GameObject enemy in waves[enemySpawnIndex].enemies)
        {
            enemy.SetActive(true);
        }

        currentWave = enemySpawnIndex;
    }

    public void SpawnEnemiesDelay(int enemySpawnIndex, float delay)
    {
        StartCoroutine(SpawnEnemiesDelay_Co(enemySpawnIndex, delay));
    }

    public IEnumerator SpawnEnemiesDelay_Co(int enemySpawnIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemies(enemySpawnIndex);
    }
}

[System.Serializable]
public class SpawnGroup
{
    public GameObject[] enemies;
    public UnityEvent waveCompleteCallback;
}
