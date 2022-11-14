using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private int _pathIndex = 1;
    public float Speed = 2;

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
    }
}
