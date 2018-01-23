using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStone : MonoBehaviour {



    public GameObject enemyOriginStone;//흰색 바둑알
    public List<GameObject> eList;//적 바둑알 리스트

    public bool canCntr;
    void Awake()
    {

        for (int i = 0; i < 5; i++)
        {
            //적바둑알 복사
            GameObject newEobj = Instantiate(enemyOriginStone);
            newEobj.name = "e_" + i.ToString();
            eList.Add(newEobj);
            eList[i].transform.position = new Vector3(-2+i, 0.5f, 2);
        }
        canCntr = false;
    }

    void Start ()
    {
		
	}

  
    // Update is called once per frame
    void Update ()
    {
        if (canCntr)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("turnChangeW");
            }
        }
		
	}

    IEnumerator turnChangeW()
    {
        canCntr = false;
        yield return new WaitForSeconds(3f);
        GetComponentInParent<gameMaster>().wHoTurn = 1;//검은바둑알에게 턴 넘겨줌
        GetComponentInParent<gameMaster>().turnChange = true;

    }






}
