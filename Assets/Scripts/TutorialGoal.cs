using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGoal : MonoBehaviour {
    
    public Vector3 rotAngle;
    public GameObject congratulations;
    public bool CoinInGoal = false;
    public GameObject nexttut;
    public Transform teleportLocation;
    public GameObject player;

    private void Update()
    {
        transform.Rotate(rotAngle);
    }

    private void OnTriggerEnter(Collider other)
    {
        CoinInGoal = true;
        StartCoroutine(GoalReached());
    }

    private void OnTriggerExit(Collider other)
    {
        CoinInGoal = false;
        Debug.Log("coin left goal area" + Time.time);
    }

    IEnumerator GoalReached()
    {
        yield return new WaitForSeconds(.8f);
        if (CoinInGoal)
        {
            Debug.Log("Goal reached!!!" + Time.time);
            congratulations.SetActive(true);
            nexttut.SetActive(true);
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.bGamePaused = true;
            player.transform.position = teleportLocation.position;
            coin playerCoin = player.GetComponent<coin>();
            playerCoin.RespawnLocation = player.transform.position;
        }        
    }
}
