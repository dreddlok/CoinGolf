using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Redirect : MonoBehaviour {

    public Vector3 rotAngle;
    public float redirectPower;
    public bool bMoving = false;
    public GameObject moveTarget;
    public float moveDuration = 1.5f;

    private void Start()
    {
        if (bMoving)
        {
            transform.DOMove(moveTarget.transform.position, moveDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!FindObjectOfType<GameManager>().bGamePaused && other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            Rigidbody coinRB = GameObject.Find("Player").GetComponent<Rigidbody>();
            coinRB.velocity = Vector3.zero;
            coinRB.AddForce(transform.right * redirectPower);
        }
    }    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotAngle);
    }
}
