using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FallMessage : MonoBehaviour {

    public float tweenDuration = 2;

    private void OnEnable()
    {
        RectTransform rectT = GetComponent<RectTransform>();
        Text text = GetComponent<Text>();
        text.color = Color.white;
        Sequence tweenSequence = DOTween.Sequence()
            .Append(rectT.DOAnchorPosY(565, tweenDuration).From())
            .Join(text.DOFade(0, tweenDuration).From())
            .AppendInterval(2.25f)
            .Append(text.DOFade(0, tweenDuration * 2))
            .OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            })
            ;
    }
}
