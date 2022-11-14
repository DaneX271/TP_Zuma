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

    void Start()
    {
        if(_pathParent) _nodes = _pathParent.GetComponentsInChildren<Transform>();
        Transform _initiateBallTransform = null;

        if (_nodes.Length >= 2) _initiateBallTransform = _nodes[1];

        if (_ball && _initiateBallTransform) _ballInstance = GameObject.Instantiate(_ball, _initiateBallTransform.position, _initiateBallTransform.rotation);
        
        if (_ballInstance)
        {
            _ballInstance.transform.parent = _ballsParent.transform;
            _currentBall = _ballInstance.GetComponent<Ball>();
        }
    }

    void Update()
    {
        _currentBall.UpdateMove(_nodes);
    }
}
