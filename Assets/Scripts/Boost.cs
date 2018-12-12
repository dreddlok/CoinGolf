using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boost : MonoBehaviour {

    public Vector3 rotAngle;
    public float BoostPower;
    public bool bMoving = false;
    public GameObject moveTarget;
    public float moveDuration = 1.5f;

    private void Start()
    {
        if (bMoving)
        {
            transform.DOMove(moveTarget.transform.position, moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
        Rigidbody coinRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        coinRB.velocity = coinRB.velocity.normalized  * BoostPower;
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(rotAngle);
    }
}
