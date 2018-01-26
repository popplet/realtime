using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject plane;
    List<GameObject> planelist;

    // Use this for initialization
    void Awake()
    {
        plane = Resources.Load<GameObject>("MapBase");
        planelist = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject obj = Instantiate<GameObject>(plane);
                obj.transform.parent = this.gameObject.transform;
                obj.name = "Mapbase" + i + j;
                obj.transform.localPosition = new Vector3(-2.7f + (0.6f * j), 0, -2.0f + (0.6f * i));
                planelist.Add(obj);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        //테스트용
        if (Input.GetKeyDown(KeyCode.G))
        {
            planelist[20].SetActive(false);
        }
    }
}

