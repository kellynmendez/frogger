using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject _visualsToDeactivate;
    [SerializeField] GameObject _playerClonePrefab;
    [SerializeField] Material _frogBody;
    [SerializeField] Material _frogParts;

    private bool _respawning = false;
    private bool _gameOver = false;
    private int _numLives = 3;
    private Color _ogBodyColor;
    private Color _ogPartsColor;
    private Vector3 _startPosition;
    private Rigidbody _rbToFreeze;
    private Collider _colliderToDeactivate;
    private Timer _timer;
    private UIController _uiController;
    private PlayerController _playerController;
    private GameObject _ladyFrog = null;
    private ScoreManager _scoreManager;

    private void Awake()
    {
        _ogBodyColor = _frogBody.color;
        _ogPartsColor = _frogParts.color;
        _startPosition = transform.position;
        _rbToFreeze = GetComponent<Rigidbody>();
        _colliderToDeactivate = GetComponent<Collider>();
        _timer = FindObjectOfType<Timer>();
        _uiController = FindObjectOfType<UIController>();
        _playerController = GetComponent<PlayerController>();
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void Kill()
    {
        // Setting respawn flag
        _respawning = true;
        // Decrementing number of lives
        _numLives--;
        // Pausing timer
        _timer.Pause();

        if (_ladyFrog != null)
        {
            Destroy(_ladyFrog);
            _ladyFrog = null;
        }

        Debug.Log($"KILLED : {_numLives}");
        if (_numLives == 0)
        {
            StartCoroutine(KillSequence(true));
        }
        else
        {
            StartCoroutine(KillSequence(false));
        }
    }

    public void ReachedEnd()
    {
        _respawning = true;
        if (_ladyFrog != null)
        {
            _ladyFrog.GetComponent<LadyFrog>().ReachedEnd();
            Destroy(_ladyFrog);
            _ladyFrog = null;
        }
        StartCoroutine(CloneAndRespawn());
    }

    private void Lose()
    {
        Debug.Log("lost");
        _gameOver = true;
        Time.timeScale = 0;
    }

    private void Win()
    {

    }

    public bool GetIfRespawning()
    {
        return _respawning;
    }

    public bool GetIfLost()
    {
        return _gameOver;
    }

    private IEnumerator KillSequence(bool lost)
    {
        _playerController.UnparentFrog();

        // Freeze constraints
        _rbToFreeze.constraints = RigidbodyConstraints.FreezePosition;
        _rbToFreeze.velocity = Vector3.zero;
        _rbToFreeze.useGravity = false;
        _colliderToDeactivate.enabled = false;

        // Dead color change sequence
        MakeFrogRed();
        yield return new WaitForSeconds(0.2f);
        MakeFrogColorNormal();
        yield return new WaitForSeconds(0.2f);
        MakeFrogRed();
        yield return new WaitForSeconds(0.2f);
        MakeFrogColorNormal();
        yield return new WaitForSeconds(0.2f);
        MakeFrogRed();
        yield return new WaitForSeconds(0.5f);

        // Deactivating visuals
        _visualsToDeactivate.SetActive(false);
        MakeFrogColorNormal();

        if (lost)
        {
            Lose();
        }
        else
        {
            // Respawning
            yield return new WaitForSeconds(1f);
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _visualsToDeactivate.SetActive(true);
            _colliderToDeactivate.enabled = true;
            _rbToFreeze.constraints = RigidbodyConstraints.FreezeRotation;
            _rbToFreeze.useGravity = true;
        }

        // Setting respawn flag false
        _respawning = false;
        // Reset timer
        _timer.ResetTimer();
        _timer.Unpause();
        // Decrease frog life images
        _uiController.DecreaseLives();

        yield break;
    }

    private IEnumerator CloneAndRespawn()
    {
        _playerController.UnparentFrog();

        // Deactivating visuals
        transform.rotation = Quaternion.identity;
        _visualsToDeactivate.SetActive(false);
        Vector3 clonePos = transform.position;
        transform.position = _startPosition;
        // Creating clone
        GameObject clone = Instantiate(_playerClonePrefab);
        clone.transform.position = clonePos;

        // Respawning
        yield return new WaitForSeconds(1f);
        _visualsToDeactivate.SetActive(true);
        _rbToFreeze.constraints = RigidbodyConstraints.FreezeRotation;

        // Reset timer
        _timer.ResetTimer();
        _timer.Unpause();

        _respawning = false;
    }

    private void MakeFrogRed()
    {
        _frogBody.color = Color.red;
        _frogParts.color = Color.red;
    }

    private void MakeFrogColorNormal()
    {
        _frogBody.color = _ogBodyColor;
        _frogParts.color = _ogPartsColor;
    }

    public void SetRespawning(bool respawning)
    {
        _respawning = respawning;
    }

    public void SetLadyFrog(GameObject lf)
    {
        if (_ladyFrog == null)
        {
            _ladyFrog = lf;
            _ladyFrog.transform.position = transform.position;
            _ladyFrog.transform.position -= new Vector3(0, 0.6f, 0);
        }
        else
        {
            Destroy(lf);
        }
    }

    public bool GetIfLadyFrog()
    {
        if (_ladyFrog != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartBonusLadyFrogUI(EndCollider endCol)
    {
        _scoreManager.LadyFrogReachedEnd(endCol);
    }
}
