using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveTut : MonoBehaviour {

    public coin coin;
    public GameObject congratulations;
    public GameObject nextTutObject;
    public GameManager gameManager;

    RaycastHit hit;
    // Use this for initialization
    void Start () {
        coin.bFlickingAbilitySuspended = true;

    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward * 10);
        // check for raycast hit from camera to goal
        if (Physics.Raycast(ray, out hit, 1000))
        {
            // if true
            if (hit.collider.tag == "TutorialGoal")
            {
                Debug.Log("goal collider hit" + Time.deltaTime);
                congratulations.SetActive(true);
                nextTutObject.SetActive(true);
                gameManager.bGamePaused = true;
                //teleport coin
                this.gameObject.SetActive(false);
            }
        }
	}
}
