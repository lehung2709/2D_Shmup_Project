using UnityEngine;

public class PlayerBullet : Bullet
{
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            BulletSpawner.Instance.DespawnBullet(this, bulletKey);
            EffectsSpawner.Instance.SpawnEffect(effectNameKey, transform.position, Quaternion.identity);

        }

    }
    
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }    


}
