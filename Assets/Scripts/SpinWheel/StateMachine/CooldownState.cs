using DG.Tweening;

public class CooldownState : IState
{
    public ITrigger OnFinish => _onFinish;

    readonly Trigger _onFinish;
    readonly Button _button;
    readonly RewardSetter _rewardSetter;
    readonly TimerText _timerText;
    readonly IModel _model;
    readonly IConfigManager _configManager;

    public CooldownState(IConfigManager configManager, IModel model, 
        Button button, RewardSetter rewardSetter, TimerText timerText)
    {
        _configManager = configManager;
        _model = model;
        _button = button;
        _rewardSetter = rewardSetter;
        _timerText = timerText;

        _onFinish = new Trigger();
    }

    void IState.OnEnter()
    {
        _button.HideText();

        int cooldownTime = _configManager.CooldownTime;
        int currentTime = 0;

        DOVirtual.Int(cooldownTime, 0, cooldownTime, (time) =>
        {
            if (currentTime != time && time != 0)
            {
                _model.GenerateNewRewards();
                _rewardSetter.SetRewards(_model.RewardType, _model.Rewards);
                _timerText.SetSecond(time);
                currentTime = time;
            }
        })
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            _onFinish.Fire();
        });
    }

    void IState.OnExit()
    {
        _timerText.Hide();
    }
}
