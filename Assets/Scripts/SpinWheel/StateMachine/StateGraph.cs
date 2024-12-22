public class StateGraph
{
    public IState currentState => _stateMachine.currentState;
    readonly StateMachine<IState> _stateMachine;

    public StateGraph(IConfigManager configManager, IModel model, RotationAnimator rotationAnimator,
    RewardAnimator rewardAnimator, Button button, RewardSetter rewardSetter, TimerText timerText)
    {
        var activeState = new ActiveState(button);

        var rewardSelectionState = new RewardSelectionState(
            configManager,
            model, 
            rotationAnimator, 
            rewardAnimator,
            rewardSetter);

        var cooldownState = new CooldownState(
            configManager, 
            model,  
            button, 
            rewardSetter, 
            timerText);

        _stateMachine = new StateMachineBuilder<IState>(cooldownState)
        .Add(cooldownState, cooldownState.OnFinish, activeState)
        .Add(activeState, activeState.OnStartSpin, rewardSelectionState)
        .Add(rewardSelectionState, rewardSelectionState.onRewardCollect, cooldownState)
        .Build();
    }
}
