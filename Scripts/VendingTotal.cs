﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendingTotal : MonoBehaviour
{
    private PlayerControll playerScript;
    private inventory inven;
    public Text seedNameText;
    public Text moneyText;
    public Text buyNumText;
    public TMP_Text leftMoneyText;

    public int seedMoney; //씨앗 구매시 필요한 돈 (구매창에 뜨는 용)
    public int buyNum; // 현재 구매할 씨앗 창 (구매창 표시)
    public string seedName; //현재 구매할 씨앗 이름
    public int totalMoney;

    public Button plusBtn;
    public Button minusBtn;
    public Button buyBtn;
    private CoinText coinTextScript;

    public GameObject rightSide; // 구매창이 있는 왼쪽부분

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        plusBtn.onClick.AddListener(AddBuyNum);
        minusBtn.onClick.AddListener(SubBuyNum);
        buyBtn.onClick.AddListener(buySeed);
        coinTextScript = GameObject.Find("haveMoney").GetComponent<CoinText>();

      
    }
    void AddBuyNum()
    {
        if (buyNum < 99)
        {
            buyNum++; //총 살 갯수
            totalMoney = buyNum * seedMoney; //필요한 돈 총액
                                             //UI반영
            moneyText.text = totalMoney.ToString() + "원";
            buyNumText.text = buyNum.ToString();
        }
    }

    void SubBuyNum()
    {
        if (buyNum > 1)
        {
            buyNum--; //총 살 갯수
            totalMoney = buyNum * seedMoney; //필요한 돈 총액
                                             //UI반영
            moneyText.text = totalMoney.ToString() + "원";
            buyNumText.text = buyNum.ToString();
        }
    }

    void buySeed()
    {
        //잔액이 충분히 있다면, 잔액 감소해서 물품 구매, 감소한 돈 반영
        if(playerScript.money >= totalMoney)
        {
            playerScript.playerMoneyChange(totalMoney, false);
            //인벤에 아이템 추가
            //Debug.Log(totalMoney + "원을 사용하여 " + seedName + " " + buyNum + "개를 구매합니다. 잔액 : " + playerScript.money);
            inven.putInventory(seedName, buyNum);
        }
        else
        {
            playerScript.playerMoneyChange(totalMoney, false);
    
        }
    }

   
}
