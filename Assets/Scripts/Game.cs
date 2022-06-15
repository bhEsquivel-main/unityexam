using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        this.point = 0;
        InitializePlayer();
        InitializeGem();
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

    //DELEGATE HANDLER
    void OnCollectGem(Gem g) 
    {
        gems.Remove(g);
        GameObject.Destroy(g.gameObject, 0);
        point++;
        if(CanSpawnNewGem == true)  {
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
