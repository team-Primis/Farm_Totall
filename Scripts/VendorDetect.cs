using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorDetect : MonoBehaviour
{
    //이 클래스에서, 플레이어가 자판기에 부딪혔는지 알려줌
    public bool isVendTouched = false;
    public bool venderchanged = false;

    public bool isContainerTouched = false;
    public bool containerChanged = false;

    GameManager GMScript;

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    //자판기로 들어갈때 : 자판기 화면 띄움 (GameManager한테 vendtouch를 true로 해줌)
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("vending")) {
            Debug.Log("entered vending machine!");
            venderchanged = true;
            isVendTouched = true;
            GMScript.isTimerStoped = true;
            
        }

        if (coll.gameObject.CompareTag("container")) {
            Debug.Log("Entered container!");
            isContainerTouched = true;
            containerChanged = true;
            GMScript.isTimerStoped = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("vending"))
        {
            Debug.Log("exited vending machine!");
            venderchanged = true;
            isVendTouched = false;
            GMScript.isTimerStoped = false;
        }

        if (coll.gameObject.CompareTag("container"))
        {
            Debug.Log("exited container!");
            isContainerTouched = false;
            containerChanged = true;
            GMScript.isTimerStoped = false;
           
        }
    }

}


