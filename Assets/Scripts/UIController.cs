using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject[] _frogLivesImages;
    [SerializeField] TMP_Text _scoreText;

    public void DecreaseLives()
    {
        bool decreased = false;
        for (int i = 0; i < _frogLivesImages.Length && !decreased; i++)
        {
            if (_frogLivesImages[i].activeSelf)
            {
                _frogLivesImages[i].SetActive(false);
                decreased = true;
            }
        }
    }

    public void ChangeScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
