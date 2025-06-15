using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Action<int> OnMoneyChangeAction;

    public int Money { get; private set; } = 0;

    public Action OnLose;
    public bool IsWin { get; private set; }= false;
    public Action OnWin;

    public bool DTDStatus {  get; private set; }
    public bool CAEStatus {  get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DTDStatus = true;
        CAEStatus = true;
    }
    private void Start()
    {
        GameManager.Instance.SpawnPlane(new Vector2(-7f, 0f));
    }

    public void Lose()
    {
        OnLose?.Invoke();
        AudioManager.Instance.SpawnSoundEmitter(null, "Lose", transform.position);

    }
    private int CountRemainCoin()
    {
        int count = 0;
        CoinPickUp[] coinPickUps = GameObject.FindObjectsByType<CoinPickUp>(FindObjectsSortMode.None);
        foreach (var coinPickUp in coinPickUps)
        {
            count += coinPickUp.gameObject.activeSelf ? 1 : 0;
        }
        return count;
    }    
    private void ClearBullet()
    {
        EnemyBullet[] bullets = GameObject.FindObjectsByType<EnemyBullet>(FindObjectsSortMode.None);

        foreach (var bullet in bullets)
        {
            bullet.Despawn();
        }
    }
    public void Win()
    {
        ClearBullet();
        OnWin?.Invoke();
        AudioManager.Instance.SpawnSoundEmitter(null, "Win", transform.position);
        IsWin = true;
        GameManager.Instance.CompleteLevel(DTDStatus, CAEStatus, Money + CountRemainCoin());

    }
    public void FailDontTakeDamageMission()
    {
        DTDStatus=false;
    }
    public void FailClearAllEnemyMission()
    {
        CAEStatus=false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AsyncLoader.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back2Menu()
    {
        Time.timeScale = 1;
        AsyncLoader.Instance.LoadScene("Menu");
    }    

    public void IncreaseMoney(int amount)
    {
        Money += amount;
        OnMoneyChangeAction(Money);
    }
}
