using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] EndCollider[] _endColliders;

    [Header("Feedback")]
    [SerializeField] AudioClip _winSFX = null;
    [SerializeField] AudioSource _audioSource;

    private UIController _uiController;

    private void Awake()
    {
        _uiController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        CheckIfFilled();
    }

    private void CheckIfFilled()
    {
        bool filled = true;
        for (int i = 0; i < _endColliders.Length && filled; i++)
        {
            filled = _endColliders[i].GetIfEntered();
        }

        if (filled)
        {
            StartCoroutine(StartWinSequence());
        }
    }

    private IEnumerator StartWinSequence()
    {
        FindObjectOfType<PlayerHealth>().SetIfGameOver(true);
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(2f);

        _uiController.ShowWinScreen();
    }
}
