using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CongratulatoryText : MonoBehaviour {

    private void OnEnable()
    {
        Text text = GetComponent<Text>();
        Sequence tweenSequence = DOTween.Sequence()
            .AppendCallback(() =>
            {
                // this is callback
                //text.DOFade(0, 1).From();
            })
            .AppendInterval(1.5f)
            //.Append(Camera.main.transform.DOLocalMove(goalPreviewCam.localPosition, 2.5f).From())
            .AppendCallback(() =>
            {
                //gameManager.bGamePaused = false;
                //IntroIsPlaying = false;
            })
            .OnComplete(() =>
            {
                //gameManager.bGamePaused = false;
                //IntroIsPlaying = false;
            })
            ;
    }
}
