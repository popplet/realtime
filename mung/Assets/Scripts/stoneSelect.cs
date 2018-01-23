using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneSelect : MonoBehaviour
{
    public GameObject orignStone;//검은색 바둑알
    public List<GameObject> sList;//내 바둑알 리스트

    public GameObject target;  //선택용 타겟
    public int startCount;      //게임시작위해 두는 바둑알 갯수
  
   
    private Vector3 fireDirection;  //발사방향
    public Vector3 fireDirectionR;  //Y축 제외
 
    public float clamps = 0.0f;         // clamp 까지 거친 결과값
    public float max = 3.0f;           // 당기는 길이 최대값
    public float current = 1.0f;        // 현재 얼마나 당겼는지 거리값
    public float powermax = 15.0f;      // 알에 가해지는 힘 최대값
    public float powermin = 3.0f;       // 알에 가해지는 힘 최소값


    public bool canCntr;                //조종가능?

    void Awake()
    {
        sList = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject newObj = Instantiate(orignStone);
            newObj.name = i.ToString();

            sList.Add(newObj);
            sList[i].SetActive(false);



        }
        startCount = 0;
        canCntr = false;
        //LineMax = 3.0f;


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //최초의 바둑알 놓기
        setPlay();


        //알 선택하는 과정 - 보류


        //발사 방향 잡기

        if (target.gameObject != null)
        {
            if (target.gameObject.tag == "myStone"&&canCntr&& 
                GetComponentInParent<gameMaster>().wHoTurn ==1)//검은바둑알 턴일때만 발사 가능
            {
                if (Input.GetMouseButton(0))
                {
                    fireDirection = target.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                fireDirectionR = new Vector3(fireDirection.x, 0, fireDirection.z);
                //발사 힘 주기
                float currentpower = (Vector3.Distance(new Vector3(target.transform.position.x, 0, target.transform.position.z),
                     new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z))
                     / max) * powermax;
                clamps = Mathf.Clamp(currentpower, powermin, powermax);
                //발사!
                if (Input.GetMouseButtonUp(0))
                {
                    target.GetComponent<Rigidbody>().AddForce(fireDirectionR.normalized *
                        clamps, ForceMode.Impulse);
                    StartCoroutine("turnChange");
                }
            }
        }
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
    IEnumerator turnChange()//턴 넘기는 시간
    {
        //한턴에 한발만 움직일 수 있어야 함 코루틴 시작됨가 동시에 검은알의 컨트롤 조건을 false로 바꿔서 더이상 못 건드리게 해야한다.
        canCntr = false;
        yield return new WaitForSeconds(3f);
        GetComponentInParent<gameMaster>().wHoTurn = 2;//흰 바둑알에게 턴 넘겨줌
        GetComponentInParent<gameMaster>().turnChange = true;
    
    }
    void setPlay()//최초1회 실행
    {
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
                if (startCount == 5)
                {
                    GetComponentInParent<gameMaster>().wHoTurn = 1;//게임의 시작을 알림, 검은바둑알 턴
                    canCntr = true;
                }
            }
        }

    }
}
