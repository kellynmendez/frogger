using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float _speed = 5f;
    private bool _varsSet = false;
    private Vector3 _targetPos;

    private void Update()
    {
        if (_varsSet)
        {
            var step = _speed * Time.deltaTime;
            transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, _targetPos, step);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void SetSpeedAndDirection(float speed, Vector3 target)
    {
        _targetPos = target;
        _speed = speed;
        _varsSet = true;
    }
}
