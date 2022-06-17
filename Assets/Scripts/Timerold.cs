using UnityEngine;

    namespace Legacy {
    public delegate void TimerEventHandler(ITimerPublisher publisher);
    public class Timer : MonoBehaviour, ITimerPublisher
    {
        
        public static Timer INSTANCE { get; set; }

        private event TimerEventHandler TimerEvent;


        private float _timeLeft = 10;
        private float _duration = 10;
        public bool isRunning = false;

        private bool _initialized = false;
    
    
        public void Register(ITimerListener l)
        {
            TimerEvent += new TimerEventHandler(l.Notify);
        }    
        public void UnRegister(ITimerListener l)
        {
            TimerEvent -= new TimerEventHandler(l.Notify);
        
        }
        public void NotifyListeners() 
        {
            if(TimerEvent != null)
            {
                TimerEvent(this);
            }
        }



        void Awake() {
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

            if(isRunning && _initialized == true) 
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    Debug.Log(Seconds);
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
            NotifyListeners();
        }
        void End()
        {
            _timeLeft = 0;
            isRunning = false;
        }

        public void Reset() {
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
}