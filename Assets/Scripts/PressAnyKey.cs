using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour {

    public GameObject titleMenu;
    public GameObject splashScreen;
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            titleMenu.SetActive(true);
            splashScreen.SetActive(false);
        }
    }
}
