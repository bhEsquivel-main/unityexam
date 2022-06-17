using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    public static Game INSTANCE { get; set; }

    public List<Gem> gems = new List<Gem>();
    public int point = 0;

    public VariableJoystick JOYSTICK;

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
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Start() {
        Timer.INSTANCE.OnStart += OnGameStarted;
        Timer.INSTANCE.OnEnd += OnGameEnded;
        Timer.INSTANCE.OnUpdate += OnGameUpdating;
        Timer.INSTANCE.Initialize(20);
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
    public void OnGameStarted(float time) {
        Initialize();
    }
    public void OnGameEnded(float time) {
        GameEnd();
    }
    public void OnGameUpdating(float time) {
        
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
       // GAMETIME.RemoveTimerListener(this);
        ClearGems();
        GEMSPAWNER.StopSpawning();
    }
    //DELEGATE HANDLER
    void OnCollectGem(Gem g) 
    {
        ClearGem(g);
        point += g.POINT_VALUE;

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
