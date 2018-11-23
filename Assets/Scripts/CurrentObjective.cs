using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CurrentObjective : MonoBehaviour {

    public float tweenDuration = 2;

    private void OnEnable()
    {
        RectTransform rectT = GetComponent<RectTransform>();
        Text text = GetComponent<Text>();
        text.color = Color.white;
        Sequence tweenSequence = DOTween.Sequence()
            .Append(rectT.DOAnchorPosX(-1335, tweenDuration).From())
            .Join(text.DOFade(0, tweenDuration).From());
    }
}
