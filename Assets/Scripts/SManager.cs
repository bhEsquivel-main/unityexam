using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    
    public static SManager I {get;set;}

    bool isSceneLoading = false;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ExecuteSceneFunc(scene);
    }
    public void ExecuteSceneFunc(Scene scene)
    {
        isSceneLoading = false;

        switch (scene.name)
        {
            case "S1":
                UIManager.I.InitializeMenu();
                break;
            case "S2":
                UIManager.I.InitializeGame();
                break;
        }
    }
    public string GetCurrentStageName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public Scene GetCurrentStage()
    {
        return SceneManager.GetActiveScene();
    }
    public IEnumerator DelayLoadStage(string stagename, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadStage(stagename);
    }
    public void LoadStage(string stagename)
    {
        if (!isSceneLoading)
        {
            isSceneLoading = true;
            StartCoroutine(LoadNextStage(stagename));
        }     
    }

    IEnumerator LoadNextStage(string stagename)
    {
        yield return new WaitUntil(() => Helper.SCENE_IS_LOADING == false);
        float fadeTime = GetComponent<SceneFadeEffect>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(stagename);
    }
}
