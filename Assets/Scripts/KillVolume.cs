using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null && !_playerHealth.GetIfRespawning())
        {
            playerHealth.Kill();
            Debug.Log("killed from volume");
        }
    }
}