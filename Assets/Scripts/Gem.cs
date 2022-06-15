using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectible
{
    
    public int POINT_VALUE;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize(int _pointValue)
    {
        this.POINT_VALUE = _pointValue;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Collect()
    {
        return this.gameObject;
    }
}
