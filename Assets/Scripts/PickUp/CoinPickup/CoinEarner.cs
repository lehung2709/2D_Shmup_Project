using UnityEngine;

public class CoinEarner : MonoBehaviour
{
    public float pickupRange = 5f;
    public LayerMask coinLayer;

    private void Start()
    {
        LevelManager.Instance.OnWin += OnWin;
    }
    private void FixedUpdate()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, pickupRange, coinLayer);

        foreach (var coin in coins)
        {
            CoinPickUp coinPickup = coin.GetComponent<CoinPickUp>();
            if (coinPickup != null && !coinPickup.IsMove2Des)
            {
                coinPickup.MoveToDes(transform);
            }
        }
    }
    private void OnWin()
    {
        pickupRange = 15f;
    }    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,pickupRange);
    }
}
