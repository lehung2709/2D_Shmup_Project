using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().TakeDamage())
            {
                BulletSpawner.Instance.DespawnBullet(this, bulletKey);
                EffectsSpawner.Instance.SpawnEffect(effectNameKey, transform.position, Quaternion.identity);

            }


        }
    }

    public void Despawn()
    {
        BulletSpawner.Instance.DespawnBullet(this, bulletKey);
        EffectsSpawner.Instance.SpawnEffect(effectNameKey, transform.position, Quaternion.identity);
    }
    

}
