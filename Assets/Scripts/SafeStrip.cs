using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeStrip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            pc.UnparentFrog();
        }
    }
}
