using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFirstGoal : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<GameManager>().bGamePaused = false;
        FindObjectOfType<coin>().bFlickingAbilitySuspended = false;
    }
}
