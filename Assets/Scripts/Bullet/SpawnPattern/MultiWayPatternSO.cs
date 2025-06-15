using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MultiWayPattern", menuName = "BulletPatterns/Patterns/MultiWay")]
public class MultiWayPatternSO : SpawnPatternSO
{
    public int bulletCount = 5; 
    public float fireAngleRange = 90f; 
    public override Coroutine Fire(Transform shootPoint, BulletSO bulletSO, MonoBehaviour gameObject)
    {
        return gameObject.StartCoroutine(FireRoutine(shootPoint, bulletSO));
    }

    protected override IEnumerator FireRoutine(Transform shootPoint, BulletSO bulletSO)
    {
        float timer = 0f;
        float angleStep = (fireAngleRange == 360f) ? (fireAngleRange / bulletCount) : (fireAngleRange / (bulletCount - 1));

        while (infiniteDuration || timer < duration)
        {

            float startAngle = (fireAngleRange == 360f) ? 0f : -fireAngleRange / 2f;

            for (int i = 0; i < bulletCount; i++)
                {
                    float angle = startAngle + i * angleStep;
                    Vector2 bulletDirection = Quaternion.Euler(0, 0, angle) * shootPoint.right;

                    BulletSpawner.Instance.SpawnBullet(shootPoint.position, bulletDirection, bulletSO);
                }


            AudioManager.Instance.SpawnSoundEmitter(null, bulletSO.soundKey, shootPoint.position);

            yield return new WaitForSeconds(fireRate);
            if (!infiniteDuration) timer += fireRate;
        }
    }
}
