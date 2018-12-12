using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSfx : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        GetComponent<Slider>().value = FindObjectOfType<PlayerSave>().sfxVolume;
	}
	
}
