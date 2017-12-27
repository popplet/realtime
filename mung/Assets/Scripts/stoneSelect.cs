using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneSelect : MonoBehaviour
{


    public GameObject orignStone;//검은색 바둑알
    public GameObject enemyOriginStone;//흰색 바둑알


    public List<GameObject> sList;//내 바둑알 리스트
    public List<GameObject> eList;//적 바둑알 리스트

    public GameObject target;  //선택용 타겟
    public int startCount;      //게임시작위해 두는 바둑알 갯수
    public Vector3 tPos;        //y값 포함된 위치 
    public Vector3 tPosr;       //진짜 발사 위치(y값제외)
    public int powerF;          //파워

    public string firstNum;         //현재 바둑알 번호
    public string lastNum;          //더 이상 못 바꾸는 바둑알
    public bool isReady;            //최종선고
    public bool readyToFire;        //발사준비완료

    public int selectCount;         // 확인할것

    private Vector3 fireDirection;   //발사방향
    public Vector3 fireDirectionR;  //Y축 제외
    public float firePower;         //발사 힘\
    public float timePower;         //clamp 확인용
    
    public float clamps = 0.0f;         // clamp 까지 거친 결과값
    public float max = 3.0f;           // 당기는 길이 최대값
    public float current = 1.0f;        // 현재 얼마나 당겼는지 거리값
    public float powermax = 15.0f;      // 알에 가해지는 힘 최대값
    public float powermin = 3.0f;       // 알에 가해지는 힘 최소값

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
        isReady = false;
        readyToFire = false;
        selectCount = 0;
        timePower = 0;
        //LineMax = 3.0f;
      
        
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

        //알 선택하는 과정 - 보류



        //발사 방향 잡기
        if (target.gameObject != null)
        {
            if (target.gameObject.tag == "myStone")
            {
                if (Input.GetMouseButton(0))
                {
                    fireDirection = target.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                fireDirectionR = new Vector3(fireDirection.x, 0, fireDirection.z);
                //발사 힘 주기
                /* firePower = 
                     (Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z),
                     new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z))
                     /LineMax)*15.0f;*/

                //최소 3~ 최대 15의 힘 
                //timePower = Mathf.Clamp(firePower, 3.0f, 15.0f);

                //3 + (4 * Mathf.Clamp(firePower, 0.5f, 3.0f));


                float currentpower = (Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z),
                     new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z))
                     / max) * powermax;
                clamps = Mathf.Clamp(currentpower, powermin, powermax);
                //발사!
                if (Input.GetMouseButtonUp(0))
                {
                    target.GetComponent<Rigidbody>().AddForce(fireDirectionR.normalized *
                        clamps, ForceMode.Impulse);
                }
              
            }
        }
    }
    private void OnGUI()
    {
        

        
    }


    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            castpRay();
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
