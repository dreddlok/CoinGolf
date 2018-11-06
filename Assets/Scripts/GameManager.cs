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

    [Header("Images")]
    public Image scoreCrown;
    public Sprite goldCrown;
    public Sprite silverCrown;
    public Sprite bronzeCrown;

    // Use this for initialization
    void Start () {
        flickDisplay.text = "SHOTS " + flicksLeft.ToString(); //TODO remove game manager from start and level select scenes
        CoinDisplay = FindObjectOfType<CoinDisplay>();
        scoreCrown = GameObject.Find("Score Crown").GetComponent<Image>();
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

    }

    public void OutOfFlicks()
    {
        StartCoroutine(QuitLevel());
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
	
}
