using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
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
            _rb.MovePosition(transform.position  + Vector3.right * Time.deltaTime * _speed);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
        _varsSet = true;
    }
}
