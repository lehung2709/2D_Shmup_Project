using UnityEngine;
using System.Collections;


public abstract class SpawnPatternSO : ScriptableObject
{
    
    public float fireRate = 0.5f; 
    public bool infiniteDuration = false; 
    public float duration = 5f;

    public abstract Coroutine Fire(Transform shootPoint, BulletSO bulletSO,MonoBehaviour gameObject);
    protected abstract IEnumerator FireRoutine(Transform shootPoint, BulletSO bulletSO);
    
}
