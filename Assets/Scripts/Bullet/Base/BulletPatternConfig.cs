using UnityEngine;

[System.Serializable]
public struct BulletPatternConfig 
{
    public SpawnPatternSO SpawnPatternSO;
    public BulletSO BulletSO;
}

[System.Serializable]
public struct BulletPatternConfigWithCoolDown
{
    public BulletPatternConfig config;
    public float coolDown;
}

[System.Serializable]
public class BulletPatternConfigWithCoolDownSet
{
    public BulletPatternConfigWithCoolDown[] bulletPatternConfigs;
}

[System.Serializable]
public class BulletPatternConfigSet
{
    public BulletPatternConfig[] bulletPatternConfigs;
}



