
using UnityEngine;

public class Player : BaseCharacter
{
   
    public delegate void PlayerHandler(Gem g);
    public event PlayerHandler OnCollect;
    public event PlayerHandler OnDefeat;

    private void Start()
    {
        Initialize();
    }
    protected override void Update()
    {
		if(Input.GetKeyDown(KeyCode.Space)){
            tryToAttackTarget();
		}
    }

    
    private void Initialize()
    {
        MOVEMENT.EnableMovement(); 
        HEALTH.Initialize(100, 100, Color.green);   
    }


    private void OnEnable() 
    {
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit += CharacterMoveHandler;
        } 
        if (HEALTH != null)
        {
            HEALTH.OnUnitDied += DeadHandler;
            HEALTH.OnUnitHit += HitHandler;
        }
    }

    private void OnDisable() {
        Deactivate();
    }


    public void Pause()
    {
        if(MOVEMENT) MOVEMENT.DisableMovement(); 
    }
 
    public void PlayerWin() 
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Win();
        Deactivate();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("gem")) {
           Gem gem = other.GetComponent<Gem>();
           HEALTH.Regenerate();
           OnCollect?.Invoke(gem);
        }
    }
    RaycastHit objectHit;
    GameObject targetEnemy;
    void tryToAttackTarget(){    
        
        Vector3 hitPos = transform.position;
        Vector3 hitDir = transform.GetChild(0).forward;
        hitPos.y = 1;
        if(CHAR_ANIM)CHAR_ANIM.Attack();
        if (Physics.Raycast(hitPos, hitDir, out objectHit, 2)) {
           targetEnemy = objectHit.collider.gameObject;
           if (targetEnemy != null) {
             targetEnemy.transform.SendMessage("hitByTarget", transform, SendMessageOptions.DontRequireReceiver );
           }
        }
	}
    public void hitByEnemy(Transform sender){
        HEALTH.Damage();
	}

    
    protected override void DeadHandler() 
    {
        base.DeadHandler();
        Deactivate();
        GameObject.Destroy (HEALTH.healthBar.gameObject);
        OnDefeat?.Invoke(null);
    }    



    
}
