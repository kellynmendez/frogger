using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] EndCollider[] _endColliders;

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
            Debug.Log("WIN");
            Time.timeScale = 0;
        }
    }
}
