using System.Collections.Generic;
using UnityEngine;

public class CompositeMoveStrategy : IMoveStrategy
{
    private List<(IMoveStrategy strategy, float duration)> strategies;
    private int currentIndex = 0;
    private float elapsedTime = 0f;
    private bool isLoop;
    private bool hasEnded = false;

    public bool HasEnded => hasEnded;

    public CompositeMoveStrategy(List<(IMoveStrategy, float)> strategies, bool isLoop)
    {
        this.strategies = strategies;
        this.isLoop = isLoop;
    }

    public void Move()
    {
        if (currentIndex >= strategies.Count)
        {
            currentIndex --;
            return;

        }

        var (currentStrategy, duration) = strategies[currentIndex];
        currentStrategy.Move();
        if (hasEnded ) return;
        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime >= duration)
        {
            currentIndex++;
            elapsedTime = 0f;

            if (currentIndex < strategies.Count)
            {
                strategies[currentIndex].strategy.SetInitialTransformData();
            }
            else if (isLoop)
            {
                currentIndex = 0;
                strategies[currentIndex].strategy.SetInitialTransformData();
            }
            else
            {
                currentIndex--;
                hasEnded = true;
            }
        }
    }

    public void SetInitialTransformData()
    {
        if (strategies.Count > 0)
        {
            currentIndex = 0;
            elapsedTime = 0f;
            hasEnded = false;
            strategies[0].strategy.SetInitialTransformData();
        }
    }
}
