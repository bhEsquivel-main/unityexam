using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game INSTANCE { get; set; }

    public VariableJoystick JOYSTICK;


    private Player _player;
    public Player PLAYER
    {
        get 
        {
            if(_player == null)_player= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            return _player;
        }
    }
    void Awake() {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
