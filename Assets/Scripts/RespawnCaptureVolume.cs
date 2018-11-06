using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCaptureVolume : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<coin>().RespawnLocation = other.transform.position;
    }
}
