using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private float _speed = 5f;
    private bool _varsSet = false;
    private Vector3 _targetPos;
    private GameObject _ladyFrog = null;

    private void Update()
    {
        if (_varsSet)
        {
            var step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, step);
        }

        if (transform.position == _targetPos)
        {
            gameObject.SetActive(false);
            if (_ladyFrog != null)
            {
                Destroy(_ladyFrog);
                _ladyFrog = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.SetPlatformAsParent(transform);
        }
    }

    public void SetSpeedAndTarget(float speed, Vector3 target)
    {
        _speed = speed;
        _targetPos = target;
        _varsSet = true;
    }

    public void GiveOutLadyFrog(GameObject go)
    {
        _ladyFrog = go;
        _ladyFrog.transform.SetParent(transform);
        _ladyFrog.transform.position = transform.position;
    }
}
