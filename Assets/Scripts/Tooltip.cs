using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 55);
    }
}
