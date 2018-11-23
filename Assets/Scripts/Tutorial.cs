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

    private bool tweenIsPlaying;
    Sequence textTween;
    public bool bPlayNoise;

    private void OnEnable()
    {
        FindObjectOfType<GameManager>().bGamePaused = true;
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
