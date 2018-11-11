using System.Collections;
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

    private Vector3 mouseStartPos = Vector3.zero;
    private Vector3 mouseEndPos = Vector3.zero;
    private Vector3 mouseDragVector = Vector3.zero;
    private Vector3 heading;
    private float distance;
    private Vector3 direction;
    private GameManager gameManager;
    private bool bFlickCancelled = false;

    private float mouseXstart;
    private float mouseYstart;
    private float yaw = 0;
    private float pitch = 0;

    private Vector2 dragVector;
    private bool IntroIsPlaying;

    private void Start()
    {
        //mouseXstart = Input.mousePosition.x;
        //mouseYstart = Input.mousePosition.y;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        RespawnLocation = transform.position;

        gameManager = FindObjectOfType<GameManager>();

        PanCameraFromGoal();
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
        Debug.Log("Yaw: " + yaw);
    }

    public void PanCameraFromGoal()
    {
        Sequence tweenSequence = DOTween.Sequence()
            .AppendCallback(() =>
            {
                // this is callback
                yaw = 0;
                pitch = 0;
                gameManager.bGamePaused = true;
                IntroIsPlaying = true;
            })
            .AppendInterval(1.5f)
            .Append(Camera.main.transform.DOLocalMove(new Vector3(1.03f, 2.45f, -2.66f), 2.5f ).From())
            .AppendCallback(() =>
            {
                // this is callback
                gameManager.bGamePaused = false;
                IntroIsPlaying = false;

            }); 
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
            Vector2 direction2D = new Vector2(direction.x, direction.y);
            Vector3 rotatedDir = new Vector3(direction.y, direction.z, direction.x);

            arrow.SetActive(true);
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, Mathf.Clamp(arrow.transform.localPosition.y + distance, .2f, 1.5f), arrow.transform.localPosition.z);

            dragVector = mouseEndPos - mouseStartPos;

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



            if (Input.GetButton("Fire1") && gameManager.flicksLeft > 0 && !bFlickCancelled)
            {

                arrow.transform.localPosition += new Vector3(0, 0, Input.GetAxis("Mouse Y"));
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y, Mathf.Clamp(arrow.transform.localPosition.z, -1f, -.3f));
                arrow.SetActive(true);

            }
            else
            {
                transform.eulerAngles = new Vector3(-90, yaw * 2, 0);
                Camera.main.transform.RotateAround(transform.position, transform.right, Input.GetAxis("Mouse Y"));

            }

            if (Input.GetButtonUp("Fire1") && arrow.activeSelf)
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
                        coinRB.AddRelativeForce(Vector3.up * flickPower * -(arrow.transform.localPosition.z));

                        arrow.transform.localPosition = new Vector3(0, 0, -.3f);
                        GetComponent<AudioSource>().Play();
                    }
                    else if (gameManager.flicksLeft > 0)
                    {
                        gameManager.Flick();
                        coinRB.AddRelativeForce(Vector3.up * flickPower * -(arrow.transform.localPosition.z));

                        arrow.transform.localPosition = new Vector3(0, 0, -.3f);
                        GetComponent<AudioSource>().Play();
                        StartCoroutine(FinalFlick());
                    }
                }
            }

            if (Input.GetButton("Fire2"))
            {
                if (Input.GetButton("Fire1"))
                {
                    bFlickCancelled = true;
                    arrow.SetActive(false);
                    arrow.transform.localPosition = new Vector3(0, 0, -.3f);
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

}
