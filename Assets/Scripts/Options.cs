using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour {

    public int[] width = new int[8];
    public int[] height = new int[8];
    public AudioMixer audioMixer;

    private PlayerSave playerSave;
        
    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();

        width[0] = 1920; height[0] = 1080;
        width[1] = 1366; height[1] = 768;
        width[2] = 2560; height[2] = 1440;
        width[3] = 1600; height[3] = 900;
        width[4] = 1440; height[4] = 900;
        width[5] = 1680; height[5] = 1050;
        width[6] = 1280; height[6] = 1024;
        width[7] = 1360; height[7] = 768;


    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetGraphicsPreset(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }

    public void SetResolution(int resPreset)
    {
        Screen.SetResolution(width[resPreset], height[resPreset], Screen.fullScreen);
        Debug.Log("Resolution set to " + width[resPreset] + "by " + height[resPreset]);
    }

    public void Save()
    {
        playerSave.Save();
    }

    public void ClearSave()
    {
        playerSave.ClearSave();
    }

    public void ChangeVolume(float value)
    {
        if (playerSave) { playerSave.ChangeVolume(value); }
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        if (playerSave)
        {
            playerSave.ChangeSFXVolume(value);
        }
        audioMixer.SetFloat("SFX Volume", -80 + value * 80);
    }
}
