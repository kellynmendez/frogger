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

    public void LadyFrogReachedEndPopUp(EndCollider endCol)
    {
        TMP_Text endTxt = endCol.GetEndText();
        StartCoroutine(ShowBonusPopup(endTxt));
    }

    private IEnumerator ShowBonusPopup(TMP_Text endTxt)
    {
        endTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        endTxt.gameObject.SetActive(false);
    }
}
