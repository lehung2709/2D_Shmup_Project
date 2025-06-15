using UnityEngine;

public class CoinPickUp :PickUp 
{
    public bool IsMove2Des {  get; private set; }=false;
    private Transform desTransform;
    [SerializeField] private float moveSpeed = 20f; 


    protected override void IsPickedUp(Player player)
    {
        LevelManager.Instance.IncreaseMoney(1);
        AudioManager.Instance.SpawnSoundEmitter(null, "Coin", transform.position);
    }

    public void MoveToDes(Transform transform)
    {
        IsMove2Des = true;
        desTransform = transform;
    }

    protected override void FixedUpdate()
    {
        if(IsMove2Des)
        {
            if (desTransform == null)
                return;

            Vector3 direction = (desTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.fixedDeltaTime;

        }
        else
        {
            transform.Rotate(0, 80 * Time.fixedDeltaTime, 0);
            moveStrategy?.Move();
        }
    }

    public override void SetInitialData()
    {
        base.SetInitialData();
        IsMove2Des=false;
    }
}
