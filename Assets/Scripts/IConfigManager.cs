using UnityEngine;

public interface IConfigManager
{
    int SectionCount { get; }
    float RotationSpead {get;}
    float RotationTime {get;}
    int CooldownTime { get; }
    float DelayGenerationTime { get; }
    float DelayAfterCooldown { get; }
    int MaxSpawnCount { get; }
    float MinSpawnRadius { get; }
    float MaxSpawnRadius { get; }
    float MinDelayMoveEndTime { get; }
    float MaxDelayMoveEnd { get; }
    float DurationToSpawnPoint { get; }
    float DurationToEndPoint { get; }
    float DelayAfterAnimationReward{ get; }

    int MinRewardAmount { get; }
    int MaxRewardAmount { get; }
    int StepRewardAmount { get; }

    void Init();

    Sprite GetRewardIcon(RewardType type);
}
