using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    Vector3 dir = Vector3.zero;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private bool isComputer = true;

    private Animator _animator;
    public Animator ANIMATOR 
    {
        get 
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }
    private void Update()
    {
        if(isComputer == true) return;
        if(Game.INSTANCE.JOYSTICK.Vertical != 0 || Game.INSTANCE.JOYSTICK.Horizontal != 0) {
            dir.z = Game.INSTANCE.JOYSTICK.Vertical;
            dir.x = Game.INSTANCE.JOYSTICK.Horizontal;
        } else {
            dir.z = Input.GetAxis("Vertical");
            dir.x = Input.GetAxis("Horizontal");
        }
        Move();
        ANIMATOR.SetFloat("magnitude", dir.magnitude);
        FaceDirection();
    }


    public void Move() {
        transform.Translate(new Vector3(dir.x * Time.deltaTime * speed, 0, dir.z * Time.deltaTime * speed));
    }
    public void Move(float x , float y, float speed = 10 ) {
        transform.Translate(new Vector3(x * Time.deltaTime * speed, 0, y * Time.deltaTime * speed));
    }

    private void FaceDirection() {
        transform.GetChild(0).LookAt(transform.position + dir);
    }


}
