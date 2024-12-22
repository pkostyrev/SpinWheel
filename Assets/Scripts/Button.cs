using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] private UnityEngine.UI.Button _button;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _inavctive;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnClick?.Invoke();
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            _text.enabled = true;
            _background.sprite = _active;
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
            _background.sprite = _inavctive;
        }
    }

    public void HideText()
    {
        _text.enabled = false;
    }
}
