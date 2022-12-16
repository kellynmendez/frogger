using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    [SerializeField] GameObject _visualsToDeactivate;
    [SerializeField] Collider _colliderToDeactivate;
    [SerializeField] int _fireflyScoreIncr = 200;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        ScoreManager sm = other.GetComponent<ScoreManager>();
        if (ph && !ph.GetIfRespawning() && sm)
        {
            sm.IncreaseScore(_fireflyScoreIncr);
            _visualsToDeactivate.SetActive(false);
            _colliderToDeactivate.enabled = false;
        }
    }

    public void ActivateVisualsAndCollider()
    {
        _visualsToDeactivate.SetActive(true);
        _colliderToDeactivate.enabled = true;
    }
}
