
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


    private Movement _movement;
     public Movement MOVEMENT 
    {
        get 
        {
            if(_movement == null)
            {
                _movement = GetComponent<Movement>();
            }
            return _movement;
        }
    }

    private CharAnimator _charAnim;
    public CharAnimator CHAR_ANIM 
    {
        get 
        {
            if(_charAnim == null)
            {
                _charAnim = GetComponent<CharAnimator>();
            }
            return _charAnim;
        }
    }

    private Health _health;
    public Health HEALTH 
    {
        get 
        {
            if(_health == null)
            {
                _health = GetComponent<Health>();
            }
            return _health;
        }
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
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit -= CharacterMoveHandler;
        }      
        if (HEALTH != null)
        {
            HEALTH.OnUnitDied -= PlayerDeadHandler;
            HEALTH.OnUnitHit += PlayerHitHandler;
        }
    }


    private void CharacterMoveHandler(Vector3 dir)
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Walk(dir.magnitude);
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


    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("gem")) {
           Gem gem = other.GetComponent<Gem>();
           OnCollect?.Invoke(gem);
        }
    }
    
}
