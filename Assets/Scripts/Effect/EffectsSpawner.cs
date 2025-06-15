using System.Collections.Generic;
using UnityEngine;

public class EffectsSpawner : MonoBehaviour
{
    public static EffectsSpawner Instance { get; private set; }

    [SerializeField] private EffectsLibSO effectLibSO;

    private Dictionary<string, Queue<Effect>> pool = new Dictionary<string, Queue<Effect>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Return2Pool(string keyName, Effect effect)
    {
        effect.gameObject.SetActive(false);

        if (!pool.ContainsKey(keyName))
        {
            pool[keyName] = new Queue<Effect>();
        }

        pool[keyName].Enqueue(effect);
    }

    public Effect SpawnEffect(string effectName, Vector3 position, Quaternion rotation)
    {
        var effectData = effectLibSO.GetEffect(effectName);
        if (effectData == null)
        {
            Debug.LogWarning($"Effect {effectName} not found in library.");
            return null;
        }

        Effect effectInstance;

        if (pool.TryGetValue(effectName, out var queue) && queue.Count > 0)
        {
            effectInstance = queue.Dequeue();
            effectInstance.transform.SetPositionAndRotation(position, rotation);
            effectInstance.gameObject.SetActive(true);
        }
        else
        {
            effectInstance = Instantiate(effectData.effectPrefab, position, rotation);
            effectInstance.SetKeyName(effectName);

        }

        effectInstance.RunAnim();

        return effectInstance;
    }
}
