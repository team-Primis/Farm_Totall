using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private PlayerControll playerScript;
    
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        changeText();

    }


    public void changeText()
    {
        coinText.text = playerScript.money.ToString();
    }
}
