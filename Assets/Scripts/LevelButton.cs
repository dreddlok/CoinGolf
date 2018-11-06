using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    public int level;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public Sprite lockedScoreSprite;
    public Sprite aCrown;
    public Sprite bCrown;
    public Sprite cCrown;
    public Color solidColor;
    public GameObject tooltip;

    private Button button;
    private PlayerSave playerSave;
    private Image coinImage1;
    private Image coinImage2;
    private Image coinImage3;
    private Image score;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();
        this.gameObject.GetComponentInChildren<Text>().text = playerSave.title[level];
        coinImage1 = transform.Find("Coin1").GetComponent<Image>();
        coinImage2 = transform.Find("Coin2").GetComponent<Image>();
        coinImage3 = transform.Find("Coin3").GetComponent<Image>();
        score = transform.Find("Score").GetComponent<Image>();
        switch (playerSave.levelCoins[level])
        {
            case 0:
                break;
            case 1:
                coinImage1.color = solidColor;
                break;
            case 2:
                coinImage1.color = solidColor;
                coinImage2.color = solidColor;
                break;
            case 3:
                coinImage1.color = solidColor;
                coinImage2.color = solidColor;
                coinImage3.color = solidColor;
                break;
        }
        if (playerSave.totalCoins < playerSave.unlockRequirement[level])
        {
            score.sprite = lockedScoreSprite;
        }
        else
        {
            switch (playerSave.levelGrade[level])
            {
                case PlayerSave.LevelGrade.Ungraded:
                    score.sprite = null;
                    break;
                case PlayerSave.LevelGrade.A:
                    score.sprite = aCrown;
                    break;
                case PlayerSave.LevelGrade.B:
                    score.sprite = bCrown;
                    break;
                case PlayerSave.LevelGrade.C:
                    score.sprite = cCrown;
                    break;
            }
        }
    }

    private void OnGUI()
    {
        PlayerSave playerData = FindObjectOfType<PlayerSave>();
        button = GetComponent<Button>();
        if (playerData.level[level])
        {
            button.image.sprite = unlockedSprite;
            button.interactable = true;
        }
        else
        {
            button.image.sprite = lockedSprite;
            button.interactable = false;
        }
    }

    public void MouseEnter()
    {
        if (playerSave.totalCoins < playerSave.unlockRequirement[level])
        {
            tooltip.transform.Find("Text").GetComponent<Text>().text = playerSave.unlockRequirement[level].ToString() + " coins needed to unlock this level";
            tooltip.SetActive(true);
        }
    }

    public void MouseExit()
    {
        if (playerSave.totalCoins < playerSave.unlockRequirement[level])
        {            
            tooltip.SetActive(false);
        }
    }

}
