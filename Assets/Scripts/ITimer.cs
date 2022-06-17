


public  interface ITimerPublisher
{
    void Register(ITimerListener listener );
    void UnRegister(ITimerListener listener );
    void NotifyListeners();
}

public interface ITimerListener
{
    void Notify(ITimerPublisher publisher);
}
