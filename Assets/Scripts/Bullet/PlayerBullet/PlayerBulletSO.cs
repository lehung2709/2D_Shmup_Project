using UnityEngine;

[CreateAssetMenu(fileName ="NewPlayerBulletSO",menuName = "Bullet/PlayerBulletSO")]
public class PlayerBulletSO : BulletSO
{
    public PlayerBullet bulletPrefab;
    public int damage;
    

    public override Bullet GetInstance()
    {
        PlayerBullet bullet = Instantiate(bulletPrefab);
        bullet.SetMoveStrategy(bulletStrategySO.GetMoveStrategy(bullet.transform));
        bullet.SetBulletkey(keyName);
        bullet.SetDamage(damage);
        return bullet;
    }    
}
