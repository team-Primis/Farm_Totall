using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControll : MonoBehaviour
{
    //플레이어관련 : 움직임, 보유 돈, 보유 씨앗, 달걀 관련

    public int money = 2000;
    public bool isMoneyChanged = false;

    public int laborCount = 5;
    GameManager GMScript;

    public string item; //현재 손에 든 아이템

    // from playercontoller
    public int numGE = 0; // 보유 중인 좋은 알의 개수
    public int numNE = 0; // 보유 중인 보통 알의 개수

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        //체력 감소에 대한 코드 : 10번 행동이 끝나면 1칸씩 준다
        if (Input.GetKeyDown(KeyCode.Space)) {
            laborCount--;
            if (laborCount == 0)
            {
                laborCount = 5;
                if (GMScript.stamina > 0)
                {
                    GMScript.stamina--;
                    if (GMScript.stamina < 2) speed = 0.5f;
                    GMScript.isStaminaChanged = true;
                }
            }
        }
        */
    }

    // from playercontoller
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "GoodEgg") // GoodEgg가 닿으면
        {
            numGE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거
        }
        else if(coll.gameObject.tag == "NormalEgg") // NormalEgg가 닿으면
        {
            numNE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거
        }
    } // 계란이 trigger (collider+rigidbody(중력=0))
}
