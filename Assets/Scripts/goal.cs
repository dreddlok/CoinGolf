using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {

    public AudioClip winSFX;
    public GameObject winScreen;
    public Vector3 rotAngle;

    public bool CoinInGoal = false;

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
            GetComponent<AudioSource>().PlayOneShot(winSFX);
            winScreen.SetActive(true);
            Cursor.visible = true;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.UpdateLevelCompleteScreen();
            gameManager.bGamePaused = true;
            gameManager.SaveLevelAchievements();
        }        
    }
}
