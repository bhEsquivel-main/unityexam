using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GemDATA", menuName = "GemDATA", order = 51)]
public class GemDATA : ScriptableObject
{
    public int POINT_VALUE;
    public float nextSpawnDuration;

    public GameObject prefab;
}