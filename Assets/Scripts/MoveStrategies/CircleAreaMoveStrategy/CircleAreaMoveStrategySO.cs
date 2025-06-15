using UnityEngine;

[CreateAssetMenu(fileName = "CircleAreaMoveStrategySO", menuName = "Strategy/CircleAreaMoveStrategySO")]
public class CircleAreaMoveStrategySO : MoveStrategySO
{
    public float radius = 3f;
    public float speed = 2f;

    public override IMoveStrategy GetMoveStrategy(Transform transform)
    {
        return new CircleAreaMoveStrategy(transform, radius, speed);
    }
}
