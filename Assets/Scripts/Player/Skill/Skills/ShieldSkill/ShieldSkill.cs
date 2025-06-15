using UnityEngine;

public class ShieldSkill : Skill
{
    [SerializeField] GameObject shieldObj;

    protected override void Start()
    {
        base.Start();
        player.ShieldBreakAction += OnShieldBreak;
    }
    protected override bool CheckCanUseSkill()
    {
        if(!base.CheckCanUseSkill()) return false;
        if(player.isShield) return false;
        
        return true;
    }

    private void OnShieldBreak()
    {
        AudioManager.Instance.SpawnSoundEmitter(null, "ShieldBreak", transform.position);
        shieldObj.SetActive(false);
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        player.isShield = true;
        shieldObj.SetActive(true);
    }
}
