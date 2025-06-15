using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "WavingPattern", menuName = "BulletPatterns/Patterns/WavingAngle")]
public class WavingPatternSO : SpawnPatternSO
{
    public float angleRange = 30f; 
    public float frequency = 2f;  

    public override Coroutine Fire(Transform shootPoint, BulletSO bulletSO, MonoBehaviour gameObject)
    {
        return gameObject.StartCoroutine(FireRoutine(shootPoint, bulletSO));
    }

    protected override IEnumerator FireRoutine(Transform shootPoint, BulletSO bulletSO)
    {
        float timer = 0f;
        float time = 0f; 

        while (infiniteDuration || timer < duration)
        {
            float angleOffset = Mathf.Sin(time * frequency) * angleRange;
            Vector2 newDirection = RotateVector(shootPoint.right, angleOffset);

            BulletSpawner.Instance.SpawnBullet(shootPoint.position, newDirection, bulletSO);

            AudioManager.Instance.SpawnSoundEmitter(null, bulletSO.soundKey, shootPoint.position);

            yield return new WaitForSeconds(fireRate);
            timer += fireRate;
            time += fireRate; 
        }
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        return (Vector2)(Quaternion.Euler(0, 0, angle) * new Vector3(vector.x, vector.y, 0));
    }
}
