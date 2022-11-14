using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public Transform[] _nodes;
    [SerializeField] private GameObject _pathParent, _ballsParent;
    [SerializeField] GameObject _ball;
    GameObject _ballInstance = null;
    Ball _currentBall;
    BallQueue _ballQueue = new BallQueue();
    [SerializeField] private float _instanciationFrequency = 0.3f;
    [SerializeField, Range(1, 100)] private int _numberOfBallsToInstanciate = 10;

    IEnumerator Start()
    {
        if(_pathParent) _nodes = _pathParent.GetComponentsInChildren<Transform>();
        Transform _initiateBallTransform = null;

        if (_nodes.Length >= 2) _initiateBallTransform = _nodes[1];

        for (int i = 0; i < _numberOfBallsToInstanciate; i++)
        {
            if (_ball && _initiateBallTransform) _ballInstance = GameObject.Instantiate(_ball, _initiateBallTransform.position, _initiateBallTransform.rotation);

            if (_ballInstance)
            {
                _ballInstance.transform.parent = _ballsParent.transform;
                _currentBall = _ballInstance.GetComponent<Ball>();
                _ballQueue.Balls.Add(_currentBall);
            }

            yield return new WaitForSecondsRealtime(_instanciationFrequency);
        }

        
    }

    void Update()
    {
        _ballQueue.Update(_nodes);
    }
}
