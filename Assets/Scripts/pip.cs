using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pip : MonoBehaviour {

    public float pipValue; // determines where in the order of pips it lies and the number needed to "activate"
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private Button button;
    private PlayerSave playerSave;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();
        button = GetComponent<Button>();
    }

    private void OnGUI()
    {
        if (FindObjectOfType<PlayerSave>().volume >= pipValue)
        {
            button.image.sprite = activeSprite;
        }
        else
        {
            button.image.sprite = inactiveSprite;
        }
    }

    public void SetVolumeToSelf()
    {
        FindObjectOfType<PlayerSave>().volume = pipValue;
    }

}
