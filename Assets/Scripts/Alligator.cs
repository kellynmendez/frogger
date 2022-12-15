using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alligator : MonoBehaviour
{
    [SerializeField] Collider _colliderToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph && !ph.GetIfRespawning())
        {
            ph.Kill();
            _colliderToDeactivate.enabled = false;
            Debug.Log("killed from alligator");
        }
    }

    public void ActivateCollider()
    {
        _colliderToDeactivate.enabled = true;
    }
}
