using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Slider>().value = FindObjectOfType<PlayerSave>().volume;
        Debug.Log("slider value = " + GetComponent<Slider>().value + "and player volume = " + FindObjectOfType<PlayerSave>().volume);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
