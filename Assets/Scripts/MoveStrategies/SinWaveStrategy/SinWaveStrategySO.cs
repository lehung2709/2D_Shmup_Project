using UnityEngine;

[CreateAssetMenu(fileName = "NewSinWaveStrategySO", menuName = "Strategy/SinWaveStrategySO")]
public class SinWaveStrategySO : StraightStrategySO
{
    public float frequency;
    public float amplitude;
    public bool isR2MoveDirect;
    public override IMoveStrategy GetMoveStrategy(Transform transform)
    {
        return new SinWaveStrategy(transform, speed, frequency, amplitude,isR2MoveDirect);
    }
}
