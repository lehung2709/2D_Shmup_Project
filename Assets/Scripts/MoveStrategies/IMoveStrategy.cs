using UnityEngine;

public interface IMoveStrategy 
{
    public void Move();
    public void SetInitialTransformData();
}


public abstract class MoveStrategySO: ScriptableObject
{
    public virtual IMoveStrategy GetMoveStrategy(Transform transform) {  return null; }

}
