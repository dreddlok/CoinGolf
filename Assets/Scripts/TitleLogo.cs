using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleLogo : MonoBehaviour {

    [RangeAttribute(0, 1)]
    public float fadeAlpha;    
    public float fadeTime;
    public float moveDuration;
    private Vector2 moveTarget;
    public Color startCol;

    
    // Use this for initialization
    void Start () {
        moveTarget = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200.0f);
        GetComponent<RectTransform>().DOAnchorPos( moveTarget, moveDuration, false);
        GetComponent<Image>().color = startCol;
        GetComponent<Image>().DOFade(1, moveDuration);//.OnComplete();
    }
}
