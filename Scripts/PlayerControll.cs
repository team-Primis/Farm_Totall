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
    private inventory inven;
    

    public string item; //현재 손에 든 아이템

    private CoinText coinTextScript;

    // from playercontoller
    public int numGE = 0; // 보유 중인 좋은 알의 개수
    public int numNE = 0; // 보유 중인 보통 알의 개수


    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        coinTextScript = GameObject.Find("haveMoney").GetComponent<CoinText>();
    }

    //플레이어의 돈의 수량을 바꾸고, 그에 맞게 UI를 업데이트
    public void playerMoneyChange(int newMoney, bool isAdded) {
        if (isAdded)
        {
            money += newMoney;
        }
        else
        {
            money -= newMoney;
        }
        coinTextScript.changeText();

    }

    // from playercontoller
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "GoodEgg") // GoodEgg가 닿으면
        {
            numGE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거

            //미해가 씀 : 인벤에 넣기 : id 11  좋은 달걀 id 12 평범 달걀
            inven.putInventory(11);
            
        }
        else if(coll.gameObject.tag == "NormalEgg") // NormalEgg가 닿으면
        {
            numNE += 1; // 보유 개수 하나 증가
            Destroy(coll.gameObject); // 해당 알 화면에서 제거

            inven.putInventory(12);
        }
    } // 계란이 trigger (collider+rigidbody(중력=0))
}
