using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPool : MonoBehaviour
{
    // game object pool will hold
    [SerializeField] GameObject _poolObject;
    // number of objects to start with
    [SerializeField] int _poolSize = 5;
    // speed
    [SerializeField] float _speed = 5f;
    // timer interval
    [SerializeField] float _timerInterval = 5f;
    // timer interval
    [SerializeField] float _startDelay = 0f;
    // list of game objects to start with
    List<GameObject> _gameObjects = new List<GameObject>();

    private float _timer = 0;
    private float _timeBenchMark;

    private void Awake()
    {
        _timeBenchMark = _startDelay;
    }

    void Start()
    {
        // create all the objects the pool will use
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject newObj = Instantiate(_poolObject);
            newObj.SetActive(false);
            // add to pool
            _gameObjects.Add(newObj);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBenchMark)
        {
            GameObject logGO = GetPooledObject(transform.position, Quaternion.identity);
            if (logGO)
            {
                Log log = logGO.GetComponent<Log>();
                log.SetSpeed(_speed);
            }
            _timeBenchMark += _timerInterval;
        }
    }

    private GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        // finding an inactive object and activating it
        foreach (GameObject gameObject in _gameObjects)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                return gameObject;
            }
        }
        // If all are active
        return null;
    }
}