using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private UIController _uiController;
    private int _score = 0;

    private void Awake()
    {
        _uiController = FindObjectOfType<UIController>();
    }

    public void IncreaseScore(int incr)
    {
        _score += incr;
        _uiController.ChangeScore(_score);
    }

    public void LadyFrogReachedEnd()
    {

}
