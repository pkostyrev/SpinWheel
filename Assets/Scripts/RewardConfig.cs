using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardConfig", menuName = "Config/RewardConfig")]
public class RewardConfig : ScriptableObject
{
    [Serializable]
    public class RewardIcon
    {
        public RewardType type;
        public Sprite sprite;
    }

    public RewardIcon[] RewardIcons => _rewardIcons;
    public int MinRewardAmount => _minRewardAmount;
    public int MaxRewardAmount => _maxRewardAmount;
    public int StepRewardAmount => _stepRewardAmount;

    [SerializeField] private RewardIcon[] _rewardIcons;
    [SerializeField] private int _minRewardAmount;
    [SerializeField] private int _maxRewardAmount;
    [SerializeField] private int _stepRewardAmount;

}
