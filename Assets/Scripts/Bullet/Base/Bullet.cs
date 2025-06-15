using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected string effectNameKey= "BulletImpact";
    private IMoveStrategy bulletStrategy;
    protected string bulletKey;
    private float existTime;
    private float timer = 0.0f;
    
    public void SetMoveStrategy(IMoveStrategy bulletStrategy)
    {
        this.bulletStrategy = bulletStrategy;
    }

    public void SetBulletkey(string bulletKey)
    {
        this.bulletKey = bulletKey;
    }

    public void SetExistTime(float existTime)
    {
        timer = 0;
        this.existTime = existTime;
    } 
        
    public void SetInitialTransformData()
    {
        bulletStrategy.SetInitialTransformData();
    }    

    private void FixedUpdate()
    {
        bulletStrategy?.Move();
        timer += Time.fixedDeltaTime;
        if (timer > existTime)
        {
            BulletSpawner.Instance.DespawnBullet(this, bulletKey);
            EffectsSpawner.Instance.SpawnEffect(effectNameKey, transform.position, Quaternion.identity);

        }


    }
    
}
