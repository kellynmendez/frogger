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

    [Header("Feedback")]
    [SerializeField] AudioClip _leapSFX = null;
    private AudioSource _audioSource;

    private bool _pause;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Get input if not responding
        if (!_playerHealth.GetIfRespawning() && !_playerHealth.GetIfGameOver() && !_pause)
        {
            CheckForInput();
        }
        else if (_playerHealth.GetIfGameOver())
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ReloadLevel();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void Pause()
    {
        _pause = true;
    }
    public void Unpause()
    {
        _pause = false;
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (transform.position + Vector3.forward).z <= _posYBounds)
        {
            PlayLeapSFX();
            Vector3 newPos = transform.position;
            newPos.z = (int)(System.Math.Round(transform.position.z, MidpointRounding.AwayFromZero) + 1);
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, true));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (transform.position + Vector3.back).z >= _negYBounds)
        {
            PlayLeapSFX();
            Vector3 newPos = transform.position;
            newPos.z = (int)(System.Math.Round(transform.position.z, MidpointRounding.AwayFromZero) - 1);
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, true));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayLeapSFX();
            Vector3 newPos = transform.position;
            newPos.x = transform.position.x + 1;
            StartCoroutine(LerpPosition(transform, transform.position, newPos, _movementDuration, false));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayLeapSFX();
            Vector3 newPos = transform.position;
            newPos.x = transform.position.x - 1;
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

    public void SetPlatformAsParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void UnparentFrog()
    {
        transform.SetParent(null);
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

        yield break;
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PlayLeapSFX()
    {
        _audioSource.PlayOneShot(_leapSFX, _audioSource.volume);
    }
}