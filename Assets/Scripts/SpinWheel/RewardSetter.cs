using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _rewards;
    [SerializeField] private Image _rewardIcon;

    IConfigManager _configManager;

    public void Init(IConfigManager configManager)
    {
        _configManager = configManager;
    }

    public void SetRewards(RewardType type, int[] rewards)
    {
        _rewardIcon.sprite = _configManager.GetRewardIcon(type);

        for (int i = 0; i < _rewards.Length; i++)
        {
            _rewards[i].text = rewards[i].ToString();
        }
    }

    public void SetShownIcon(bool isShown)
    {
        _rewardIcon.gameObject.SetActive(isShown);
    }
}
