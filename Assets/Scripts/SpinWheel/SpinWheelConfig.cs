using UnityEngine;

[CreateAssetMenu(fileName = "SpinWheelConfig", menuName = "Config/SpinWheelConfig")]
public class SpinWheelConfig : ScriptableObject
{
    public RewardType[] RewardTypes => _rewardTypes;
    public int SectionCount => _sectionCount;
    public float RotationTime => _rotationTime;
    public float RotationSpead => _rotationSpead;
    public int CooldownTime => _cooldownTime;
    public float DelayGenerationTime => _delayGenerationTime;
    public float DelayAfterCooldown => _delayAfterCooldown;
    public int MaxSpawnCount => _maxSpawnCount;
    public float MinSpawnRadius => _minSpawnRadius;
    public float MaxSpawnRadius => _maxSpawnRadius;
    public float MinDelayMoveEnd => _minDelayMoveEnd;
    public float MaxDelayMoveEnd => _maxDelayMoveEnd;
    public float DurationToSpawnPoint => _durationToSpawnPoint;
    public float DurationToEndPoint => _durationToEndPoint;
    public float DelayAfterAnimationReward => _delayAfterAnimationReward;

    [SerializeField] private RewardType[] _rewardTypes;
    [SerializeField] private int _sectionCount;
    [SerializeField] private float _rotationTime;
    [SerializeField] private float _rotationSpead;
    [SerializeField] private int _cooldownTime;
    [SerializeField] private float _delayGenerationTime;
    [SerializeField] private float _delayAfterCooldown;

    [Header("Animation reward")]
    [SerializeField] private int _maxSpawnCount;
    [SerializeField] private float _minSpawnRadius;
    [SerializeField] private float _maxSpawnRadius;
    [SerializeField] private float _minDelayMoveEnd;
    [SerializeField] private float _maxDelayMoveEnd;
    [SerializeField] private float _durationToSpawnPoint;
    [SerializeField] private float _durationToEndPoint;
    [SerializeField] private float _delayAfterAnimationReward;
}
