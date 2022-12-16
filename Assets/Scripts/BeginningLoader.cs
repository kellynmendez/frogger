using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginningLoader : MonoBehaviour
{
    [SerializeField] GameObject _loadMenu;
    [SerializeField] GameObject[] _frogs;
    private Timer _timer;
    private PlayerController _playerController;

    private void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        _loadMenu.SetActive(true);
        _timer.Pause();
        _playerController.Pause();

        for (int i = 0; i < _frogs.Length; i++)
        {
            RectTransform rectTrans = _frogs[i].GetComponent<RectTransform>();

            for (int j = 0; j < 14 - i; j++)
            {
                Vector2 from = rectTrans.anchoredPosition;
                Vector2 to = new Vector2(rectTrans.anchoredPosition.x - 95, rectTrans.anchoredPosition.y);
                float duration = 0.2f;

                rectTrans.anchoredPosition = from;

                float elapsedTime = 0;
                while (elapsedTime < duration)
                {
                    rectTrans.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(0.15f);

                rectTrans.anchoredPosition = to;
                yield return null;
            }
        }

        for (int i = 0; i < _frogs.Length; i++)
        {
            GameObject frogImg = _frogs[i].transform.GetChild(0).gameObject;
            GameObject frogLetter = _frogs[i].transform.GetChild(1).gameObject;
            yield return new WaitForSeconds(0.2f);
            frogImg.SetActive(false);
            frogLetter.SetActive(true);
        }

        yield return new WaitForSeconds(4f);

        _loadMenu.SetActive(false);
        _timer.Unpause();
        _playerController.Unpause();

        for (int i = 0; i < _frogs.Length; i++)
        {
            GameObject frogImg = _frogs[i].transform.GetChild(0).gameObject;
            GameObject frogLetter = _frogs[i].transform.GetChild(1).gameObject;
            frogImg.SetActive(true);
            frogLetter.SetActive(false);
        }
    }

}
