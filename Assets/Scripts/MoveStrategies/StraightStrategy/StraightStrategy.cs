using UnityEngine;

public class StraightStrategy : IMoveStrategy
{
    protected Transform _transform;
    protected float speed;
    protected Vector2 initialRightDirection;


    public StraightStrategy(Transform transform, float speed)
    {
        _transform = transform;
        this.speed = speed;
    }

    public virtual void Move()
    {
        _transform.Translate(initialRightDirection * speed * Time.fixedDeltaTime,Space.World);
    }

    public virtual void SetInitialTransformData()
    {
        initialRightDirection=_transform.right;
    }
}

