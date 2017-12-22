using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class touchTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	//UI 눌렀는지 판정하기위한 코드
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI누름");
        }

        


    }
}
