using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorDetect : MonoBehaviour
{
    //이 클래스에서, 플레이어가 자판기에 부딪혔는지 알려줌
   


    public GameObject vendingUI;
    public GameObject containerUI;
    public GameObject sellingUI;

    public bool isTriggeredVending = false;
    public bool isTriggerContainer = false;
    public bool isTriggerSelling = false;
    GameManager GMScript;



    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggeredVending)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vendingUI.gameObject.SetActive(!vendingUI.gameObject.activeSelf); //여기에 UI 뜨는거 있음 (자판기)
            }
        }
        if (isTriggerContainer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                containerUI.gameObject.SetActive(!containerUI.gameObject.activeSelf);//여기에 UI 뜨는거 있음 (보관상자)
            }
        }
        if (isTriggerSelling)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sellingUI.gameObject.SetActive(!sellingUI.gameObject.activeSelf);
            }
        }
       
    }

    //자판기로 들어갈때 : 자판기 화면 띄움 (GameManager한테 vendtouch를 true로 해줌)
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("vending")) {
             
            isTriggeredVending = true;

            GMScript.isTimerStoped = true;
            
        }

        if (coll.gameObject.CompareTag("container")) {
            isTriggerContainer = true;
            GMScript.isTimerStoped = true;
            
        }

        if (coll.gameObject.CompareTag("sellContainer"))
        {
            isTriggerSelling = true;
            GMScript.isTimerStoped = true;

        }
        


    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("vending"))
        {
            isTriggeredVending = false;
            vendingUI.gameObject.SetActive(false);
            GMScript.isTimerStoped = false;
        }

        if (coll.gameObject.CompareTag("container"))
        {
            isTriggerContainer = false;
            containerUI.gameObject.SetActive(false);
            GMScript.isTimerStoped = false;
           
        }
        if (coll.gameObject.CompareTag("sellContainer"))
        {
            isTriggerSelling = false;
            sellingUI.gameObject.SetActive(false);
            GMScript.isTimerStoped = false;

        }


    }

    

   

}


