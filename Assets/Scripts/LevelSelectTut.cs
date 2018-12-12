using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelectTut : MonoBehaviour {

    public float tweenDuration;
    public float textTweenDuration;
    public Text tutorialText;
    public GameObject clickToCont;
    public GameObject panel;

    private bool tweenIsPlaying;
    Sequence textTween;

    // Use this for initialization
    void Start () {
        if (FindObjectOfType<PlayerSave>().totalCoins > 3)
        {
            panel.gameObject.SetActive(false);
        }

        RectTransform rt = GetComponent<RectTransform>();
        Sequence tweenSequence = DOTween.Sequence()
            .Append(rt.DOAnchorPosY(rt.position.y - 100, tweenDuration).From());
        Sequence textTween = DOTween.Sequence()
            .AppendCallback(() =>
            {
                GetComponent<AudioSource>().Play();
                tweenIsPlaying = true;
            })
            .Append(tutorialText.DOText("", textTweenDuration, true).From())
            .AppendCallback(() =>
            {
                clickToCont.gameObject.SetActive(true);
            })
            .OnComplete(() =>
            {
                tweenIsPlaying = false;
                GetComponent<AudioSource>().Stop();
                clickToCont.gameObject.SetActive(true);
            });
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire1"))
        {
            if (tweenIsPlaying)
            {
                textTween.Complete();
            }
            else
            {
                panel.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
