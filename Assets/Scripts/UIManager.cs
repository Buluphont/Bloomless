using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    GameObject[] pauseObjects;
    GameObject[] finishObjects;
    PlayerControllerScript playerController;

	// Use this for initialization
	void Start () 
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
        hidePaused();
        hideFinished();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }
    public void Reload()
    {
       Application.LoadLevel(Application.loadedLevel);
       playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }

    // Update is called once per frame
    void Update () 
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1 && playerController.alive == true)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0 && playerController.alive == true)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }
	}
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }

    }
    public void respawn()
    {
        GameObject.FindWithTag("Game State Manager").GetComponent<CheckpointController>().RespawnFromLastCheckpoint();
        playerController.alive = true;
        Time.timeScale = 1;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        hideFinished();
    }
    public void showPaused()
    {
        foreach (GameObject button in pauseObjects)
        {
            button.SetActive(true);
        }
    }
    public void hidePaused()
    {
        foreach(GameObject button in pauseObjects)
        {
            button.SetActive(false);
        }
    }
    public void showFinished()
    {
        foreach(GameObject button in finishObjects)
        {
            button.SetActive(true);
        }
    }
    public void hideFinished()
    {
        foreach(GameObject button in finishObjects)
        {
            button.SetActive(false);
        }
    }
}
