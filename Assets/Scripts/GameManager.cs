using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Header("Level Attributes")]
    public int flicksLeft = 5;
    public int ARank = 6;
    public int BRank = 3;
    public int CRank = 1;

    [Header("Misc")]
    public Text flickDisplay;
    public CoinDisplay CoinDisplay;
    public AudioClip loseSFX;
    public GameObject loseScreen;
    public bool bGamePaused = false;
    public GameObject pauseMenu;
    public int CoinsCollected = 0;
    public string levelGrade; // set at the end of the game in UpdateLevelCompleteScreen() used to save to playersave

    [Header("Tutorial")]
    public bool bInTutorial = false;
    public GameObject congratulations;
    public GameObject nextTut;
    public Transform player;
    public Transform teleportLocation;
    public Text CurrentObjectiveDisplay;
    public GameObject shotsPanel;
    public GameObject shotCount;
    public AudioClip failChallenge;

    [Header("Images")]
    public Image scoreCrown;
    public Sprite goldCrown;
    public Sprite silverCrown;
    public Sprite bronzeCrown;
    public Sprite largeGoldCrown;
    public Sprite largeSilverCrown;
    public Sprite largeBronzeCrown;

    [Header("Level Complete UI Elements")]
    public Image LargeScoreCrown;
    public Image Coin1;
    public Image Coin2;
    public Image Coin3;

    // Use this for initialization
    void Start () {
        flickDisplay.text = "SHOTS " + flicksLeft.ToString(); //TODO remove game manager from start and level select scenes
        CoinDisplay = FindObjectOfType<CoinDisplay>();
        scoreCrown = GameObject.Find("Score Crown").GetComponent<Image>();
        //LargeScoreCrown = GameObject.Find("Large Score Crown").GetComponent<Image>();
        //Coin1 = GameObject.Find("Collectable").GetComponent<Image>();
        //Coin2 = GameObject.Find("Collectable2").GetComponent<Image>();
        //Coin3 = GameObject.Find("Collectable3").GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("OpenMenu"))
        {
            PauseGame();            
        }
    }

    public void AddCoin()
    {
        CoinsCollected++;
        CoinDisplay.RefreshImages();
        if (bInTutorial)
        {
            CurrentObjectiveDisplay.text = "Collect three coins (" + CoinsCollected + "/3)";
            if (CoinsCollected >= 3)
            {
                congratulations.SetActive(true);
                nextTut.SetActive(true);
                bGamePaused = true;
                player.position = teleportLocation.position;
                coin playerCoin = player.GetComponent<coin>();
                playerCoin.RespawnLocation = player.transform.position;
                flicksLeft = 3;
                flickDisplay.text = "SHOTS " + flicksLeft.ToString();
                CoinDisplay.gameObject.SetActive(true);
                shotCount.SetActive(true);
                shotsPanel.SetActive(true);
            }            
        }
    }

    public void OutOfFlicks()
    {
        if (!bInTutorial)
        {
            StartCoroutine(QuitLevel());
        }
        else
        {            
            GetComponent<AudioSource>().clip = failChallenge;
            GetComponent<AudioSource>().Play();
            player.position = teleportLocation.position;
            flicksLeft = 3;
            flickDisplay.text = "SHOTS " + flicksLeft.ToString();
        }
    }

    public void PauseGame()
    {
        bGamePaused = !bGamePaused;
        Cursor.visible = bGamePaused;
        Debug.Log("Pause state = " + bGamePaused + Time.deltaTime);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    IEnumerator QuitLevel()
    {
        Debug.Log("Level Failed" + Time.time);
        GetComponent<AudioSource>().PlayOneShot(loseSFX);
        loseScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        FindObjectOfType<LevelManager>().LoadLevel("Level Select");
    }

    public void Flick()
    {
        flicksLeft--;
        flickDisplay.text = "SHOTS " + flicksLeft.ToString();
        if (flicksLeft >= ARank)
        {
            scoreCrown.sprite = goldCrown;
        }
        else if (flicksLeft >= BRank)
        {
            scoreCrown.sprite = silverCrown;
        }
        else if (flicksLeft >= CRank)
        {
            scoreCrown.sprite = bronzeCrown;
        }
    }

    public void UpdateLevelCompleteScreen()
    {
        Coin1.color = CoinDisplay.collectable1.color;
        Coin2.color = CoinDisplay.collectable2.color;
        Coin3.color = CoinDisplay.collectable3.color;

        if (flicksLeft >= ARank)
        {
            LargeScoreCrown.sprite = largeGoldCrown;
            levelGrade = "A";
        }
        else if (flicksLeft >= BRank)
        {
            LargeScoreCrown.sprite = largeSilverCrown;
            levelGrade = "B";
        }
        else if (flicksLeft >= CRank)
        {
            LargeScoreCrown.sprite = largeBronzeCrown;
            levelGrade = "C";
        }
    }

    public void SaveLevelAchievements()
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        if (playerSave.levelCoins[playerSave.currentlevel] < CoinsCollected)
        {
            playerSave.levelCoins[playerSave.currentlevel] = CoinsCollected;
        }
        switch (levelGrade)
        {
            case "A":
                playerSave.levelGrade[playerSave.currentlevel] = PlayerSave.LevelGrade.A;
                break;
            case "B":
                if(playerSave.levelGrade[playerSave.currentlevel] == PlayerSave.LevelGrade.C || playerSave.levelGrade[playerSave.currentlevel] == PlayerSave.LevelGrade.Ungraded)
                {
                    playerSave.levelGrade[playerSave.currentlevel] = PlayerSave.LevelGrade.B;
                }
                break;
            case "C":
                if (playerSave.levelGrade[playerSave.currentlevel] == PlayerSave.LevelGrade.Ungraded)
                {
                    playerSave.levelGrade[playerSave.currentlevel] = PlayerSave.LevelGrade.C;
                }
                break;
        }
        playerSave.RefreshTotalCoins();
        playerSave.Save();
    }
	
}
