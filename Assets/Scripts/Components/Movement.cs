using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementType
{
    STATIONARY = 0,
    CONTROLLED,
    LtoR,
    ROAMER,
    FOLLOWER,
}
public class Movement : MonoBehaviour
{
    
    public delegate void MovementHandler(Vector3 dir);
    public event MovementHandler OnMoveUnit;

    Vector3 dir = Vector3.zero;

    Vector3 _initPosition;

    //LEFTTOR
    float _ltr_x_min;
    float _ltr_x_max;
    //ROAMER
    float _roamer_radius;
    //FOLLOWER
    Transform _target;

    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private MovementType _movementType = MovementType.STATIONARY;

    private bool _enabled = false;

    public void DisableMovement() 
    {
        _enabled = false;
    }
    public void EnableMovement() 
    {
        _enabled = true;
    }
    bool CanMove() {
        return _enabled == true;
    }
    private void Update()
    {
        if(!CanMove()) return;
        if(_movementType == MovementType.CONTROLLED)CONTROL();
    }

    /// <summary>
    /// Initialize Position for STATIONARY and CONTROLLED
    /// </summary>
    public void Initialize(Vector3 initPos) 
    {
        this._initPosition = initPos;
        this.transform.position = initPos;
    }
    /// <summary>
    /// LtoR [pos, left, right]
    /// </summary>
    public void Initialize(Vector3 initPos, float xMin, float xMax)
    {
        Initialize(initPos);
        this._ltr_x_min = xMin;
        this._ltr_x_max = xMax;
    }
    /// <summary>
    /// Roamer [pos, radius]
    /// </summary>

    public void Initialize(Vector3 initPos, float radius)
    {
        Initialize(initPos);
        this._roamer_radius = radius;
    }
    /// <summary>
    /// Follower [pos, targetTrans]
    /// </summary>
    public void Initialize(Vector3 initPos, Transform target)
    {
        Initialize(initPos);
        this._target = target;
    }

    private void CONTROL()
    {
        if(Game.INSTANCE.JOYSTICK.Vertical != 0 || Game.INSTANCE.JOYSTICK.Horizontal != 0) {
            dir.z = Game.INSTANCE.JOYSTICK.Vertical;
            dir.x = Game.INSTANCE.JOYSTICK.Horizontal;
        } else {
            dir.z = Input.GetAxis("Vertical");
            dir.x = Input.GetAxis("Horizontal");
        }
        Move();
        FaceDirection();
        OnMoveUnit?.Invoke(dir);
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
