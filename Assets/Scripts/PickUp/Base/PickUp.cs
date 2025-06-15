using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] private MoveStrategySO moveStrategySO;
    protected IMoveStrategy moveStrategy;
    private string poolKey;

    private void Awake()
    {
        moveStrategy = moveStrategySO.GetMoveStrategy(this.transform);

    }
  
    protected virtual void FixedUpdate()
    {
        if (transform.position.x<=-10)
        {
            PickUpSpawner.Instance.ReturnToPool(this.gameObject, poolKey);

        }
        moveStrategy?.Move();
    }

    public void SetPoolKey(string poolKey)
    {
        this.poolKey = poolKey;
    }

    public virtual void SetInitialData()
    {
        moveStrategy.SetInitialTransformData();
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IsPickedUp(collision.gameObject.GetComponent<Player>());
            PickUpSpawner.Instance.ReturnToPool(this.gameObject, poolKey);
        }    
    }

    protected virtual void IsPickedUp(Player player)
    {
        AudioManager.Instance.SpawnSoundEmitter(null,"Pickup",transform.position);
    }    
}
