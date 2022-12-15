using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    [SerializeField] int _homeScoreIncr = 50;
    private ScoreManager _scoreManager;
    private bool _entered = false;

    private void Awake()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph)
        {
            StartCoroutine(PlayerReachedEnd(ph));
        }
    }

    public bool GetIfEntered()
    {
        return _entered;
    }

    private IEnumerator PlayerReachedEnd(PlayerHealth ph)
    {
        _scoreManager.IncreaseScore(_homeScoreIncr);
        yield return new WaitForSeconds(1f);
        _entered = true;
        ph.ReachedEnd();
    }    
}
