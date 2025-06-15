using UnityEngine;

public class ClearBulletSkill : Skill
{
    [SerializeField] private GameObject effectObj;
    [SerializeField] private float scaleDuration = 0.3f;
    [SerializeField] private float targetScale = 1f;

    


    private bool isScaling = false;
    private float scaleTimer = 0f;

    protected override bool CheckCanUseSkill()
    {
        if (!base.CheckCanUseSkill()) return false;
        return true;
    }

    private void FixedUpdate()
    {
        if (!isScaling || effectObj == null) return;

        scaleTimer += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(scaleTimer / scaleDuration);
        effectObj.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * targetScale, t);

        if (t >= 1f)
        {
            isScaling = false;
            effectObj.SetActive(false);
        }
    }

    private void ClearBullets()
    {
        EnemyBullet[] bullets = GameObject.FindObjectsByType<EnemyBullet>(FindObjectsSortMode.None);

        foreach (var bullet in bullets)
        {
            bullet.Despawn();
        }
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        ClearBullets();
        if (effectObj != null)
        {
            effectObj.SetActive(true);
            effectObj.transform.localScale = Vector3.zero;
            isScaling = true;
            scaleTimer = 0f;
        }
    }
}
