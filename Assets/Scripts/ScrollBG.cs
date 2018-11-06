using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBG : MonoBehaviour {

    public float scrollSpeed = 0.5f;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float offset = Time.deltaTime * scrollSpeed;
        RawImage image = GetComponent<RawImage>();
        image.uvRect = new Rect(image.uvRect.x + offset, image.uvRect.y, image.uvRect.width, image.uvRect.height);
    }
}
