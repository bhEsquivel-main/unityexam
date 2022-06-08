using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float _yM;
    float _xM;

    public float speed;
    public void FixedUpdate()
    {
        if(Game.INSTANCE.JOYSTICK.Vertical != 0 || Game.INSTANCE.JOYSTICK.Horizontal != 0) {
            _yM = Game.INSTANCE.JOYSTICK.Vertical;
            _xM = Game.INSTANCE.JOYSTICK.Horizontal;
        } else {
            _yM = Input.GetAxis("Vertical");
            _xM = Input.GetAxis("Horizontal");
        }

        transform.Translate(new Vector3(_xM * Time.deltaTime * speed, 0, _yM * Time.deltaTime * speed));
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
