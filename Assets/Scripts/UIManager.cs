using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public delegate void UIEventHandler(bool isPaused);

    public event UIEventHandler OnPauseEvent;
    
    private bool isPaused = false;

    [SerializeField]
    private Text timerLabel;

    [SerializeField]
    private Text pointsLabel;

    [SerializeField]
    private Button pauseBtn;
    private Text pauseBtnLabel;

    public Text PAUSEBTNLBL 
    {
        get {
            if(pauseBtnLabel == null) 
            {
                pauseBtnLabel = pauseBtn.GetComponentInChildren<Text>();
            }
            return pauseBtnLabel;
        }
    }


    public void OnClickPause() {
        isPaused = !isPaused;
        PAUSEBTNLBL.text = isPaused ? "UNPAUSE" : "PAUSE";
        OnPauseEvent?.Invoke(isPaused);
    }



    public void UpdateTimer(string value) 
    {
        timerLabel.text = value;
    }

    public void UpdatePoints(string value)
    {
        pointsLabel.text = value;
    }




}
