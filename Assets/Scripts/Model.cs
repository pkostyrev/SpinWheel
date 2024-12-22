using UnityEngine;

public class Model : IModel
{
    public RewardType RewardType => _currentRewardType;

    public int[] Rewards => _currentRewards;

    readonly IConfigManager _configManager;
    readonly RewardGenerator _rewardGenerator;

    int[] _currentRewards;
    RewardType _currentRewardType;
    RewardData _currentReward = new RewardData();

    public Model(IConfigManager configManager)
    {
        _configManager = configManager;
        _rewardGenerator = new RewardGenerator(configManager);
    }

    public void GenerateNewRewards()
    {
        _currentRewardType = _rewardGenerator.GenerateRewardType();
        _currentRewards = _rewardGenerator.GenerateRewardValues();
    }

    public RewardData SelectNewReward()
    {
        int index = Random.Range(0, _configManager.SectionCount);
        _currentReward.index = index;
        _currentReward.amount = _currentRewards[index];
        return _currentReward;
    }
}
