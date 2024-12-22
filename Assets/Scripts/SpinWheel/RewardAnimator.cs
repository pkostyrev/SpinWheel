using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using TMPro;

public class RewardAnimator : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Reward _reward;
    [SerializeField] private TextMeshProUGUI _textReward;
    IConfigManager _configManager;

    ObjectPool<Reward> _pool;
    RewardType _currentType;
    int _currentRewardAmount;

    public void Init(IConfigManager configManager)
    {
        _configManager = configManager;

        _pool = new ObjectPool<Reward>(() => Instantiate(_reward, _spawnPoint),
        (rew) =>
        {
            rew.transform.localScale = Vector3.zero;
            rew.SetReward(_currentType);
            rew.gameObject.SetActive(true);
        },
        (rew) =>
        {
            rew.gameObject.SetActive(false);
            rew.transform.position = _spawnPoint.position;
        },
        (obj) => Destroy(obj), false, 0, _configManager.MaxSpawnCount);

        _textReward.gameObject.SetActive(false);
    }

    public Tween StartAnimation(RewardType type, int rewardAmount)
    {
        _currentType = type;

        int maxSpawnCount = _configManager.MaxSpawnCount;

        int rewardObjA = rewardAmount % maxSpawnCount;
        int rewardAmountB = (rewardAmount - rewardObjA) / maxSpawnCount;
        int rewardAmountA = rewardObjA != 0 ? (rewardAmount - rewardAmountB * (maxSpawnCount - rewardObjA)) / rewardObjA : 0;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            _currentRewardAmount = 0;
            _textReward.text = _currentRewardAmount.ToString();
            _textReward.gameObject.SetActive(true);
        });

        for (int i = 0; i < System.Math.Clamp(rewardAmount, 0, maxSpawnCount); i++)
        {
            int addValue = i < rewardObjA ? rewardAmountA : rewardAmountB;
            RewardAnimation(sequence, _pool.Get(), addValue);
        }

        sequence.AppendInterval(_configManager.DelayAfterAnimationReward);
        sequence.AppendCallback(() =>
        {
            _textReward.gameObject.SetActive(false);
        });

        return sequence;
    }

    void RewardAnimation(Sequence sequence, Reward reward, int addValue)
    {
        Sequence rewadSequence = DOTween.Sequence();

        rewadSequence.Append(MoveToEndSpawnPosition(reward));
        rewadSequence.Join(reward.transform.DOScale(Vector3.one, _configManager.DurationToSpawnPoint));

        rewadSequence.AppendInterval(Random.Range(_configManager.MinDelayMoveEndTime, _configManager.MaxDelayMoveEnd));

        rewadSequence.Append(reward.transform.DOMove(_endPoint.position, _configManager.DurationToEndPoint));
        rewadSequence.Join(reward.transform.DOScale(Vector3.zero, _configManager.DurationToEndPoint));

        rewadSequence.AppendCallback(() =>
        {
            _currentRewardAmount += addValue;
            _textReward.text = _currentRewardAmount.ToString();
            _pool.Release(reward);
        });

        sequence.Join(rewadSequence);
    }

    Tween MoveToEndSpawnPosition(Reward reward)
    {
        float randAng = Random.Range(0, Mathf.PI * 2);
        float randomRadius = Random.Range(_configManager.MinSpawnRadius, _configManager.MaxSpawnRadius);
        Vector3 endSpawnPosition = new Vector2(Mathf.Cos(randAng), Mathf.Sin(randAng)) * randomRadius;

        return reward.transform.DOLocalMove(endSpawnPosition, _configManager.DurationToSpawnPoint);
    }
}
