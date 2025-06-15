using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance {  get; private set; }
    private Dictionary<string, Queue<Bullet>> bulletPools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            bulletPools = new Dictionary<string, Queue<Bullet>>();
        }
        else
        {
            Destroy(gameObject);
        }    
            
    }

    public void SpawnBullet (Vector2 position,Vector2 direction,BulletSO bulletSO)
    {
        if (!bulletPools.ContainsKey(bulletSO.keyName))
        {
            bulletPools[bulletSO.keyName] = new Queue<Bullet>();
        }

        Bullet bullet;
        if (bulletPools[bulletSO.keyName].Count > 0)
        {
            bullet = bulletPools[bulletSO.keyName].Dequeue();
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = bulletSO.GetInstance();
        }

        bullet.transform.position = position;
        bullet.transform.right = direction;
        bullet.SetInitialTransformData();
        bullet.SetExistTime(bulletSO.existTime);


    }

    public void DespawnBullet(Bullet bullet, string bulletKey)
    {
        bullet.gameObject.SetActive(false);
        if (!bulletPools.ContainsKey(bulletKey))
        {
            bulletPools[bulletKey] = new Queue<Bullet>();
        }
        bulletPools[bulletKey].Enqueue(bullet);
    }


}


