using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour {

    private bool bCoinCollected = false;
    public Material glowMaterial;
    public float fadeDuration = 3;

    public AudioClip oneSFX;
    public AudioClip twoSFX;
    public AudioClip threeSFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (!bCoinCollected && collision.gameObject.tag == "Player")
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            bCoinCollected = true;

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.AddCoin();
            AudioSource audioSource = GetComponent<AudioSource>();
            switch (gameManager.CoinsCollected)
            {
                case 1:
                    audioSource.clip = oneSFX;
                    break;
                    case 2:
                    audioSource.clip = twoSFX;
                    break;
                case 3:
                    if (!gameManager.bInTutorial)
                    {
                        audioSource.clip = threeSFX;
                    }
                    break;
            }
            audioSource.Play();
            renderer.material = glowMaterial;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshCollider>().enabled = false;
        }
    }
}
