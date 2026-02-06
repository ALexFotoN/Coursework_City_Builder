using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TitlePopUp : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text titleText;

    private IEnumerator enumerator;

    public void ShowTitle(string text, float watedTime, float timeBefore = 0f, Action callback = null)
    {
        canvasGroup.alpha = 0;
        titleText.text = "";
        gameObject.SetActive(true);
        if (enumerator != null)
        {
            StopCoroutine(enumerator);
        }
        enumerator = Show(text, watedTime, timeBefore, callback);
        StartCoroutine(enumerator);
    }

    private IEnumerator Show(string title, float watedTime, float timeBefore, Action callback)
    {
        if(title != "")
        {
            yield return new WaitForSecondsRealtime(timeBefore);
            titleText.text = title;
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.01f;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(watedTime);
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 0.01f;
                yield return null;
            }
            gameObject.SetActive(false);
            callback?.Invoke();
        }
    }
}
