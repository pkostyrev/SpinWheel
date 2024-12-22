using System;
using System.Collections;
using System.Collections.Generic;

public class WrongStateException<TBaseState> : Exception
        where TBaseState : IState
    {
        public TBaseState currentState => _currentState;

        public override IDictionary Data =>
            new Dictionary<string, object>
            {
                {
                    "CurrentState", currentState.GetType()
                       .FullName
                },
                { "TargetState", _targetStateType.FullName }
            };

        readonly TBaseState _currentState;
        readonly Type _targetStateType;

        public WrongStateException(TBaseState currentState, Type targetStateType)
        {
            _currentState = currentState;
            _targetStateType = targetStateType;
        }
    }