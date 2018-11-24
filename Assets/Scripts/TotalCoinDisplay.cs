using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoinDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<PlayerSave>().RefreshTotalCoins();
        GetComponent<Text>().text = FindObjectOfType<PlayerSave>().totalCoins.ToString();
	}
}
