using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
   
    public delegate void HealthHandler( );
    public event HealthHandler OnUnitDied;
    public event HealthHandler OnUnitHit;
    public event HealthHandler OnUnitRegen;

	public Transform healthBar;

    private float currentHP;
    private float maxHP;
    private bool alive = false;


    public void Initialize(float _currentHP, float _maxHP, Color barColor) {
        this.currentHP = _currentHP;
        this.maxHP = _maxHP;
        this.alive = true;
        GameObject healthbarPrefab = Resources.Load("healthBar") as GameObject;
        
		healthBar = Instantiate (healthbarPrefab.transform, Game.I.getCanvas());
        healthBar.GetChild (0).GetComponent<Image> ().color = barColor;
    }

    public void Update() 
    {
            if (!this.alive)return;
            if (this.maxHP == 0) return;

            if(this.currentHP <= 0) {
                this.alive = false;
                OnUnitDied?.Invoke();
            }
		    healthBarReposition ();
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
        this.currentHP = Mathf.Clamp(this.currentHP, 0, this.maxHP);
        updateHealthbar();
        OnUnitHit?.Invoke();
    }
    public void Regenerate() 
    {
        this.currentHP++;
        this.currentHP = Mathf.Clamp(this.currentHP, 0, this.maxHP);
        updateHealthbar();
        OnUnitRegen?.Invoke();
    }

    void updateHealthbar() {
        if (alive == false)return;
        healthBar.GetChild (0).GetComponent<RectTransform> ().localScale = new Vector3((this.currentHP / this.maxHP), 1, 1);
    }
    private void healthBarReposition(){
        if (alive == false)return;
		healthBar.position = Camera.main.WorldToScreenPoint (new Vector3(transform.position.x, transform.position.y + 0.544f, transform.position.z)); //0.544 = center of enemy body
	}


}
