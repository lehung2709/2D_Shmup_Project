using TMPro;
using UnityEngine;

public class InGameMoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyTMP;
    void Start()
    {
        LevelManager.Instance.OnMoneyChangeAction += OnMoneyChange;
        OnMoneyChange(LevelManager.Instance.Money);
    }

    private void OnMoneyChange(int money)
    {
        moneyTMP.text = money.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnMoneyChangeAction -= OnMoneyChange;

    }
}
