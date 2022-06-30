using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    
    public static JoystickController I { get; set; }

    [SerializeField]
    private VariableJoystick JOYSTICK;    
     void Awake() {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    

    public float Vertical 
    {
        get 
        {
            if (JOYSTICK) 
                return JOYSTICK.Vertical;
            else return 0;
        }
    }
    public float Horizontal 
    {
        get 
        {
            if (JOYSTICK) 
                return JOYSTICK.Horizontal;
            else return 0;
        }
    }


}
