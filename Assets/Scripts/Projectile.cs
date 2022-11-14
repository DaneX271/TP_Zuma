using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 _direction;
    private float _speed = 1;
    private bool _destroyDone = false;
    public void Initialize(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destroyDone) return;

        Ball otherBall;

        if(collision.TryGetComponent<Ball>(out otherBall))
        {
            _destroyDone = true;

            Ball ball = GetComponent<Ball>();
            ball.enabled = true;
            ball.PathIndex = otherBall.PathIndex;

            MainGame.Instance.BallQueue.InsertAfter(ball, otherBall);
            GameObject.Destroy(this);

            MainGame.Instance.BallQueue.CheckCombo(ball);
        }
        
    }
}
