using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallQueue
{
    public List<Ball> Balls = new List<Ball>();

    public void Update(Transform[] path)
    {
        if (Balls.Count > 0)
        {
            Balls[Balls.Count - 1].UpdateMove(path);
        }
    }
}
