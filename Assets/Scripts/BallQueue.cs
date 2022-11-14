using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallQueue
{
    public List<Ball> Balls = new List<Ball>();

    public void Update(Transform[] path)
    {
        foreach(Ball ball in Balls)
        {
            ball.UpdateMove(path);
        }
    }
}
