using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public static PickUpSpawner Instance { get; private set; }

    private Dictionary<string, Queue<GameObject>> pickUpPools;
    [SerializeField] private List<GameObject> pickUpPrefabs;
    [SerializeField] private GameObject coinPickUpPrefab;
    [SerializeField] private List<Transform> coinPattern;
    [SerializeField] private float interval = 2f;
    [SerializeField] private float spawnOffset = 5f;
    private float timer = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            pickUpPools = new Dictionary<string, Queue<GameObject>>();

            foreach (var prefab in pickUpPrefabs)
            {
                pickUpPools[prefab.name] = new Queue<GameObject>();
            }
            pickUpPools[coinPickUpPrefab.name] = new Queue<GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > interval)
        {
            SpawnRandomPickUp();
            timer = 0;
        }
    }

    private void SpawnRandomPickUp()
    {
        if (pickUpPrefabs.Count == 0) return;

        GameObject prefab = pickUpPrefabs[Random.Range(0, pickUpPrefabs.Count)];

        
        float offSet = Random.Range(-spawnOffset, spawnOffset);
        Vector3 spawnPosition = new Vector3(transform.position.x , transform.position.y + offSet, transform.position.z);

        SpawnPickUp(prefab, spawnPosition);
       

    }
    private void SpawnPickUp(GameObject prefab,Vector3 spawnPosition)
    {
        GameObject pickUp;
        if (pickUpPools[prefab.name].Count > 0)
        {
            pickUp = pickUpPools[prefab.name].Dequeue();
        }
        else
        {
            pickUp = Instantiate(prefab);
            pickUp.GetComponent<PickUp>().SetPoolKey(prefab.name);
        }
        pickUp.transform.right = this.transform.right;
        pickUp.transform.position = spawnPosition;
        pickUp.GetComponent<PickUp>().SetInitialData();
        pickUp.SetActive(true);

    }
    private void SpawnCoin(Vector2 pos, Transform pattern=null)
    {
        if (pattern != null)
        {
            foreach (Transform child in pattern)
            {
                Vector3 worldPos = pos + (Vector2)child.localPosition;
                SpawnPickUp(coinPickUpPrefab, worldPos);
            }
        }
    }

    public void SpawnRandomCoinPattern(Vector2 spawnPos)
    {
        if (coinPattern == null || coinPattern.Count == 0) return;

        Transform randomPattern = coinPattern[Random.Range(0, coinPattern.Count)];


        SpawnCoin(spawnPos, randomPattern);
    }



    public void ReturnToPool(GameObject pickUp,string poolKey)
    {
        pickUp.SetActive(false);
        pickUpPools[poolKey].Enqueue(pickUp);
    }
}
