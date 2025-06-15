using UnityEngine;

[CreateAssetMenu(fileName = "NewStraightStrategySO", menuName = "Strategy/StraightStrategySO")]
public class StraightStrategySO : MoveStrategySO
{
    public float speed;
    public override IMoveStrategy GetMoveStrategy(Transform transform)
    {
        return new StraightStrategy(transform, speed);
    }
}
