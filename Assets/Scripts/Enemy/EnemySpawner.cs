using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemySpawnData
    {
        public GameObject enemyPrefab;
        public bool isBoss;
        public int count;
        public float spawnDelayEachInstance;
        public float delay2NextSpawnData;
        public float offset;
        public bool isRandomPos;
    }

    [System.Serializable]
    public struct Wave
    {
        public List<EnemySpawnData> enemies;
        public float waveDelay;
    }

    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private BossHealthUI bossHealthUI;

    [SerializeField]private List<Wave> waves;

    [SerializeField] private int waveIndex = 0;
    [SerializeField] private int enemyCount = 0;
    [SerializeField] private int spawnCorotineCount=0;
    
    [SerializeField] private float spawnOffset;
    private float preOffset = 0;

    private Dictionary<string, Queue<GameObject>> enemyPools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            enemyPools = new Dictionary<string, Queue<GameObject>>();

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (waves.Count>0)
        {
            StartCoroutine(SpawnWave(waves[0]));
            waveIndex++;
        }
        
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        spawnCorotineCount++;
        try
        {
            yield return new WaitForSeconds(wave.waveDelay);

            foreach (var enemyData in wave.enemies)
            {
                StartCoroutine(Spawn(enemyData));
                yield return new WaitForSeconds(enemyData.delay2NextSpawnData);
            }
        }
        finally
        {
            spawnCorotineCount--;
        }
    }

    private IEnumerator Spawn(EnemySpawnData enemyData)
    {
        spawnCorotineCount++;
        try
        {
            for (int i = 0; i < enemyData.count; i++)
            {
                GameObject enemy = GetFromPool(enemyData.enemyPrefab);
                enemy.transform.right = transform.right;

                float offSet;
                if (enemyData.isRandomPos)
                {
                    do
                    {
                        offSet = Random.Range(-spawnOffset, spawnOffset);
                    } while (Mathf.Abs(offSet - preOffset) < 1.5f);
                    preOffset = offSet;
                }
                else
                {
                    offSet = Mathf.Clamp(enemyData.offset, -spawnOffset, spawnOffset);
                    preOffset = 0;
                }

                enemy.transform.position = transform.position + new Vector3(0, offSet, 0);
                Enemy enemyComponent= enemy.GetComponent<Enemy>();
                enemyComponent.SetInitialTransformData();
                enemyCount++;
                enemy.SetActive(true);
                if (enemyData.isBoss) bossHealthUI.RegisterEvent(enemyComponent);
                yield return new WaitForSeconds(enemyData.spawnDelayEachInstance);
            }
        }
        finally
        {
            spawnCorotineCount--;
        }
    }

    private GameObject GetFromPool(GameObject prefab)
    {
        if (!enemyPools.ContainsKey(prefab.name))
        {
            enemyPools[prefab.name] = new Queue<GameObject>();
        }


        if (enemyPools[prefab.name].Count > 0)
        {
            return enemyPools[prefab.name].Dequeue();
        }
        else
        {
            return Instantiate(prefab);
        }
    }

    public void ReturnToPool(GameObject enemy)
    {
        if(!enemy.activeSelf) return;
        enemyCount--;
        if(enemyCount == 0 && spawnCorotineCount==0  )
        {
            if(waves.Count > waveIndex)
            {
                StartCoroutine(SpawnWave(waves[waveIndex]));
                waveIndex++;
            }
            else
            {
                LevelManager.Instance.Win();
            }    
            
        }
        string enemyKey = enemy.name.Replace("(Clone)", "").Trim();
        if (!enemyPools.ContainsKey(enemyKey))
        {
            enemyPools[enemyKey] = new Queue<GameObject>();
        }
        enemy.SetActive(false);
        enemyPools[enemyKey].Enqueue(enemy);


    }
}
