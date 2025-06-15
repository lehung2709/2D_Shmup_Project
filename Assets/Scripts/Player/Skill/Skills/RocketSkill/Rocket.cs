using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Vector2 desPos;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 80; 
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private string explosionEffectKey = "BigExplode";
    [SerializeField] private string explosionSoundKey = "Explosion";




    private void Start()
    {
        Vector2 direction = desPos - (Vector2)transform.position;
        transform.right = direction.normalized;
    }

   
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, desPos, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, desPos) < 0.1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        AudioManager.Instance.SpawnSoundEmitter(null, explosionSoundKey, transform.position);
        EffectsSpawner.Instance.SpawnEffect(explosionEffectKey, transform.position, Quaternion.identity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);

    }

    
}
