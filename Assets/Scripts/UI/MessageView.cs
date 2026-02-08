using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class MessageView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _messageBlock;
    [SerializeField]
    private TMP_Text _messageText;
    [SerializeField]
    private float _textSpeed;
    [SerializeField]
    private float _fadeSpeed;
    [SerializeField]
    private float _timeBeforeEnd = 1f;

    public event Action OnEndShowMessage;

    private void Awake()
    {
        _messageBlock.alpha = 0;
    }

    public void SetText(string text, bool fade = true)
    {
        StartCoroutine(AnimationText(text, fade));
    }

    private IEnumerator AnimationText(string text, bool fade)
    {
        _messageText.text = "";
        _messageBlock.DOFade(1, 1 / _fadeSpeed);
        foreach (var character in text)
        {
            _messageText.text += character;
            yield return new WaitForSeconds(1 / _textSpeed);
        }
        if (fade)
        {
            yield return new WaitForSeconds(_timeBeforeEnd);
            _messageBlock.DOFade(0, 1 / _fadeSpeed).OnComplete(() => OnEndShowMessage?.Invoke());
        }
        else
        {
            OnEndShowMessage?.Invoke();
        }
    }

    public void HideText()
    {
        _messageBlock.DOFade(0, 1 / _fadeSpeed);
    }
}
