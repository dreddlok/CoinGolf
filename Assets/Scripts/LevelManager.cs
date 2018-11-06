﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject levelComplete;
    public Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
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
            SceneManager.LoadScene(LeveltoLoad);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Application quit requested");
        Application.Quit();
    }

    public void LoadNextLevel ()
    {
        currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
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
        LoadLevel(LeveltoLoad);
    }
}