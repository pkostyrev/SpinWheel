using System;
using System.Collections.Generic;

public class TransitionInfo<TBaseState>
{
    public TBaseState sourceState;
    public ITrigger trigger;
    public TBaseState targetState;
}

public class StateMachineBuilder<TBaseState>
       where TBaseState : class, IState
{
    readonly TBaseState _startState;
    readonly List<TransitionInfo<TBaseState>> _transitions;

    public StateMachineBuilder(TBaseState startState)
    {
        _startState = startState;
        _transitions = new List<TransitionInfo<TBaseState>>();
    }

    public StateMachineBuilder<TBaseState> Add(TBaseState sourceState,
            ITrigger trigger, TBaseState targetState)
    {
        _transitions.Add(
            new TransitionInfo<TBaseState>
            {
                sourceState = sourceState,
                trigger = trigger,
                targetState = targetState
            }
        );

        return this;
    }
    public StateMachine<TBaseState> Build()
    {
        return new StateMachine<TBaseState>(_startState, _transitions.ToArray());
    }
}

public class StateMachine<TBaseState>
        where TBaseState : class, IState
{
    public TBaseState currentState => _currentState;

    readonly IDictionary<ITrigger, Action> _triggersEventsSubscriptions;

    TransitionInfo<TBaseState>[] _transitions;
    TBaseState _currentState;
    bool _destroyed;

    internal StateMachine(TBaseState startState, TransitionInfo<TBaseState>[] transitions)
    {
        if (startState == null)
        {
            throw new Exception("Can't define initial state for StateMachine");
        }

        _transitions = transitions;

        _triggersEventsSubscriptions = new Dictionary<ITrigger, Action>();

        SetCurrentState(startState);
    }

    public void Destroy()
    {
        if (_destroyed)
        {
            throw new Exception("StateMachine already destroyed");
        }

        if (_currentState != null)
        {
            ResetCurrentState();
        }

        _transitions = null;

        _destroyed = true;
    }

    public T GetTypedCurrentState<T>()
        where T : TBaseState
    {
        return _currentState switch
        {
            T typedCurrentState => typedCurrentState,
            _ => throw new WrongStateException<TBaseState>(_currentState, typeof(T))
        };
    }

    void SetCurrentState(TBaseState targetState)
    {
        _currentState = targetState;
        _currentState.OnEnter();

        SubscribeTriggers();
    }

    void ResetCurrentState()
    {
        UnsubscribeTriggers();

        _currentState.OnExit();
        _currentState = null;
    }

    void SubscribeTriggers()
    {
        foreach (TransitionInfo<TBaseState> transitionInfo in Array.FindAll(_transitions, t => Equals(t.sourceState, _currentState)))
        {
            var callback = new Action(
                                () => GoToState(transitionInfo.targetState)
                            );

            transitionInfo.trigger.onFired += callback;
            _triggersEventsSubscriptions.Add(transitionInfo.trigger, callback);
        }
    }

    void UnsubscribeTriggers()
    {
        foreach ((ITrigger trigger, Action action) in _triggersEventsSubscriptions)
        {
            trigger.onFired -= action;
        }

        _triggersEventsSubscriptions.Clear();
    }

    void GoToState(TBaseState targetState)
    {
        if (_destroyed)
        {
            throw new Exception("Attempt to change state of destroyed StateMachine");
        }

        ResetCurrentState();

        SetCurrentState(targetState);
    }
}
