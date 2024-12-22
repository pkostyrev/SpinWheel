using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetSecond(int second)
    {
        _text.text = second.ToString();
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}