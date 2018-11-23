using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour {

    public Text tutorialText;
    public GameObject clickToCont;
    public Text CurrentObjectiveDisplay;
    [TextArea]
    public string CurrentObjectiveText;
    public float textTweenDuration = 5;
    public GameObject nextTutorialObject;

    public CanvasRenderer canvasGroup;
    private bool tweenIsPlaying;
    Sequence textTween;
    public bool bPlayNoise;
    public float tweenDuration = 1.5f;

    private void OnEnable()
    {
        CurrentObjectiveDisplay.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().bGamePaused = true;
        RectTransform rt = GetComponent<RectTransform>();
        //float x;
        //DOTween.To(canvasGroup.GetAlpha(), x => canvasGroup.SetAlpha = x, 0, tweenDuration).From();
        Sequence tweenSequence = DOTween.Sequence()
            .Append(rt.DOAnchorPosY(rt.position.y - 100, tweenDuration).From());
            //.Join(canvasGroup. //.DOFade(0, tweenDuration).From());

        textTween = DOTween.Sequence()
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

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            if (tweenIsPlaying)
            {
                textTween.Complete();
            } else
            {
                CurrentObjectiveDisplay.gameObject.SetActive(false);
                CurrentObjectiveDisplay.gameObject.SetActive(true);
                CurrentObjectiveDisplay.text = CurrentObjectiveText;
                FindObjectOfType<GameManager>().bGamePaused = false;
                if(nextTutorialObject)
                {
                    nextTutorialObject.SetActive(true);
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}
