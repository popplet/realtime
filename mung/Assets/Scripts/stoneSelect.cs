using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneSelect : MonoBehaviour
{


    public GameObject orignStone;//검은색 바둑알
    public GameObject enemyOriginStone;//흰색 바둑알


    public List<GameObject> sList;//내 바둑알 리스트
    public List<GameObject> eList;//적 바둑알 리스트

    private GameObject target;  //선택용 타겟
    public int startCount;      //게임시작위해 두는 바둑알 갯수
    public Vector3 tPos;        //y값 포함된 위치 
    public Vector3 tPosr;       //진짜 발사 위치(y값제외)
    public int powerF;          //파워


    public float currentAngle;  //직빵 내적 벡터
    public float playerAngle;   //플레이어가 실제로 쏜 내적 벡터
    
    void Awake()
    {
        sList = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject newObj = Instantiate(orignStone);
            newObj.name = i.ToString();
            sList.Add(newObj);
            sList[i].SetActive(false);

            //적바둑알 복사
            GameObject newEobj = Instantiate(enemyOriginStone);
            newEobj.name = "e_" + i.ToString();
            eList.Add(newEobj);
            eList[i].transform.position = new Vector3(Random.Range(-2, 2), 0.5f, Random.Range(0, 2));

        }
        startCount = 0;
        powerF = 0;
        currentAngle = 1.0f; //1이면 제대로 정빵 
        playerAngle = 0.0f;
      
        
    }

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //최초의 바둑알 놓기
        if (startCount < 5)
        {
            if (Input.GetMouseButtonDown(0))//게임시작
            {
                Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 mPosr = new Vector3(mPos.x, 0.5f, mPos.z);
                Debug.Log(mPosr);
                sList[startCount].transform.position = mPosr;
                sList[startCount].SetActive(true);
                startCount++;
            }
        }


        //내적값 구하기 -> 플레이어가 알 선택하면 동작 하면됨
      /* playerAngle = Vector3.Dot(Vector3.Normalize(eList[0].transform.position - target.transform.position),
           Vector3.Normalize(tPosr - target.transform.position));
           */
    }
    
    
    
   private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            castpRay();
        }

        //방향지정
        if (Input.GetKeyDown(KeyCode.D))
        {
            tPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tPosr = new Vector3(tPos.x, 0, tPos.z);
            Debug.Log(tPosr);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 fPos = tPosr - target.transform.position;
            target.GetComponent<Rigidbody>().AddForce(fPos.normalized * powerF, ForceMode.Impulse);
        }

    }
    void castpRay()
    {
        target = null;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray pRay;
        pRay = new Ray(pos, Vector3.down);
        RaycastHit hit;

        hit = new RaycastHit();
        if (Physics.Raycast(pRay, out hit, 10.0f))
        {
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                target = hit.collider.gameObject;
            }
        }
    }
    

}
