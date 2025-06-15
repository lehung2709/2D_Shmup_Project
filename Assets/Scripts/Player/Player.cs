using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int health=3;
    private int maxHealth = 3;
    [SerializeField] private int skillPoint =3;
    private int maxSkillPoint = 3;
    [SerializeField] private float moveSpeed = 5f;
    public bool isShield=false;
    public event Action ShieldBreakAction;

    [SerializeField] private string explosionEffectKey = "SmallExplode";
    [SerializeField] private string explosionSoundKey = "Explosion";



    [SerializeField] private Vector2 minLimit = new Vector2(-5f, -4f);
    [SerializeField] private Vector2 maxLimit = new Vector2(5f, 4f);


    [SerializeField] private Transform shootPoint;
    [SerializeField] private BulletPatternConfigSet[] bulletPatternConfigSets;

    private int bulletLevel = 1;
    private List<Coroutine> fireCorotines;


    [SerializeField] private float invincibleTime = 2f; 
    [SerializeField] private float blinkInterval = 0.1f; 
    [SerializeField] private float transparentAlpha = 0.3f;

    private bool isInvincible = false;
    private SpriteRenderer[] spriteRenderers;


    private bool isWin=false;

    
    [SerializeField] float tiltAmount = 10f;


    private void Awake()
    {
        health= maxHealth;
        skillPoint = maxSkillPoint;
        fireCorotines = new List<Coroutine>();
    }

    private void Start()
    {
        
        LevelManager.Instance.OnWin += OnWin;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        PlayerUI.Instance.Init(health,skillPoint);
       
        StartShooting(bulletLevel);
    }


    private void FixedUpdate()
    {
        if (isWin)
        {
            if (transform.position.x > 13) return;

            transform.Translate( Vector2.right*moveSpeed * Time.fixedDeltaTime, Space.World);

            return;
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime, Space.World);

        float clampedX = Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x);
        float clampedY = Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        HandleTilt(vertical); 
    }
    private void HandleTilt(float verticalInput)
    {
        float targetZRotation = verticalInput * tiltAmount;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetZRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
    }




    private void OnDisable()
    {
        StopAllShooting();
    }

    private void StartShooting(int level)
    {
        foreach (var bulletPatternConfig in bulletPatternConfigSets[level-1].bulletPatternConfigs)
        {
            fireCorotines.Add(bulletPatternConfig.SpawnPatternSO.Fire(shootPoint,  bulletPatternConfig.BulletSO, this));
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

    public bool TakeDamage()
    {
        if(isWin) return false;
        if (!isInvincible && health>0)
        {
            AudioManager.Instance.SpawnSoundEmitter(null,"TakeDmg",transform.position);
            
            if (isShield)
            {
                isShield = false;
                ShieldBreakAction?.Invoke();
                StartCoroutine(BecomeInvincible());
                return true;
            }
            DowngradeBullet();
            StartCoroutine(BecomeInvincible());
            PlayerUI.Instance.DecreaseHealPoint(health-1);
            health--;
            if (health == 0)
            {
                LevelManager.Instance.Lose();
                EffectsSpawner.Instance.SpawnEffect(explosionEffectKey, transform.position, Quaternion.identity);
                AudioManager.Instance.SpawnSoundEmitter(null, explosionSoundKey, transform.position);
                gameObject.SetActive(false);
                
            }
            LevelManager.Instance.FailDontTakeDamageMission();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseHealth()
    {
        if(health == maxHealth) return;
        health++;
        PlayerUI.Instance.IncreaseHealPoint(health-1);

    }
    public void IncreaseSkillPoint()
    {
        if (skillPoint == maxSkillPoint) return;
        skillPoint++;
        PlayerUI.Instance.IncreaseSkillPoint(skillPoint - 1);

    }

    public bool IsHaveSkillPoint()
    {
        if (skillPoint == 0) return false;
       
        return true;
    } 
    public void UseSkill()
    {
        skillPoint--;
        PlayerUI.Instance.DecreaseSkillPoint(skillPoint);
    }


    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        float elapsed = 0f;
        bool transparent = false;

        while (elapsed < invincibleTime)
        {
            float alpha = transparent ? transparentAlpha : 1f;
            foreach (var sprite in spriteRenderers)
            {
                Color c = sprite.color;
                sprite.color = new Color(c.r, c.g, c.b, alpha);
            }

            transparent = !transparent;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        foreach (var sprite in spriteRenderers)
        {
            Color c = sprite.color;
            sprite.color = new Color(c.r, c.g, c.b, 1f);
        }

        isInvincible = false;
    }

    public void UpgradeBullet()
    {
        if (bulletLevel == bulletPatternConfigSets.Length || isWin) return;
        StopAllShooting();
        bulletLevel++;
        StartShooting(bulletLevel);
    }
    public void DowngradeBullet()
    {
        if (bulletLevel==1 || isWin) return;
        StopAllShooting();
        bulletLevel--;
        StartShooting(bulletLevel);
    }

    private void OnWin()
    {
        StopAllShooting();
        moveSpeed = 10;
        isWin = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        TakeDamage();
    }



}
