using UnityEngine;

public class SkillPointPickUp : PickUp
{
    protected override void IsPickedUp(Player player)
    {
        base.IsPickedUp(player);
        player.IncreaseSkillPoint();
    }
}
