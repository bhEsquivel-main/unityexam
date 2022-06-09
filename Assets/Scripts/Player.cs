
using UnityEngine;

public class Player : BaseCharacter
{
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
    public bool IsDEAD = false;
    protected override void Update() {

    }
}
