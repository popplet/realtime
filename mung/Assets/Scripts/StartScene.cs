using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    public bool isStart = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isStart)
        {
            SceneManager.LoadScene(1);
        }
	}
    public void startGame()
    {
        isStart = true;
    }
}
