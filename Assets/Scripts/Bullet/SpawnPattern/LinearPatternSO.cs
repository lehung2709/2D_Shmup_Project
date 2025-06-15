using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LinearPattern", menuName = "BulletPatterns/Patterns/Linear")]
public class LinearPatternSO : SpawnPatternSO
{
    public int bulletCount = 1;
    public float yOffset=0;
    public float xOffset=0;
    
    public override Coroutine Fire(Transform shootPoint, BulletSO bulletSO,MonoBehaviour gameObject)
    {
        Coroutine coroutine= gameObject.StartCoroutine(FireRoutine(shootPoint, bulletSO));
        return coroutine;
    }

    protected override IEnumerator FireRoutine(Transform shootPoint, BulletSO bulletSO)
    {
        float timer = 0f;

        float yOffsetShifter = (bulletCount % 2 == 0) ? yOffset / 2f : 0f;// If the bullet count is even, apply a small vertical shift to avoid an empty gap in the center



        while (infiniteDuration || timer < duration)
        {
            Vector2 perpendicular = new Vector2(-shootPoint.right.y, shootPoint.right.x).normalized;

            for (int i = 0; i < bulletCount/2; i++)
            {
                Vector3 spawnPoint = shootPoint.position + (Vector3)shootPoint.right * i * xOffset + (Vector3)perpendicular * ((bulletCount/2) - i) * yOffset + new Vector3(0f, -yOffsetShifter, 0f);
                BulletSpawner.Instance.SpawnBullet(spawnPoint, shootPoint.right, bulletSO);
            }
            for (int i = 0; i < bulletCount / 2; i++)
            { 
                Vector3 spawnPoint = shootPoint.position + (Vector3)shootPoint.right * i * xOffset + (Vector3)perpendicular * -((bulletCount / 2) - i) * yOffset+new Vector3(0f, yOffsetShifter, 0f);
                BulletSpawner.Instance.SpawnBullet(spawnPoint, shootPoint.right, bulletSO);
            }
            if(bulletCount%2==1)
            BulletSpawner.Instance.SpawnBullet(shootPoint.position +(Vector3)shootPoint.right * ((bulletCount / 2)+1)*xOffset, shootPoint.right, bulletSO);

            AudioManager.Instance.SpawnSoundEmitter(null,bulletSO.soundKey, shootPoint.position);

            yield return new WaitForSeconds(fireRate);
            timer += fireRate;
        }
    }
}
