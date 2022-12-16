using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCollider : MonoBehaviour
{
    [SerializeField] int _homeScoreIncr = 50;
    [SerializeField] TMP_Text _endText;
    [Header("Feedback")]
    [SerializeField] AudioClip _homeSFX = null;
    private AudioSource _audioSource;
    private ScoreManager _scoreManager;
    private bool _entered = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _endText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            ph.SetRespawning(true);
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.UnparentFrog();
            StartCoroutine(PlayerReachedEnd(ph));
            if (ph.GetIfLadyFrog())
            {
                ph.StartBonusLadyFrogUI(this);
            }
        }
    }

    public bool GetIfEntered()
    {
        return _entered;
    }

    private IEnumerator PlayerReachedEnd(PlayerHealth ph)
    {
        PlayHomeSFX();
        _scoreManager.IncreaseScore(_homeScoreIncr);
        yield return new WaitForSeconds(1f);
        ph.ReachedEnd();
        _entered = true;
    }

    public TMP_Text GetEndText()
    {
        return _endText;
    }

    private void PlayHomeSFX()
    {
        if (_audioSource != null && _homeSFX != null)
        {
            _audioSource.PlayOneShot(_homeSFX, _audioSource.volume);
        }
    }
}
