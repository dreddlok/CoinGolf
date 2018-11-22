using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelTitle : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        Text text = GetComponent<Text>();
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        text.text = playerSave.title[playerSave.currentlevel];
        if (playerSave)
        {
            Debug.Log("player save found");
        }
        else
        {
            Debug.LogError("player save not found");
        }
        Debug.Log(playerSave.title[playerSave.currentlevel]);
        Image background = GameObject.Find("BG Level Title").GetComponent<Image>();

        DOTween.Sequence()
            .Append(text.DOFade(0, 1.0f).From())
            .Join(background.DOFade(0, 1.2f).From())
            .AppendInterval(1.5f)
            .Append(text.DOFade(0, 2))
            .Join(background.DOFade(0, 2))
            .AppendCallback(() => {
                // this is callback
                Destroy(this.gameObject);
            });
    }
}
