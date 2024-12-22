using UnityEngine;

public class SpinWheelWindow : MonoBehaviour
{
    [SerializeField] private RewardSetter _rewardSetter;
    [SerializeField] private RotationAnimator _rotationAnimator;
    [SerializeField] private RewardAnimator _rewardAnimator;
    [SerializeField] private Button _button;
    [SerializeField] private TimerText _timerText;

    void Start()
    {
        IConfigManager configManager = Root.ConfigManager;

        _rewardSetter.Init(configManager);
        _rewardAnimator.Init(configManager);
        _rotationAnimator.Init(configManager);
        _rotationAnimator.Rotate(0, 0);

        var model = new Model(configManager);
        model.GenerateNewRewards();

        _rewardSetter.SetRewards(model.RewardType, model.Rewards);

        _button.SetActive(false);
        _button.HideText();

        new StateGraph(
            configManager,
            model,
            _rotationAnimator,
            _rewardAnimator,
            _button,
            _rewardSetter,
            _timerText);
    }
}
