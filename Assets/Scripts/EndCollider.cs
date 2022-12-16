using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCollider : MonoBehaviour
{
    [SerializeField] int _homeScoreIncr = 50;
    [SerializeField] TMP_Text _endText;
    private ScoreManager _scoreManager;
    private bool _entered = false;

    private void Awake()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        _endText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        PlayerController pc = other.GetComponent<PlayerController>();
        if (ph && pc)
        {
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
        _scoreManager.IncreaseScore(_homeScoreIncr);
        ph.SetRespawning(true);
        yield return new WaitForSeconds(1f);
        ph.ReachedEnd();
        _entered = true;
    }

    public TMP_Text GetEndText()
    {
        return _endText;
    }
}
