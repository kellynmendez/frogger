using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float _startTimeValue = 15;

    private PlayerHealth _playerHealth;
    private Slider _timeSlider;
    private float _timeValue;
    private bool _timerOut = false;
    private bool _pause = false;

    private void Awake()
    {
        _timeSlider = GetComponent<Slider>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _timeValue = _startTimeValue;
    }

    void Update()
    {
        if (_timeValue > 0 && !_pause)
        {
            _timeValue -= Time.deltaTime;
        }
        else if (_timeValue <= 0 && !_timerOut)
        {
            _timerOut = true;
            _playerHealth.Kill();
            Debug.Log("killed from timer");
        }

        UpdateSliderValue(_timeValue / _startTimeValue);
    }

    private void UpdateSliderValue(float timeToDisplay)
    {
        _timeSlider.value = timeToDisplay;
    }

    public void ResetTimer()
    {
        _timerOut = false;
        _timeValue = _startTimeValue;
    }

    public void Pause()
    {
        _pause = true;
    }

    public void Unpause()
    {
        _pause = false;
    }
}