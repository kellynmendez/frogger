using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    [SerializeField] Renderer[] _pads;
    [SerializeField] GameObject _artToDeactivate;
    [SerializeField] Material _matSource;
    [SerializeField] float _intervalUpper = 12f;
    [SerializeField] float _intervalLower = 6f;
    [SerializeField] float _timeInterval = 1f;
    [SerializeField] Color _oldColor;
    [SerializeField] Color _newColor;

    private Collider _collider;
    private float _speed = 5f;
    private bool _varsSet = false;
    private Vector3 _targetPos;
    private float _timeValue;
    private Material _lilypadMat;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _timeValue = Random.Range(_intervalLower, _intervalUpper);
        _lilypadMat = new Material(_matSource);
        _lilypadMat.color = _oldColor;

        foreach(Renderer ren in _pads)
        {
            ren.material = _lilypadMat;
        }
    }

    private void Update()
    {
        if (_timeValue > 0)
        {
            _timeValue -= Time.deltaTime;
        }
        else
        {
            _timeValue = Random.Range(_intervalLower, _intervalUpper);
            StartCoroutine(SinkLilyPad(_lilypadMat, _oldColor, _newColor, _timeInterval));
        }

        if (_varsSet)
        {
            var step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.UnparentFrog();
            pc.SetPlatformAsParent(transform);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(SetGOInactive());
        }
    }

    public void SetSpeedAndTarget(float speed, Vector3 target)
    {
        _lilypadMat.color = _oldColor;
        _speed = speed;
        _targetPos = target;
        _varsSet = true;
    }

    private IEnumerator SinkLilyPad(Material mat, Color from, Color to, float duration)
    {
        // Lerping the color
        mat.color = from;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            mat.color = Color.Lerp(from, to, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mat.color = to;

        // Deactivating lily pad
        _artToDeactivate.SetActive(false);
        _collider.enabled = false;

        yield return new WaitForSeconds(1.5f);

        // Reactivating lily pad
        _lilypadMat.color = _oldColor;
        _artToDeactivate.SetActive(true);
        _collider.enabled = true;


        yield break;
    }

    private IEnumerator SetGOInactive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        yield break;
    }

    public void ActivateArtAndCollider()
    {
        _lilypadMat.color = _oldColor;
        _artToDeactivate.SetActive(true);
        _collider.enabled = true;
    }
}
