public class ActiveState : IState
{
    public ITrigger OnStartSpin => _onStartSpin;
    readonly Button _button;

    readonly Trigger _onStartSpin;

    public ActiveState(Button button)
    {
        _button = button;
        _onStartSpin = new Trigger();
    }

    void OnButtonClick()
    {
        _onStartSpin.Fire();
    }

    void IState.OnEnter()
    {
        _button.OnClick += OnButtonClick;
        _button.SetActive(true);
    }

    void IState.OnExit()
    {
        _button.OnClick -= OnButtonClick;
        _button.SetActive(false);
    }
}
