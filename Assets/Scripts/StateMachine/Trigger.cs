using System;

public interface ITrigger
{
    event Action onFired;
}

public class Trigger : ITrigger
{
    public event Action onFired;

    public void Fire()
    {
        onFired?.Invoke();
    }
}

