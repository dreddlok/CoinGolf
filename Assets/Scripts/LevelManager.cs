using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject levelComplete;
    public Scene currentScene;
    public GameObject loadingScreen;
    public GameObject nextLevelAlertText;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        loadingScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            OpenEscapeMenu();
        }
    }

    public void OpenEscapeMenu()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Start")
        { }
        else
        {
            //FindObjectOfType<PauseManager>().Pause();
        }
    }

    public void LoadLevel(string LeveltoLoad)
    {
        Debug.Log("Level load requested for " + LeveltoLoad);
        if (LeveltoLoad != "")
        {
            //SceneManager.LoadScene(LeveltoLoad);
            //GameObject.Find("UI").transform.Find("Loading Screen").gameObject.SetActive(true);
            loadingScreen.SetActive(true);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LeveltoLoad);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Application quit requested");
        Application.Quit();
    }

    public void LoadNextLevel ()
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;

        if (playerSave.totalCoins > playerSave.unlockRequirement[currentSceneIndex + 1])
        {           
            SceneManager.LoadScene(currentSceneIndex + 1);
            playerSave.currentlevel += 1;
            loadingScreen.SetActive(true);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        }
        else
        {
            nextLevelAlertText.SetActive(true);
        }
    }

    public void ReloadLevel()
    {
        currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //wrapper for startgame function as it must be a ienumerator but these cannnot be accesed directly by buttons
    public void ClickStartGame(string LeveltoLoad)
    {
        StartCoroutine(StartGame(LeveltoLoad));
    }
    
    //Used when moving from the Title Screen to the main game
    public IEnumerator StartGame(string LeveltoLoad)
    {
        GetComponent<AudioSource>().Play();
        float fadeTime = GetComponent<Fading>().BeginFade(1); // we are storing the time it will take to fully fade out
        yield return new WaitForSeconds(fadeTime);
        GetComponent<Fading>().BeginFade(-1);
        if (FindObjectOfType<PlayerSave>().totalCoins < 3)
        {
            LoadLevel("_Tutorial");
        }
        else
        {
            LoadLevel(LeveltoLoad);
        }
    }
}
