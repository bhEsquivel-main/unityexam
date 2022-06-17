using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Juggler : MonoBehaviour, ITimerListener
{

    private void Awake() {
    }
    public virtual void Start() {
    }
    public void Notify(ITimerPublisher publisher)
    {
        Debug.Log("Notify");
    }

    private void OnEnable() {
    } 
    private void OnDisable() {
    }

    public void OnEnd()
    {

    }

}