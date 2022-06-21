using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyDATA", menuName = "EnemyDATA", order = 52)]
public class EnemyDATA : GameSO
{

    public int power;
    public int HP;
    public MovementType movementType;   

    public RuntimeAnimatorController animatorController;
    public Avatar avatar;
}