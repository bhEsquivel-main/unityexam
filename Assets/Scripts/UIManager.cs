using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public delegate void UIEventHandler(bool isPaused);

    public event UIEventHandler OnPauseEvent;

    public static UIManager I {get;set;}

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    [SerializeField]
    private GameObject GameHUD;
    [SerializeField]
    private GameObject MenuHUD;

    [SerializeField]
    private GameObject GameOverHUD;


    #region GAME REGION
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

    public void InitializeGame()
    {
        pointsLabel.text = "0";
        GameHUD?.SetActive(true);
        GameOverHUD?.SetActive(false);
        MenuHUD?.SetActive(false);
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
    
    #endregion


    #region  MAINMENU
    [SerializeField]
    private Text highScoreLabel;
    public void InitializeMenu()
    {
        GameHUD?.SetActive(false);
        GameOverHUD?.SetActive(false);
        MenuHUD?.SetActive(true);
        Debug.Log(Helper.GetHighScore());
        highScoreLabel.text = Helper.GetHighScore().ToString();
    }
    public void OnClickStartgame()
    {
        SManager.I.LoadStage("S2");
    }

    #endregion


    #region  GAMEOVER
    [SerializeField]
    private Text finalScoreLbl;
    
    public void InitializeGameOver()
    {
        finalScoreLbl.text = Helper.CURRENT_SCORE.ToString();
        Helper.SaveHighScore(Helper.CURRENT_SCORE);
        GameHUD?.SetActive(false);
        MenuHUD?.SetActive(false);
        GameOverHUD?.SetActive(true);
    }
    public void OnClickTryAgain()
    {
        SManager.I.LoadStage("S2");
    }   
    public void OnClickExit()
    {
        SManager.I.LoadStage("S1");
    }

    #endregion


}
