using UnityEngine;

public class RewardGenerator
{
    readonly IConfigManager _configManager;

    RewardType[] _rewardTypes;
    int[] _rewardValues;

    public RewardGenerator(IConfigManager configManager)
    {
        _configManager = configManager;

        int rewardCount = (_configManager.MaxRewardAmount - _configManager.MinRewardAmount) / _configManager.StepRewardAmount + 1;

        _rewardValues = new int[rewardCount];

        for (int i = 0; i < _rewardValues.Length; i++)
        {
            _rewardValues[i] = Mathf.Clamp((i + 1) * _configManager.StepRewardAmount, _configManager.MinRewardAmount, _configManager.MaxRewardAmount);
        }
        _rewardTypes = (RewardType[])System.Enum.GetValues(typeof(RewardType));
    }

    public RewardType GenerateRewardType()
    {
        int randomIndex = Random.Range(0, _rewardTypes.Length - 1);
        RewardType currentType = _rewardTypes[randomIndex];

        _rewardTypes[randomIndex] = _rewardTypes[^1];
        _rewardTypes[^1] = currentType;

        return currentType;
    }

    public int[] GenerateRewardValues()
    {
        int[] rewardValues = new int[_configManager.SectionCount];

        int i = _rewardValues.Length - 1;
        int j = 0;

        for (; i >= 1; i--, j++)
        {
            if (j == rewardValues.Length)
            {
                break;
            }

            int randomRewardIndex = Random.Range(0, i);
            int randomReward = _rewardValues[randomRewardIndex];

            _rewardValues[randomRewardIndex] = _rewardValues[i];
            _rewardValues[i] = randomReward;
            rewardValues[j] = randomReward;
        }

        return rewardValues;
    }
}
