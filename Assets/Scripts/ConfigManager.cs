using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour, IConfigManager
{
    [SerializeField] private RewardConfig _rewardConfig;
    [SerializeField] private SpinWheelConfig _spinWheelConfig;

    Dictionary<RewardType, Sprite> _icons = new Dictionary<RewardType, Sprite>();

    public int SectionCount => _spinWheelConfig.SectionCount;
    public float RotationSpead => _spinWheelConfig.RotationSpead;
    public float RotationTime => _spinWheelConfig.RotationTime;
    public int CooldownTime => _spinWheelConfig.CooldownTime;
    public float DelayGenerationTime => _spinWheelConfig.DelayGenerationTime;
    public float DelayAfterCooldown => _spinWheelConfig.DelayAfterCooldown;
    public int MaxSpawnCount => _spinWheelConfig.MaxSpawnCount;
    public float MinSpawnRadius => _spinWheelConfig.MinSpawnRadius;
    public float MaxSpawnRadius => _spinWheelConfig.MaxSpawnRadius;
    public float MinDelayMoveEndTime => _spinWheelConfig.MinDelayMoveEnd;
    public float MaxDelayMoveEnd => _spinWheelConfig.MaxDelayMoveEnd;
    public float DurationToSpawnPoint => _spinWheelConfig.DurationToSpawnPoint;
    public float DurationToEndPoint => _spinWheelConfig.DurationToEndPoint;

    public int MinRewardAmount => _rewardConfig.MinRewardAmount;
    public int MaxRewardAmount => _rewardConfig.MaxRewardAmount;
    public int StepRewardAmount => _rewardConfig.StepRewardAmount;

    public float DelayAfterAnimationReward => _spinWheelConfig.DelayAfterAnimationReward;

    public void Init()
    {
        foreach (RewardConfig.RewardIcon rewardIcon in _rewardConfig.RewardIcons)
        {
            _icons.Add(rewardIcon.type, rewardIcon.sprite);
        }
    }

    public Sprite GetRewardIcon(RewardType type) => _icons[type];
}
