using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPFlyingCamera : MonoBehaviour {

    public float camspeed = 1;
    
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Mouse X") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        var strafe = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        transform.Translate(strafe, 0, 0);
        
    }
}
