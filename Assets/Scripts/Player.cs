
using UnityEngine;

public class Player : BaseCharacter
{
   
    public delegate void PlayerHandler(Gem g);
    public event PlayerHandler OnCollect;

    private void Start()
    {
        Initialize();
    }
    protected override void Update()
    {

    }

    
    private void Initialize()
    {
        MOVEMENT.EnableMovement(); 
        HEALTH.Initialize(100, 100);   
    }


    private void OnEnable() 
    {
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit += CharacterMoveHandler;
        } 
        if (HEALTH != null)
        {
            HEALTH.OnUnitDied += PlayerDeadHandler;
            HEALTH.OnUnitHit += PlayerHitHandler;
        }
    }

    private void OnDisable() {
        DeactivatePlayer();
    }


    private void CharacterMoveHandler(Vector3 dir)
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Walk(dir.magnitude);
    }

    public void Pause()
    {
        if(MOVEMENT) MOVEMENT.DisableMovement(); 
    }

    private void PlayerDeadHandler() 
    {
        if(MOVEMENT) MOVEMENT.DisableMovement(); 
        if(CHAR_ANIM)CHAR_ANIM.Dead();
    }    
    private void PlayerHitHandler() 
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Hit();
    }

    public void PlayerWin() 
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Win();
        DeactivatePlayer();
    }
    public void DeactivatePlayer()
    {
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit -= CharacterMoveHandler;
        }      
        if (HEALTH != null)
        {
            HEALTH.OnUnitDied -= PlayerDeadHandler;
            HEALTH.OnUnitHit -= PlayerHitHandler;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("gem")) {
           Gem gem = other.GetComponent<Gem>();
           OnCollect?.Invoke(gem);
        }
    }
    
}
