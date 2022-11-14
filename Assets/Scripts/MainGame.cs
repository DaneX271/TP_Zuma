using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public Transform[] _nodes;
    [SerializeField] private GameObject _pathParent, _ballsParent, _frog;
    [SerializeField] GameObject[] _prefabBalls;
    GameObject _ballInstance = null;
    Ball _currentBall;
    BallQueue _ballQueue = new BallQueue();
    [SerializeField] private float _instanciationFrequency = 0.3f, _ballSpeed = 2f;
    [SerializeField, Range(1, 100)] private int _numberOfBallsToInstanciate = 10;
    float BallSize;

    void Start()
    {
        BallSize = _prefabBalls[1].GetComponent<SpriteRenderer>().size.x * _prefabBalls[1].transform.localScale.x;
        if(_pathParent) _nodes = _pathParent.GetComponentsInChildren<Transform>();
        Transform _initiateBallTransform = null;

        if (_nodes.Length >= 2) _initiateBallTransform = _nodes[1];

        StartCoroutine(CreateBallQueue(_initiateBallTransform));
    }

    IEnumerator CreateBallQueue(Transform _initiateBallTransform)
    {
        for (int i = 0; i < _numberOfBallsToInstanciate; i++)
        {
            int rand = Random.Range(0, _prefabBalls.Length);
            if (_prefabBalls[rand] && _initiateBallTransform) _ballInstance = GameObject.Instantiate(_prefabBalls[rand], _initiateBallTransform.position, _initiateBallTransform.rotation);

            if (_ballInstance)
            {
                _ballInstance.transform.parent = _ballsParent.transform;
                _currentBall = _ballInstance.GetComponent<Ball>();
                _currentBall.IndexInQueue = i;
                _currentBall.Speed = _ballSpeed;
                _currentBall.UpdateMove(_nodes, BallSize * (float)(_numberOfBallsToInstanciate - i));
                _ballQueue.Balls.Add(_currentBall);
            }

            yield return new WaitForSecondsRealtime(_instanciationFrequency);
        }
    }

    private void FixedUpdate()
    {
        _ballQueue.Update(_nodes);
    }

    void Update()
    {
        FrogLookAtMouse();
        ShootBallFromFrog();
    }

    void ShootBallFromFrog()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject ballToShoot = GameObject.Instantiate(_prefabBalls[Random.Range(0, _prefabBalls.Length)]);
            ballToShoot.transform.position = _frog.transform.position;
            ballToShoot.GetComponent<Ball>().enabled = false;
        }
    }

    private void FrogLookAtMouse()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = worldMousePosition - _frog.transform.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        _frog.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
