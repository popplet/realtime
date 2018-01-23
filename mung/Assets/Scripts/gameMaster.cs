using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameMaster : MonoBehaviour
{
    enum turn
    {
        player = 1,
        enemy = 2
    };
    stoneSelect bScontr; //검은알컨트롤
    enemyStone wScontr;  //흰바둑알 컨트롤
    public int wHoTurn;
    public bool turnChange;//턴 변경 될 때마다 각 바둑알 컨트롤 할 수 1회 실행 조건 만들어주기

    void Awake()
    {
        //각각 컨트롤 할 것
        bScontr = this.GetComponentInChildren<stoneSelect>();
        wScontr = this.GetComponentInChildren<enemyStone>();
        wHoTurn = 0;//0일땐 게임 아직 시작 안 한것
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (turnChange)
        {
            if (wHoTurn == 1)
            {
                if (!bScontr.canCntr)
                {
                    bScontr.canCntr = true;
                    turnChange = false;
                }
            }
            else if (wHoTurn == 2)
            {
                if (!wScontr.canCntr)
                {
                    wScontr.canCntr = true;
                    turnChange = false;
                }
            }
        }

    }


}
