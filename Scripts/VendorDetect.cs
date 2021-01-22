using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorDetect : MonoBehaviour
{
    //이 클래스에서, 플레이어가 자판기에 부딪혔는지 알려줌
   


    public GameObject vendingUI;
    public GameObject containerUI;

    public bool isTriggeredVending = false;
    public bool isTriggerContainer = false;
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
                Debug.Log("spacebar touched");
                vendingUI.gameObject.SetActive(!vendingUI.gameObject.activeSelf);
            }
        }
        if (isTriggerContainer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("spacebar touched");
                containerUI.gameObject.SetActive(!containerUI.gameObject.activeSelf);
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

        
    }

    

   

}


