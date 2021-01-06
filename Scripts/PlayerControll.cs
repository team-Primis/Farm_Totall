using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControll : MonoBehaviour
{
    //플레이어관련 : 움직임, 보유 돈,보유 씨앗

    private float speed = 1.5f;
    public int money = 2000;
    public bool isMoneyChanged = false;

    public int laborCount = 5;
    GameManager GMScript;

    public string item; //현재 손에 든 아이템

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 움직임 관련
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (GMScript.isInvenOpen)
            {
                GMScript.isInvenOpen = false;
                GMScript.isInvenStateChanged = true;
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (GMScript.isInvenOpen)
            {
                GMScript.isInvenOpen = false;
                GMScript.isInvenStateChanged = true;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (GMScript.isInvenOpen)
            {
                GMScript.isInvenOpen = false;
                GMScript.isInvenStateChanged = true;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (GMScript.isInvenOpen)
            {
                GMScript.isInvenOpen = false;
                GMScript.isInvenStateChanged = true;
            }

        }

        if (Input.GetKey(KeyCode.I))
        {
            if (!GMScript.isTimerStoped)
            {
                GMScript.isInvenOpen = true;
                GMScript.isInvenStateChanged = true;
            }
        }

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

        
    }
}
