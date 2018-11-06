using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour {

    public enum LevelGrade { Ungraded, A, B, C};
    public bool[] level;
    public LevelGrade[] levelGrade;
    public int[] unlockRequirement; // the amount of total coins collected need to play the level
    public string[] title;
    public int[] levelCoins; // the number of coins collected in the level
    public int totalCoins;
    public float volume;
    public float sfxVolume;
    public static PlayerSave instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Load();

        level[0] = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Save();
        }
        else if (Input.GetKeyDown("backspace"))
        {
            Load();
            Debug.Log("Game Loaded");
        }
    }

    public void Save()
    {
        SaveGameManager.SavePlayer(this);
        Debug.Log("Game Saved");
    }

    public void Load()
    {
        
        level = SaveGameManager.LoadPlayer(out volume);
    }

    public void ChangeVolume(float amount)
    {
        volume = amount;
        volume = Mathf.Clamp(volume, 0, 1);
    }

    public void ChangeSFXVolume(float amount)
    {
        sfxVolume = amount;
        sfxVolume = Mathf.Clamp(volume, 0, 1);
    }

    public void ClearSave()
    {
        for (int i = 0; i < level.Length; i++)
        {
            level[i] = false;
            levelGrade[i] = LevelGrade.Ungraded;
            levelCoins[i] = 0;
        }
        level[1] = true;

        volume = 1;
        sfxVolume = 1;
        Save();
        Debug.Log("save cleared");
    }
}
