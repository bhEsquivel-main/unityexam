using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimator : MonoBehaviour
{
    private Animator _animator;


    public void Start() 
    {
        if(_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }

    public void Walk(float value) 
    {
        if(_animator)_animator.SetFloat(Helper.P_ANIM_MOVEMENT, value);
    }

    public void Dead() 
    {
        if(_animator)_animator.SetBool(Helper.P_ANIM_DEAD, true);
    }
    public void Alive() 
    {
        if(_animator)_animator.SetBool(Helper.P_ANIM_DEAD, false);
    }

    public void Hit()
    {
        if(_animator)_animator.SetBool(Helper.P_ANIM_DEAD, false);
    }

}
