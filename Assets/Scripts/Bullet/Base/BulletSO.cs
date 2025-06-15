using UnityEngine;

public class BulletSO : ScriptableObject
{
    public string keyName;
    public  MoveStrategySO bulletStrategySO;
    public float existTime;
    public string soundKey;
    public virtual Bullet GetInstance() { return null; }

}
