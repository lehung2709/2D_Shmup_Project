using UnityEngine;

public class AcceleratedStraightStrategy: StraightStrategy
{
    private float initialSpeed;
    private float acceleration;
    private float minSpeed;
    private float maxSpeed;

    public AcceleratedStraightStrategy(Transform transform, float initialSpeed, float acceleration, float minSpeed, float maxSpeed)
        : base(transform, Mathf.Clamp(initialSpeed, minSpeed, maxSpeed))
    {
        this.initialSpeed = initialSpeed;
        this.acceleration = acceleration;
        this.minSpeed = minSpeed;
        this.maxSpeed = maxSpeed;
    }

    public override void Move()
    {
        speed += acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        base.Move();
    }

    public override void SetInitialTransformData()
    {
        base.SetInitialTransformData();
        speed = initialSpeed;
    }
}
