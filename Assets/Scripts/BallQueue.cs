using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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

    public void InsertAfter(Ball newBall, Ball previousBall)
    {
        int index = Balls.IndexOf(previousBall);

        if (index == -1) throw new System.Exception("Can't find the ball...");

        Balls.Insert(index + 1, newBall);

        newBall.transform.position = previousBall.transform.position;

        for (int i = 0; i < Balls.Count; i++)
        {
            Balls[i].IndexInQueue = i;
        }

        for (int i = 0; i <= index; i++)
        {
            Balls[i].UpdateMove(MainGame.Instance._nodes, MainGame.Instance.BallSize);
        }
    }

    public Ball GetBallAfter(Ball ball)
    {
        int index = Balls.IndexOf(ball);
        if (index == -1) throw new System.Exception("Can't find the ball...");
        if (index == 0) return null;
        else return Balls[index - 1];
    }

    public void CheckCombo(Ball newBall)
    {
        List<Ball> sameColorBalls = new List<Ball>();

        int index = Balls.IndexOf(newBall);

        for (int i = index; i < Balls.Count; i++)
        {
            Ball otherBall = Balls[i];
            if (otherBall.ballColor != newBall.ballColor) break;
            sameColorBalls.Add(otherBall);
        }

        for (int i = index; i >= 0; i--)
        {
            Ball otherBall = Balls[i];
            if (otherBall.ballColor != newBall.ballColor) break;
            sameColorBalls.Add(otherBall);
        }

        if(sameColorBalls.Count >= 3)
        {
            for (int i = sameColorBalls.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(sameColorBalls[i].gameObject);
                Balls.Remove(sameColorBalls[i]);
            }

            for (int i = 0; i < Balls.Count; i++)
            {
                Balls[i].IndexInQueue = i;
            }
        }
    }
}
