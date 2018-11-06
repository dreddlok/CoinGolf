using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSFXonCollision : MonoBehaviour {

    public AudioClip chinkSFX;

    private void OnCollisionEnter(Collision collision)
    {
        //AudioSource.PlayClipAtPoint(chinkSFX, transform.position, 1);
    }
}
