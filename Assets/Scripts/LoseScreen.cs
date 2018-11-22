using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Find("BG Level Complete").GetComponent<Image>().DOFade(0,1).From();
        transform.Find("Text").GetComponent<Text>().DOFade(0,2).From();
        transform.Find("Text").GetComponent<RectTransform>().DOScale(1.1f, 5);
    }
	
	
}
