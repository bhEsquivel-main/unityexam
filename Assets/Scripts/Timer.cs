using UnityEngine;

public class Timer : MonoBehaviour
{

    public delegate void TimerEventHandler(float _time);

    public event TimerEventHandler OnStart;
    public event TimerEventHandler OnEnd;
    public event TimerEventHandler  OnUpdate;
    public static Timer INSTANCE { get; set; }


    private float _timeLeft = 10;
    private float _duration = 10;
    public bool isRunning = false;

    private bool _initialized = false;

    void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Update()
    {

        if (isRunning && _initialized == true)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                 OnUpdate?.Invoke(_timeLeft);
            }
            else
            {
                End();
            }

        }
    }
    public void Initialize(float timeDuration)
    {
        if (_initialized == true) return;
        _initialized = true;
        _duration = timeDuration;
        _timeLeft = _duration;
        isRunning = true;
        OnStart?.Invoke(_timeLeft);
    }
    void End()
    {
        _timeLeft = 0;
        isRunning = false;
        OnEnd?.Invoke(_timeLeft);
    }

    public void Reset()
    {
        _timeLeft = _duration;
        isRunning = true;
    }

    public float CurrentTime
    {
        get
        {
            return _timeLeft;
        }
    }

    public int Minutes
    {
        get
        {
            return Mathf.FloorToInt(_timeLeft / 60);
        }
    }
    public int Seconds
    {
        get
        {
            return Mathf.FloorToInt(_timeLeft % 60);
        }
    }
}