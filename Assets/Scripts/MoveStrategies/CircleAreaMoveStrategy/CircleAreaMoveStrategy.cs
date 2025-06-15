using UnityEngine;

public class CircleAreaMoveStrategy : IMoveStrategy
{
    private Transform _transform;
    private Vector3 _center;
    private float _radius;
    private float _speed;

    private Vector3 _targetPoint;
    private float _reachThreshold = 0.1f;

    public CircleAreaMoveStrategy(Transform transform, float radius, float speed)
    {
        _transform = transform;
        _radius = radius;
        _speed = speed;
    }

    public void SetInitialTransformData()
    {
        _center = _transform.position;
        PickNewTargetPoint();
    }

    public void Move()
    {
        Vector3 direction = (_targetPoint - _transform.position).normalized;
        _transform.position += direction * _speed * Time.fixedDeltaTime;

        if (Vector3.Distance(_transform.position, _targetPoint) < _reachThreshold)
        {
            PickNewTargetPoint();
        }
    }

    private void PickNewTargetPoint()
    {
        Vector2 randomPoint = Random.insideUnitCircle * _radius;
        _targetPoint = _center + new Vector3(randomPoint.x, randomPoint.y, 0);
    }
}
