using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerGrow : MonoBehaviour {
    
    public Vector3 endScale;
    private RectTransform rectTransform;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, endScale, Time.deltaTime);
    }
}
