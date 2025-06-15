using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

[System.Serializable] 
public class EnemyPhaseData
{
    [Range(0, 100)]
    public int healthPercentThreshold; 
    public BulletPatternConfigWithCoolDownSet bulletPatternConfig;
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;
    public event Action<int,int> heahthChangeAction;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private List<EnemyPhaseData> phases;
    private int currentPhaseIndex = -1;

    [SerializeField] private MoveStrategySO moveStrategySO;
    IMoveStrategy moveStrategy;
    private List<Coroutine> fireCorotines;

    private DamageFlash damageFlash;

    [SerializeField] private string explosionEffectKey = "SmallExplode";
    [SerializeField] private string dieSoundKey = "Die";


    private void Awake()
    {
        phases.Sort((a, b) => b.healthPercentThreshold.CompareTo(a.healthPercentThreshold));
        fireCorotines = new List<Coroutine>();
        moveStrategy = moveStrategySO.GetMoveStrategy(this.transform);
    }
    private void Start()
    {
        damageFlash = GetComponent<DamageFlash>();
    }

    private void FixedUpdate()
    {
        moveStrategy?.Move();
        if (transform.position.x < -10)
        {
            LevelManager.Instance.FailClearAllEnemyMission();
            EnemySpawner.Instance.ReturnToPool(this.gameObject);
        }
    }

    private void OnEnable()
    {
        currentPhaseIndex = -1;
        Health = MaxHealth;
        StartShooting(); 
    }

    private void OnDisable()
    {
        StopAllShooting();
    }

    private void StartShooting()
    {
        UpdatePhase(); 
    }

    

    private void UpdatePhase()
    {
        int healthPercent = (int)((float)Health / MaxHealth * 100);

        for (int i = 0; i < phases.Count; i++)
        {
           
            if (currentPhaseIndex < i)
            {
                if (healthPercent <= phases[i].healthPercentThreshold)
                {
                    currentPhaseIndex = i;
                    StopAllShooting();
                    StartShootingCoroutine(phases[i].bulletPatternConfig);
                }
            }
            else
            {
                continue;
            } 
                    
                
            
        }
    }

    private void StartShootingCoroutine(BulletPatternConfigWithCoolDownSet configSet) 
    {
        if (configSet == null || configSet.bulletPatternConfigs.Length == 0)  return;

        
        foreach (var bulletPatternConfig in configSet.bulletPatternConfigs)
        {
            fireCorotines.Add(StartCoroutine(ShootingPatternCoroutine(bulletPatternConfig)));
        }
        
    }

    private IEnumerator ShootingPatternCoroutine(BulletPatternConfigWithCoolDown bulletPatternConfig)
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletPatternConfig.coolDown);
            fireCorotines.Add(bulletPatternConfig.config.SpawnPatternSO.Fire(shootPoint, bulletPatternConfig.config.BulletSO, this));
            if(bulletPatternConfig.config.SpawnPatternSO.infiniteDuration) yield break;
            yield return new WaitForSeconds(bulletPatternConfig.config.SpawnPatternSO.duration);
        }
        
    }

    private void StopAllShooting()
    {
        foreach (var coroutine in fireCorotines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
        fireCorotines.Clear();
    }

    public void SetInitialTransformData()
    {
        moveStrategy.SetInitialTransformData();
    }

    public void TakeDamage(int damage)
    {
        damageFlash.CallDamageFlash();
        Health -= damage;
        heahthChangeAction?.Invoke(Health,MaxHealth);

        if (Health <= 0)
        {
            Death();
        }
        else
        {
            UpdatePhase();

        }
    }

    private void Death()
    {
        PickUpSpawner.Instance.SpawnRandomCoinPattern(transform.position);
        AudioManager.Instance.SpawnSoundEmitter(null, dieSoundKey, transform.position);
        EffectsSpawner.Instance.SpawnEffect(explosionEffectKey, transform.position, Quaternion.identity);
        EnemySpawner.Instance.ReturnToPool(this.gameObject);
    }
}
