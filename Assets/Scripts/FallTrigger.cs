using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour {

    public GameObject fallMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<coin>().Respawn();
            GetComponent<AudioSource>().Play();
            if (fallMessage)
            {
                fallMessage.SetActive(false);
                fallMessage.SetActive(true);
            }
        }
    }
}
