using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    private float _speed = 5f;
    private bool _varsSet = false;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_varsSet)
        {
            _rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * _speed);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(SetGOInactive());
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
        _varsSet = true;
    }

    private IEnumerator SetGOInactive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        yield break;
    }
}
