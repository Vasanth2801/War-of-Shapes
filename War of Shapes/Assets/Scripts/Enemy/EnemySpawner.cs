using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public float timeBetweenSpawns = 0.5f;
        public float timeBetweenWaves = 1f;
        public int enemiesCount;
    }

    [Header("Wave Settings")]
    public Wave[] waves;
    [SerializeField] private float countDown;
    public Transform[] spawnPoint;
    public int currentWave = 1;
    public bool countDownBegin;

    void Start()
    {
        countDownBegin = true;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesCount = waves[i].enemies.Length;
        }
    }

    void Update()
    {
        if (currentWave > waves.Length)
        {
            Debug.Log("Wave Spawned");
            UIManager.Instance.WinScreen();
            return;
        }

        if (countDownBegin == true)
        {
            countDown -= Time.deltaTime;
        }

        if (countDown <= 0f)
        {
            countDownBegin = false;
            countDown = waves[currentWave].timeBetweenWaves;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWave].enemiesCount == 0)
        {
            countDownBegin = true;
            currentWave++;
        }
    }

    IEnumerator SpawnWave()
    {
        if (currentWave < waves.Length)
        {
            if (currentWave < waves.Length)
            {
                for (int i = 0; i < waves[currentWave].enemies.Length; i++)
                {
                    Transform spawnPoints = spawnPoint[Random.Range(0, spawnPoint.Length)];
                    GameObject enemy = Instantiate(waves[currentWave].enemies[i], spawnPoints.position, Quaternion.identity);
                    yield return new WaitForSeconds(waves[currentWave].timeBetweenWaves);
                }
                Debug.Log("Wave " + currentWave + " Spawned");
            }
        }
    }
}
