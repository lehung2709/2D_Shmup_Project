using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyBulletSO", menuName = "Bullet/EnemyBulletSO")]

public class EnemyBulletSO : BulletSO
{
    public EnemyBullet bulletPrefab;

    public override Bullet GetInstance()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.SetMoveStrategy(bulletStrategySO.GetMoveStrategy(bullet.transform));
        bullet.SetBulletkey(keyName);
        return bullet;
    }
}
