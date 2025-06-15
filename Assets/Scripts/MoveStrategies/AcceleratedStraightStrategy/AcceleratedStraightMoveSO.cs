using UnityEngine;

[CreateAssetMenu(fileName = "NewAcceleratedStraightStrategySO",menuName = "Strategy/AcceleratedStraightStrategySO")]
public class AcceleratedStraightMoveSO : MoveStrategySO
{
    [SerializeField] private float initialSpeed = 2f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 10f;

    public override IMoveStrategy GetMoveStrategy(Transform transform)
    {
        return new AcceleratedStraightStrategy(transform, initialSpeed, acceleration, minSpeed, maxSpeed);
    }
}
