using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyTMP;
    void Start()
    {
        GameManager.Instance.OnMoneyChangeAction += OnMoneyChange;
        OnMoneyChange(GameManager.Instance.GetMoney());
    }

    private void OnMoneyChange(int money)
    {
        moneyTMP.text=money.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnMoneyChangeAction -= OnMoneyChange;

    }


}
