using UnityEngine;

public class BulletUpgradePickUp : PickUp
{
    protected override void IsPickedUp(Player player)
    {
        base.IsPickedUp(player);
        player.UpgradeBullet();
        
    }
}
