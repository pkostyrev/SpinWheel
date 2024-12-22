using DG.Tweening;
using UnityEngine;

public class RewardSelectionState : IState
{
    public ITrigger onRewardCollect => _onRewardCollect;

    readonly IConfigManager _configManager;
    readonly IModel _model;
    readonly RotationAnimator _rotationAnimator;
    readonly RewardAnimator _rewardAnimator;
    readonly RewardSetter _rewardSetter;
    readonly Trigger _onRewardCollect;

    public RewardSelectionState(IConfigManager configManager, IModel model, RotationAnimator rotationAnimator,
        RewardAnimator rewardAnimator, RewardSetter rewardSetter)
    {
        _configManager = configManager;
        _model = model;
        _rotationAnimator = rotationAnimator;
        _rewardAnimator = rewardAnimator;
        _rewardSetter = rewardSetter;

        _onRewardCollect = new Trigger();
    }

    void IState.OnEnter()
    {
        RewardData reward = _model.SelectNewReward();
        Debug.Log($"Reward: {reward.amount}");

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rotationAnimator.Rotate(reward.index, _configManager.RotationTime));
        sequence.AppendCallback(() => _rewardSetter.SetShownIcon(false));
        sequence.Append(_rewardAnimator.StartAnimation(_model.RewardType, reward.amount));
        sequence.AppendCallback(() => _onRewardCollect.Fire());
    }

    void IState.OnExit()
    {
        _rewardSetter.SetShownIcon(true);
    }
}
