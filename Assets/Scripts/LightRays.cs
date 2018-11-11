using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LightRays : MonoBehaviour {

    public Vector3 rotAngle;
    public float rotDuration;

    // Use this for initialization
    void Start () {
        GetComponent<RectTransform>().DORotate(rotAngle, rotDuration).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
	}
	
}
