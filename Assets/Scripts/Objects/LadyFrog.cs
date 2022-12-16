using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyFrog : MonoBehaviour
{
    [SerializeField] GameObject _visualsToDeactivate;
    [SerializeField] Collider _colliderToDeactivate;
    [SerializeField] int _ladyFrogScoreIncr = 200;
    [SerializeField] float _leapInterval = 2f;
    [SerializeField] float _leapDuration = 0.1f;

    private float _timeValue;
    ScoreManager _scoreManager;
    private bool _leapLeft = false;
    private bool _caught = false;

    private void Awake()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _timeValue = _leapInterval;
    }

    private void Update()
    {
        if (_timeValue > 0)
        {
            _timeValue -= Time.deltaTime;
        }
        else
        {
            if (!_caught)
            {
                _timeValue = _leapInterval;
                if (_leapLeft)
                {
                    StartCoroutine(LerpPosition(transform, transform.position, transform.position += (Vector3.left * 0.5f), _leapDuration));
                    _leapLeft = false;
                }
                else
                {
                    StartCoroutine(LerpPosition(transform, transform.position, transform.parent.transform.position, _leapDuration));
                    _leapLeft = true;
                }
            }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph && !ph.GetIfRespawning())
        {
            _caught = true;
            transform.SetParent(ph.transform);
            ph.SetLadyFrog(gameObject);
        }
    }

    public void ReachedEnd()
    {
        _scoreManager.IncreaseScore(_ladyFrogScoreIncr);
        _scoreManager.LadyFrogReachedEnd();
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
