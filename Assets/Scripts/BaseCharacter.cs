using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
 
    public int _ID;

    


    protected virtual void Update() {}
    public virtual void CharacterMoveHandler(string[] args){}
}
