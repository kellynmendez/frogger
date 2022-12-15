using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float _speed = 5f;
    private bool _varsSet = false;
    private bool _movingRight = false;
    [SerializeField] Rigidbody _rb;

    private void FixedUpdate()
    {
        if (_varsSet)
        {
            if (_movingRight)
            {
                _rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * _speed);
            }
            else
            {
                _rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * _speed);
            }
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void SetSpeedAndDirection(float speed, bool movingRight)
    {
        _movingRight = movingRight;
        _speed = speed;
        _varsSet = true;
    }
}
