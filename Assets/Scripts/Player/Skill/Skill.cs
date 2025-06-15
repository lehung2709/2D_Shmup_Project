using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] private Sprite skillSprite;
    [SerializeField] private string skillDes;

    protected virtual void Start()
    {
        PlayerUI.Instance.SetSkillDes(skillSprite,skillDes);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(CheckCanUseSkill()) UseSkill();
        }
    }

    protected virtual bool CheckCanUseSkill()
    {
        if(!player.IsHaveSkillPoint()) return false;
        return true;
    }
    protected virtual void UseSkill() 
    { 
        player.UseSkill();
        AudioManager.Instance.SpawnSoundEmitter(null, "UseSkillSound", transform.position);
    }
    

}
