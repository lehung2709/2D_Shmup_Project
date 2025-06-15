using UnityEngine;

public class RocketSkill : Skill
{
    [SerializeField] private GameObject rocketPrefab;

    protected override bool CheckCanUseSkill()
    {
        if(!base.CheckCanUseSkill())return false;

        return true;

    }

    protected override void UseSkill()
    {
        base.UseSkill();
        GameObject rocket = Instantiate(rocketPrefab, this.transform.position, Quaternion.identity);
        AudioManager.Instance.SpawnSoundEmitter(null, "RocketLaunch", transform.position);


    }
}
