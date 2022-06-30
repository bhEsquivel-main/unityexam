using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
 
    public int _ID;
    protected virtual void Update() {}

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

    protected virtual  void CharacterMoveHandler(Vector3 dir)
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Walk(dir.magnitude);
    }


    protected virtual void DeadHandler() 
    {
        if(MOVEMENT) MOVEMENT.DisableMovement(); 
        if(CHAR_ANIM)CHAR_ANIM.Dead();
    }    


    protected virtual  void HitHandler() 
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Hit();
    }


    public virtual  void Deactivate()
    {
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit -= CharacterMoveHandler;
        }      
        if (HEALTH != null)
        {
            HEALTH.OnUnitDied -= DeadHandler;
            HEALTH.OnUnitHit -= HitHandler;
        }
    }

}
