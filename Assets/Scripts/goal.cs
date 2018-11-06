using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {

    public AudioClip winSFX;
    public GameObject winScreen;

    public bool CoinInGoal = false;

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
            yield return new WaitForSeconds(3);
            FindObjectOfType<LevelManager>().LoadLevel("Level Select");
        }        
    }
}
