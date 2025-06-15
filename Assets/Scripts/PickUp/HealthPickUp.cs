using UnityEngine;

public class HealthPickUp : PickUp
{
    protected override void IsPickedUp(Player player)
    {
        base.IsPickedUp(player);
        player.IncreaseHealth();
    }
}
