using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _negYBounds = 2;
    [SerializeField] float _posYBounds = 15;
    [SerializeField] float _movementDuration = 0.03f;

    private PlayerHealth _playerHealth;
    private Rigidbody _rb;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input if not responding
        if (!_playerHealth.GetIfRespawning() && !_playerHealth.GetIfLost())
        {
            CheckForInput();
        }
        else if (_playerHealth.GetIfLost())
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ReloadLevel();
            }
        }
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (transform.position + Vector3.forward).z <= _posYBounds)
        {
            Vector3 newPos = transform.position;
            newPos.z = (int)(System.Math.Round(transform.position.z, MidpointRounding.AwayFromZero) + 1);
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, true));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (transform.position + Vector3.back).z >= _negYBounds)
        {
            Vector3 newPos = transform.position;
            newPos.z = (int)(System.Math.Round(transform.position.z, MidpointRounding.AwayFromZero) - 1);
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, true));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 newPos = transform.position;
            newPos.x = (int)(System.Math.Round(transform.position.x, MidpointRounding.AwayFromZero) + 1);
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, false));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 newPos = transform.position;
            newPos.x = (int)(System.Math.Round(transform.position.x, MidpointRounding.AwayFromZero) - 1);
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, false));
        }
    }

    private void ReloadLevel()
    {
        // Reloading the level
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private IEnumerator LerpPosition(Transform target, Vector3 from, Vector3 to, float duration, bool forwardBackward)
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

        if (forwardBackward)
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }

        yield break;
    }
}