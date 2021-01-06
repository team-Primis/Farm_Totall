using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class VendingText : MonoBehaviour
{
    //현재 보유 금액 바꾸는 애
    PlayerControll playerScript;
    public TMP_Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isMoneyChanged == true) {

            moneyText.text = playerScript.money.ToString();
            playerScript.isMoneyChanged = false;
        }
    }
}
