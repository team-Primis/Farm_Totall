﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControll : MonoBehaviour
{
    //플레이어관련 : 움직임, 보유 돈, 보유 씨앗, 달걀 관련

    public int money = 2000;
    public bool isMoneyChanged = false;

    public int laborCount = 5;
    GameManager GMScript;
    private inventory inven;
    

    public string item; //현재 손에 든 아이템

    private CoinText coinTextScript;

    /*// from playercontoller - (성현, 0219 제거)
    public int numGE = 0; // 보유 중인 좋은 알의 개수
    public int numNE = 0; // 보유 중인 보통 알의 개수*/

    // (성현, 0219 추가)
    public SpawnManager SMScript;

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        coinTextScript = GameObject.Find("haveMoney").GetComponent<CoinText>();

        // (성현, 0219 추가)
        SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    //플레이어의 돈의 수량을 바꾸고, 그에 맞게 UI를 업데이트
    public void playerMoneyChange(int newMoney, bool isAdded) {
        //playerMoneyChange(돈 액수, true) : 돈 추가
        if (isAdded)
        {
            money += newMoney;
        }
        //playerMoneyChange(돈 액수, false) : 돈 줄어듦
        else
        {
            if (money >= newMoney)
            {
                money -= newMoney;
            }
            else
            {
                Debug.Log("잔액이 부족합니다.");
            }
        }
        coinTextScript.changeText();

    }

    // from playercontoller
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "GoodEgg") // GoodEgg가 닿으면
        {
            //numGE += 1; // 보유 개수 하나 증가 (성현, 0219 제거)
            
            // (성현, 0219 추가) 리스트에서도 제거
            int num = SMScript.gEggList.Count;
            for (int i = 0; i < SMScript.gEggList.Count; i++)
            {
                if(SMScript.gEggList[i] == coll.gameObject)
                num = i;
            }
            if(num < SMScript.gEggList.Count) // index 찾았으면
            {
                SMScript.gEggList.RemoveAt(num);
            }
            else
            {
                Debug.Log("GoodEgg Error");
            }

            Destroy(coll.gameObject); // 해당 알 화면에서 제거

            //미해가 씀 : 인벤에 넣기 : id 11  좋은 달걀 id 12 평범 달걀
            inven.putInventory(11);
            
        }
        else if(coll.gameObject.tag == "NormalEgg") // NormalEgg가 닿으면
        {
            //numNE += 1; // 보유 개수 하나 증가 (성현, 0219 제거)

            // (성현, 0219 추가) 리스트에서도 제거
            int num = SMScript.nEggList.Count;
            for (int i = 0; i < SMScript.nEggList.Count; i++)
            {
                if(SMScript.nEggList[i] == coll.gameObject)
                num = i;
            }
            if(num < SMScript.nEggList.Count) // index 찾았으면
            {
                SMScript.nEggList.RemoveAt(num);
            }
            else
            {
                Debug.Log("NormalEgg Error");
            }

            Destroy(coll.gameObject); // 해당 알 화면에서 제거

            inven.putInventory(12);
        }
    } // 계란이 trigger (collider+rigidbody(중력=0))
}
