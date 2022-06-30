using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
	public float lookForPlayerIntervalInSeconds = 1;
    private Transform _target;
	private float lastAttackTime = 0;

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

    public void Initialize(EnemyDATA data, Transform target)
    {
        _target = target;
        MOVEMENT.InitializeType(data.movementType);
        HEALTH.Initialize(data.HP, data.HP, Color.red);
        MOVEMENT.EnableMovement(); 

        
    }

    protected override void DeadHandler() 
    {
        base.DeadHandler();
        Deactivate();
        DestroyMe();
    }    

    void DestroyMe() 
    {
        GameObject.Destroy (HEALTH.healthBar.gameObject);
        GameObject.DestroyImmediate(this);
    }

    
    protected override void Update() {
        if(_target == null) return;
            float currentTimeInSeconds = Time.time;
            if (lastAttackTime + lookForPlayerIntervalInSeconds < currentTimeInSeconds) {
                tryToAttackTarget ();
                lastAttackTime = Time.time; 
		}
    }


	void tryToAttackTarget(){
		if(Vector3.Distance(transform.position, _target.position) < 3){ 
             if(CHAR_ANIM)CHAR_ANIM.Attack();
            _target.transform.SendMessage("hitByEnemy", transform, SendMessageOptions.DontRequireReceiver);
		}
	}
    public void hitByTarget(Transform sender){
        HEALTH.Damage();
	}
}
