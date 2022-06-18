using System;
using System.Collections;
using UnityEngine;

public class AppMain : MonoBehaviour
{
    public static AppMain I { get; set; }

    private void Awake()
    {
        //SET APPLICATION FPS
        Application.targetFrameRate = Helper.FRAME_RATE;

        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        this.gameObject.AddComponent<SManager>();
        this.gameObject.AddComponent<SceneFadeEffect>();
        StartCoroutine(StartApp());
    }

    
    IEnumerator StartApp()
    {
        yield return new WaitForSeconds(2);
        SManager.I.LoadStage("S1");
    }


}
