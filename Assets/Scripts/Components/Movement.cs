using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
    
	private NavMeshAgent agent;
	public float rerouteDuration = 3f;
	private float reroutingTimer = 0;
	public float stopDistance = 3f;

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
        if(_movementType == MovementType.FOLLOWER)FOLLOW();
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
        dir.z = JoystickController.I.Vertical;
        dir.x = JoystickController.I.Horizontal;
        if (dir.z == 0 && dir.x == 0)
        {
            dir.z = Input.GetAxis("Vertical");
            dir.x = Input.GetAxis("Horizontal");
        }
        Move();
        FaceDirection();
        OnMoveUnit?.Invoke(dir);
    }

    private void FOLLOW() 
    {
        if (reroutingTimer <= Time.time) { //only reroute in time intervalls of X seconds

			//Only use navMesh while Player is out of reach
			if(Vector3.Distance(_target.position, transform.position) > agent.stoppingDistance){ //No >= because it stops too abruptly
				agent.enabled = true;
				agent.SetDestination (_target.position); //reroute
			}else{
				//Player is close

				//if can se no obstacle disable navigation
				if(!Physics.Linecast(transform.position, _target.transform.position, LayerMask.GetMask("StaticObjects"))){ //Player can be shot
					//Disable Navmesh to allow for manual transformation
					agent.enabled = false;

				}else if(agent.stoppingDistance != stopDistance){ //can see obstable and stopping distance is still large
					//move closer
					agent.enabled = true;
					agent.stoppingDistance = stopDistance;

				}else if(agent.stoppingDistance == stopDistance){ //obstable despite moving in closer
					agent.enabled = true;
					//if still unable to reach, move somewhere else
					agent.SetDestination(new Vector3(
						Random.value * (stopDistance+1) + _target.transform.position.x,
						_target.transform.position.y,
						Random.value * (stopDistance+1) + _target.transform.position.z
					));
				}
			}
			reroutingTimer = Time.time + rerouteDuration; //reset delay

			if(false == agent.enabled){
				//Enemy is not navigating

				//Keep facing the placer
				Vector3 direction = (_target.position - transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
				transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
			}
		}

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
