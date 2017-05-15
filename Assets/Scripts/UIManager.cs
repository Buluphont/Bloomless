using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
    GameObject[] pauseObjects;

	// Use this for initialization
	void Start () 
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
	}

    public void Reload()
    {
       Application.LoadLevel(Application.loadedLevel);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}
