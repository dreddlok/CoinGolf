using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour {

    private bool bCoinCollected = false;
    public Material glowMaterial;
    public float fadeDuration = 3;
    private float emissionScale = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (!bCoinCollected && collision.gameObject.tag == "Player")
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            bCoinCollected = true;
            GetComponent<AudioSource>().Play();
            FindObjectOfType<GameManager>().AddCoin();
            renderer.material = glowMaterial;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshCollider>().enabled = false;
        }
    }
}
