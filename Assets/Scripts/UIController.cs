using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject[] _frogLivesImages;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] TMP_Text _finalScore;
    [SerializeField] TMP_Text _message;

    private void Awake()
    {
        _gameOverScreen.SetActive(false);
    }

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
        _finalScore.text = score.ToString();
    }

    public void LadyFrogReachedEndPopUp(EndCollider endCol)
    {
        TMP_Text endTxt = endCol.GetEndText();
        StartCoroutine(ShowBonusPopup(endTxt));
    }

    public void ShowWinScreen()
    {
        _message.text = "YOU WIN!";
        _gameOverScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _message.text = "YOU LOST";
        _gameOverScreen.SetActive(true);
    }

    private IEnumerator ShowBonusPopup(TMP_Text endTxt)
    {
        endTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        endTxt.gameObject.SetActive(false);
    }
}
