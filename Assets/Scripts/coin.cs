﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class coin : MonoBehaviour {

    public float flickPower = 100;
    public float rotSpeed = 10;
    public float scaleFactor = .5f; //used to dampen how sensitive the mouse is for choosing flickpower
    public GameObject arrow;
    public Rigidbody coinRB;
    public enum FlickType  { rotateAndFlick, vectorDrag, separateRotNFlick};
    public FlickType flickType;
    public AudioClip coinSlideSFX;
    public AudioClip chinkSFX;
    public Vector3 RespawnLocation;
    public float pitchSpeed = 2.0f;
    public float yawMultiplier = 2;
    public Transform goalPreviewCam;
    public bool bPlayCutScene = true;
    public bool bFlickingAbilitySuspended = false;

    private Vector3 mouseStartPos = Vector3.zero;
    private Vector3 mouseEndPos = Vector3.zero;
    private Vector3 heading;
    private float distance;
    private Vector3 direction;
    public GameManager gameManager;
    private bool bFlickCancelled = false;
    private Vector3 goalViewingCameraPos;

    private float mouseXstart;
    private float mouseYstart;
    private float yaw = 0;
    private float pitch = 0;
    public bool IntroIsPlaying;
    private Vector3 initialVector = Vector3.forward;

    Sequence tweenSequence;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        RespawnLocation = transform.position;

        gameManager = FindObjectOfType<GameManager>();

        if (bPlayCutScene)
        {
            PanCameraFromGoal();
        }
    }

    // Update is called once per frame
    void Update () {
        if (flickType == FlickType.rotateAndFlick)
        {
            RotateAndFlick();
        } else if (flickType == FlickType.vectorDrag)
        {
            VectorDrag();
        } else if (flickType == FlickType.separateRotNFlick)
        {
            SeparateRoateAndFlick();
        }

        if (Input.GetButton("Fire1"))
        {
            yawMultiplier = 2;
        } else
        {
            yawMultiplier = .5f;
        }
        
        if(bPlayCutScene)
        {
            if (Input.GetButtonUp("Fire1") && tweenSequence.IsPlaying())
            {
                if (gameManager.bGamePaused == true)
                {
                    gameManager.bGamePaused = false;
                }
                tweenSequence.Complete();
            }
        }
    }

    public void PanCameraFromGoal()
    {
        tweenSequence = DOTween.Sequence()
            .AppendCallback(() =>
            {
                // this is callback
                yaw = 0;
                pitch = 0;
                gameManager.bGamePaused = true;
                IntroIsPlaying = true;
            })
            .AppendInterval(1.5f)
            .Append(Camera.main.transform.DOLocalMove(goalPreviewCam.localPosition, 2.5f ).From())
            .AppendCallback(() =>
            {
                gameManager.bGamePaused = false;
                IntroIsPlaying = false;
            })
            .OnComplete(() =>
            {
                gameManager.bGamePaused = false;
                IntroIsPlaying = false;
            })
            ;

        if (Input.GetButtonUp("Fire1"))
        {
            tweenSequence.Complete();
        }
    }

    public void Respawn()
    {
        transform.position = RespawnLocation;
        coinRB.velocity = Vector3.zero;
    }

    private void RotateAndFlick()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            coinRB.isKinematic = false;
            coinRB.AddForce(transform.up * flickPower * arrow.transform.localPosition.y);
            arrow.SetActive(false);
            arrow.transform.localPosition = new Vector3(0, .2f, 0);
        }

        if (Input.GetButton("Fire1"))
        {
            coinRB.isKinematic = true;
            transform.Rotate(new Vector3(0, 0, Input.GetAxis("Mouse X") * rotSpeed));
            arrow.transform.localPosition += new Vector3(0, -Input.GetAxis("Mouse Y") * scaleFactor, 0);//new Vector3(0, 0, Input.GetAxis("Mouse Y")* scaleFactor);
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, Mathf.Clamp(arrow.transform.localPosition.y, .2f, 1.5f), arrow.transform.localPosition.z); //new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y, Mathf.Clamp(arrow.transform.localPosition.z, -1f, -.2f));
            arrow.SetActive(true);
        }
    }

    private void VectorDrag()
    {    
        if (Input.GetButtonDown("Fire1"))
        {
            Ray vRayStart = Camera.main.ScreenPointToRay(Input.mousePosition);
            mouseStartPos = vRayStart.origin;
            Debug.Log("mouse start: " + mouseStartPos);
        }

        if (Input.GetButton("Fire1"))
        {
            Ray vRayEnd = Camera.main.ScreenPointToRay(Input.mousePosition);
            mouseEndPos = vRayEnd.origin;
            heading = mouseEndPos - mouseStartPos;
            distance = heading.magnitude;
            direction = heading / distance;
            //Vector2 direction2D = new Vector2(direction.x, direction.y);
            //Vector3 rotatedDir = new Vector3(direction.y, direction.z, direction.x);

            arrow.SetActive(true);
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, Mathf.Clamp(arrow.transform.localPosition.y + distance, .2f, 1.5f), arrow.transform.localPosition.z);

            //dragVector = mouseEndPos - mouseStartPos;

            Debug.DrawLine(mouseStartPos, Input.mousePosition);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            //coinRB.AddForce(heading);
            arrow.SetActive(false);
            arrow.transform.localPosition = new Vector3(0, .2f, 0);
            Debug.Log("mouse drag vector:" + direction);
        }
    }

    private void SeparateRoateAndFlick()
    {
        
        if (!gameManager.bGamePaused)
        {
            yaw += Input.GetAxis("Mouse X");
            pitch += Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(-90, yaw, 0);
            if (Input.GetButton("Fire1") && gameManager.flicksLeft > 0 && !bFlickCancelled && !bFlickingAbilitySuspended)
            {                
                arrow.transform.localPosition -= new Vector3(0, Input.GetAxis("Mouse Y") * .1f, 0);
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, Mathf.Clamp(arrow.transform.localPosition.y, .3f, 1f), arrow.transform.localPosition.z );
                arrow.SetActive(true);
            }
            else
            {
                OrbitCamera();
            }

            if (Input.GetButtonUp("Fire1") && arrow.activeSelf && !bFlickingAbilitySuspended)
            {
                
                if (bFlickCancelled)
                {
                    bFlickCancelled = false;
                }
                else
                {                    
                    arrow.SetActive(false);
                    if (gameManager.flicksLeft > 1)
                    {
                        gameManager.Flick();
                        coinRB.AddRelativeForce(Vector3.up * flickPower * arrow.transform.localPosition.y);
                        arrow.transform.localPosition = new Vector3(0, 0.3f, 0);
                        GetComponent<AudioSource>().Play();
                    }
                    else if (gameManager.flicksLeft > 0)
                    {
                        gameManager.Flick();
                        coinRB.AddRelativeForce(Vector3.up * flickPower * (arrow.transform.localPosition.y));
                        arrow.transform.localPosition = new Vector3(0, .3f, 0);
                        GetComponent<AudioSource>().Play();
                        StartCoroutine(FinalFlick());
                    }
                }
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (Input.GetButton("Fire1"))
                {
                    bFlickCancelled = true;
                    arrow.SetActive(false);
                    arrow.transform.localPosition = new Vector3(0, 0, .3f);
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!Input.GetButton("Fire1"))
                {
                    bFlickCancelled = false;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (!Input.GetButton("Fire2"))
                {
                    bFlickCancelled = false;
                }
            }

            if (coinRB.velocity.magnitude <= 0.1 && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Stop();
            }            
        }
    }

    IEnumerator FinalFlick()
    {
        yield return new WaitForSeconds(1.2f);
        if (FindObjectOfType<goal>().CoinInGoal == false)
        {
            gameManager.OutOfFlicks();
        }
    }

    private void OrbitCamera()
    {
        Transform cameraTransform = Camera.main.transform;
        cameraTransform.RotateAround(transform.position, transform.right, Input.GetAxis("Mouse Y"));
        float x = cameraTransform.localEulerAngles.x;
        if (x < 290)
        {
            cameraTransform.RotateAround(transform.position, transform.right, -Input.GetAxis("Mouse Y"));
        }
        if (x > 350)
        {
            cameraTransform.RotateAround(transform.position, transform.right, -Input.GetAxis("Mouse Y"));
        }
    }
    
}
