using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SavingIcon : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Text text = transform.Find("Text").GetComponent<Text>();
        Image coin = text.transform.Find("Coin Spin").GetComponent<Image>();

        text.DOFade(0, 4).From();
        coin.DOFade(0, 4).From();
    }
}
