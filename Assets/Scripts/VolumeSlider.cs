using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

    public PlayerSave playerSave;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();   
    }

    public void ChangeVolume(float amount)
    {
        FindObjectOfType<PlayerSave>().ChangeVolume(amount);
    }

}
