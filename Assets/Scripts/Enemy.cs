using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{


    private void OnEnable() 
    {
        if (HEALTH != null)
        {
            HEALTH.OnUnitDied += DiedHandler;
            HEALTH.OnUnitHit += HitHandler;
        }
    }
    public void Initialize(EnemyDATA data)
    {
        MOVEMENT.InitializeType(data.movementType);
        HEALTH.Initialize(data.HP, data.HP);
        MOVEMENT.EnableMovement(); 

        
    }

    private void DiedHandler() 
    {
        if(MOVEMENT) MOVEMENT.DisableMovement(); 
        if(CHAR_ANIM)CHAR_ANIM.Dead();
    }    
    private void HitHandler() 
    {
        if(CHAR_ANIM && HEALTH.IsAlive)CHAR_ANIM.Hit();
    }
}
