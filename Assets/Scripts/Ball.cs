using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallColor
{
    Blue,
    Green,
    Red,
    Yellow
}

public class Ball : MonoBehaviour
{
    public BallColor ballColor;

    public int _pathIndex = 1;
    public float Speed;
    private List<Ball> _neighbourBalls = new List<Ball>();
    public int IndexInQueue = 0;

    public int PathIndex { get => _pathIndex; set => _pathIndex = value; }

    public void UpdateMove(Transform[] path)
    {
        if (_pathIndex >= path.Length) return;

        Vector3 targetPosition = path[_pathIndex].position;
        Vector3 direction = targetPosition - transform.position;

        float moveStep = Time.deltaTime * Speed;
        float distance = Vector3.Distance(targetPosition, transform.position);

        while (distance <= moveStep)
        {
            _pathIndex++;

            if (_pathIndex >= path.Length) return;

            targetPosition = path[_pathIndex].position;
            direction = targetPosition - transform.position;

            moveStep = Time.deltaTime * Speed;
            distance = Vector3.Distance(targetPosition, transform.position);
        }

        direction.Normalize();
        transform.position += moveStep * direction;

        //PushOtherBalls(path);
        Ball ballAfter = MainGame.Instance.BallQueue.GetBallAfter(this); 
        if (ballAfter)
        {
            if(Vector3.Distance(transform.position, ballAfter.transform.position) < MainGame.Instance.BallSize)
            {
                ballAfter.UpdateMove(path);
            }
        }
    }

    public void UpdateMove(Transform[] path, float distance)
    {
        while(distance > 0)
        {
            distance -= Time.deltaTime * Speed;
            UpdateMove(path);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Ball otherBall;
    //    if(collision.TryGetComponent<Ball>(out otherBall))
    //    {
    //        _neighbourBalls.Add(otherBall);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Ball otherBall;
    //    if (collision.TryGetComponent<Ball>(out otherBall))
    //    {
    //        _neighbourBalls.Remove(otherBall);
    //    }
    //}

    //public void PushOtherBalls(Transform[] path)
    //{
    //    foreach (Ball ball in _neighbourBalls)
    //    {
    //        if(ball.IndexInQueue < IndexInQueue)
    //        {
    //            ball.UpdateMove(path);
    //        }
    //    }
    //}
}
