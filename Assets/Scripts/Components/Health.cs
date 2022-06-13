using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
   
    public delegate void HealthHandler( );
    public event HealthHandler OnUnitDied;
    public event HealthHandler OnUnitHit;
    public event HealthHandler OnUnitRegen;

    private float currentHP;
    private float maxHP;
    private bool alive = false;


    public void Initialize(float _currentHP, float _maxHP) {
        this.currentHP = _currentHP;
        this.maxHP = _maxHP;
        this.alive = true;
    }

    public void Update() 
    {
            if (!this.alive)return;
            if (this.maxHP == 0) return;

            if(this.currentHP <= 0) {
                this.alive = false;
                OnUnitDied?.Invoke();
            }
    }

    public bool IsAlive 
    {
        get {
            return this.alive;
        }
    }

    public void Damage() 
    {
        this.currentHP--;
        OnUnitHit?.Invoke();
    }
    public void Regenerate() 
    {
        this.currentHP++;
        this.currentHP = Mathf.Clamp(this.currentHP, 0, this.maxHP);
        OnUnitRegen?.Invoke();
    }


}
