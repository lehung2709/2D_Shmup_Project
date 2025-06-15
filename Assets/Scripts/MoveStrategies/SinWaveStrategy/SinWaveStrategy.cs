using UnityEngine;

public class SinWaveStrategy : StraightStrategy
{
   
    protected float frequency;
    protected float amplitude;
    protected bool isR2MoveDirect;
    protected float elapsedTime = 0.0f;
    protected Vector2 initialUpDirection;
    protected Vector3 initialPos;

    public SinWaveStrategy(Transform transform, float speed,float frequency,float amplitude,bool isR2MoveDirect) : base(transform, speed)
    {
        this.frequency = frequency;
        this.amplitude = amplitude;
        this.isR2MoveDirect = isR2MoveDirect;
        
    }

    public override void Move()
    {
        elapsedTime += Time.fixedDeltaTime;

        Vector3 rightOffset = initialRightDirection * speed * elapsedTime;
        float sinWaveOffset = Mathf.Sin(elapsedTime * frequency) * amplitude;
        Vector3 upOffset = initialUpDirection * sinWaveOffset;

        Vector3 newPosition = initialPos + rightOffset + upOffset;
        Vector3 movementDirection = newPosition - _transform.position; 

        _transform.position = newPosition;

        if (isR2MoveDirect && movementDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg; 
            _transform.rotation = Quaternion.Euler(0, 0, angle); 
        }


    }

    public override void SetInitialTransformData()
    {
        base.SetInitialTransformData();
        initialUpDirection = new Vector2(-initialRightDirection.y, initialRightDirection.x);
        initialPos = _transform.position;
        elapsedTime = 0.0f;
    }
}

