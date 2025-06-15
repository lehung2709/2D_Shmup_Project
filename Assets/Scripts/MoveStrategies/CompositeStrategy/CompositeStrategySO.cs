using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StrategyDurationPair
{
    public MoveStrategySO strategySO;
    public float duration;
}


[CreateAssetMenu(fileName = "NewCompositeStrategySO", menuName = "Strategy/CompositeStrategySO")]
public class CompositeStrategySO : MoveStrategySO
{
    public List<StrategyDurationPair> strategies;
    public bool isLoop;

    public override IMoveStrategy GetMoveStrategy(Transform transform)
    {
        List<(IMoveStrategy, float)> runtimeStrategies = new();
        foreach (var pair in strategies)
        {
            if (pair.strategySO != null)
            {
                IMoveStrategy strategy = pair.strategySO.GetMoveStrategy(transform);
                runtimeStrategies.Add((strategy, pair.duration));
            }
        }

        return new CompositeMoveStrategy(runtimeStrategies,isLoop);
    }
}
