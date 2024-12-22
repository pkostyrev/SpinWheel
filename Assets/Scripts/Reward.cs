using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void SetReward(RewardType type)
    {
        _icon.sprite = Root.ConfigManager.GetRewardIcon(type);
    }
}
