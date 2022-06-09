
using UnityEngine;

public class Player : BaseCharacter
{
   
    public bool IsDEAD = false;
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

    private Animator _animator;
    public Animator ANIMATOR 
    {
        get 
        {
            if(_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }
            return _animator;
        }
    }


    void Start()
    {

    }

    private void OnEnable() 
    {
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit += CharacterMoveHandler;
        } 
    }

    private void OnDisable() {
        if(MOVEMENT != null) 
        {
            MOVEMENT.OnMoveUnit -= CharacterMoveHandler;
        } 
    }

    protected override void Update()
    {

    }


    public void CharacterMoveHandler(Vector3 dir)
    {
        AnimateWalk(dir.magnitude);
    }

    public void AnimateWalk(float value) 
    {
        ANIMATOR.SetFloat(Helper.P_ANIM_MOVEMENT, value);
    }

    public void AnimateDeath() 
    {
        ANIMATOR.SetBool(Helper.P_ANIM_DEAD, IsDEAD);
    }
}
