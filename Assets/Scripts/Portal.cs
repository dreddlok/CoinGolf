using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour {
    
    public Vector3 rotAngle;
    public Transform otherPortal;
    public bool CoinInPortal = false;
    public bool bPortalActive = true;
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

    private void Update()
    {
        transform.Rotate(rotAngle);
    }

    private void OnTriggerEnter(Collider other)
    {
        CoinInPortal = true;
        StartCoroutine(GoalReached());
    }

    private void OnTriggerExit(Collider other)
    {
        CoinInPortal = false;
        bPortalActive = true;
        Debug.Log("coin left portal area" + Time.time);
    }

    IEnumerator GoalReached()
    {
        yield return new WaitForSeconds(.8f);
        if (CoinInPortal && bPortalActive)
        {
            Debug.Log("Portal entered" + Time.time);
            GetComponent<AudioSource>().Play();
            otherPortal.GetComponent<Portal>().bPortalActive = false;
            GameObject.Find("Player").transform.position = otherPortal.position + new Vector3 (0,1,0);
        }
    }
}
