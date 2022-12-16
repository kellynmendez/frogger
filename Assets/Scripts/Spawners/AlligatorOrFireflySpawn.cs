using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorOrFireflySpawn : MonoBehaviour
{
    [SerializeField] GameObject _alligatorPrefab;
    [SerializeField] GameObject _fireflyPrefab;
    [SerializeField] float _interval = 6f;
    [SerializeField] float _timeIntoView = 1f;
    [SerializeField] EndCollider[] _endPositionsArray;

    private EndCollider _currentCollider;
    private PlayerHealth _playerHealth;
    private List<EndCollider> _endPositionsList;
    private Alligator _alligator;
    private Firefly _firefly;
    private GameObject _currentSpawn;
    private float _timeValue;
    private bool _onAlligator = true;

    private void Awake()
    {
        _endPositionsList = new List<EndCollider>();
        foreach(EndCollider go in _endPositionsArray)
        {
            _endPositionsList.Add(go);
        }

        _alligator = Instantiate(_alligatorPrefab).GetComponent<Alligator>();
        _firefly = Instantiate(_fireflyPrefab).GetComponent<Firefly>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _timeValue = _interval;
    }

    private void Start()
    {
        _alligator.gameObject.SetActive(false);
        _firefly.gameObject.SetActive(false);

        _currentSpawn = _alligator.gameObject;
    }

    private void Update()
    {
        for (int i = 0; i < _endPositionsList.Count; i++)
        {
            if (_endPositionsList[i].GetIfEntered())
            {
                _endPositionsList.RemoveAt(i);
                i--;
            }
        }

        if (_timeValue > 0)
        {
            _timeValue -= Time.deltaTime;
        }
        else
        {
            SetAlligatorActiveOrUnactive();
            _timeValue = _interval;
        }

        
    }

    private void SetAlligatorActiveOrUnactive()
    {
        if (_currentSpawn.activeSelf && !_playerHealth.GetIfRespawning())
        {
            _currentSpawn.SetActive(false);
            _currentCollider.gameObject.SetActive(true);
            if (_onAlligator)
            {
                _currentSpawn = _firefly.gameObject;
                _firefly.ActivateVisualsAndCollider();
                _onAlligator = false;
            }
            else
            {
                _currentSpawn = _alligator.gameObject;
                _alligator.ActivateCollider();
                _onAlligator = true;
            }
        }
        else if (!_currentSpawn.activeSelf)
        {
            if (_endPositionsList.Count > 0)
            {
                int index = Random.Range(0, _endPositionsList.Count);
                Vector3 endPos = _endPositionsList[index].transform.position;
                _currentCollider = _endPositionsList[index];
                if (_onAlligator)
                {
                    _currentCollider.gameObject.SetActive(false);
                }
                _currentSpawn.transform.position = endPos - (Vector3.right * 2); // Starts two units over
                _currentSpawn.SetActive(true);
                StartCoroutine(LerpPosition(_currentSpawn.transform,
                                            _currentSpawn.transform.position,
                                            endPos,
                                            _timeIntoView));
            }
        }
    }

    private IEnumerator LerpPosition(Transform target, Vector3 from, Vector3 to, float duration)
    {
        // initial value
        target.position = from;

        // animate value
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            target.position = Vector3.Lerp(from, to, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // final value
        target.position = to;

        yield break;
    }
}
