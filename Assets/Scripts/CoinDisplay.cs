using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour {

    public Image collectable1;
    public Image collectable2;
    public Image collectable3;
    public Color solidCol;

    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void RefreshImages()
    {
        if (gameManager.CoinsCollected == 3)
        {
            collectable3.color = solidCol;
        }
        else if (gameManager.CoinsCollected == 2)
        {
            collectable2.color = solidCol;
        }
        else if (gameManager.CoinsCollected == 1)
        {
            collectable1.color = solidCol;
        }
    }
}
