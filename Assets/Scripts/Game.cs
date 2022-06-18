using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    public static Game I { get; set; }
    [SerializeField]
    private bool isPaused = false;

    public List<Gem> gems = new List<Gem>();
    public int point = 0;

    private Spawner _gspawner;
    public Spawner GEMSPAWNER 
    {
        get 
        {
            if(_gspawner == null)_gspawner= GameObject.Find("GemSpawner").GetComponent<Spawner>();
            return _gspawner;
        }
    }

   

    private Player _player;
    public Player PLAYER
    {
        get 
        {
            if(_player == null)_player= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            return _player;
        }
    }
    void Awake() {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Start() {
        UIManager.I.OnPauseEvent += OnGamePaused;

        Timer.INSTANCE.OnStart += OnTimeStarted;
        Timer.INSTANCE.OnEnd += OnTimeExpired;
        Timer.INSTANCE.OnUpdate += OnGameUpdating;
        Timer.INSTANCE.Initialize(Helper.GAME_DURATION);
    }

    void Initialize() 
    {   
        this.point = 0;
        InitializePlayer();
        InitializeGem();
    }
    void Update()
    {   
    }

    public void OnGamePaused(bool pause)
    {
        if(pause) {
            Timer.INSTANCE.Stop();
            PLAYER.MOVEMENT.DisableMovement();
        } else {
            PLAYER.MOVEMENT.EnableMovement();
            Timer.INSTANCE.Resume();
        }
    }
    public void OnTimeStarted(float time) 
    {
        Debug.Log("OnTimeStarted");
        Initialize();
        UpdateUITimer();
    }
    public void OnTimeExpired(float time) 
    {
        GameEnd();
        UpdateUITimer();
    }
    public void OnGameUpdating(float time) {
        UpdateUITimer();

        
    }


    private void UpdateUITimer()
    {
        UIManager.I.UpdateTimer(Timer.INSTANCE.Minutes + ":" + Timer.INSTANCE.Seconds);
    }




    void InitializePlayer() 
    {
        PLAYER.OnCollect += OnCollectGem;
    }

    void InitializeGem() 
    {
        GEMSPAWNER.OnSpawn += OnSpawnGem;
        GEMSPAWNER.StartSpawning();
    }

    bool CollectionCompleted() {
        return point >= Helper.POINTS_TO_WIN;
    }

    void ClearGems() {
        if (gems.Count > 0) 
        {
            ClearGem(gems[gems.Count-1]);
            ClearGems();
        }
    }
    void ClearGem(Gem g) {
        gems.Remove(g);
        GameObject.Destroy(g.gameObject, 0);
    }
    void GameEnd() {
        //PLAYER.PlayerWin();
        PLAYER.OnCollect -= OnCollectGem;
        GEMSPAWNER.OnSpawn -= OnSpawnGem;
        Timer.INSTANCE.Stop();
        PLAYER.MOVEMENT.DisableMovement();
       // GAMETIME.RemoveTimerListener(this);
        ClearGems();
        GEMSPAWNER.StopSpawning();
        Helper.CURRENT_SCORE = point;

        GameOverScene();
        

    }
    void GameOverScene() 
    {
        UIManager.I.InitializeGameOver();
    }
    //DELEGATE HANDLER
    void OnCollectGem(Gem g) 
    {
        ClearGem(g);
        point += g.POINT_VALUE;
        UIManager.I.UpdatePoints(point.ToString());

        if (CollectionCompleted()) 
        {
            GameEnd();
            return;
        }

        if(CanSpawnNewGem == true)  
        {
            GEMSPAWNER.StartSpawning();
        }
    }
    void OnSpawnGem(GameObject obj, GameSO data) 
    {
        Gem newgem = obj.GetComponent<Gem>();
        newgem.Initialize((data as GemDATA).POINT_VALUE);
        gems.Add(newgem);

        if(CanSpawnNewGem == false)  {
            GEMSPAWNER.StopSpawning(); 
        }
    }

    bool CanSpawnNewGem
    {
        get
        {
            return gems.Count < Helper.MAX_GEM_OBJECTS;
        }
    }
}
