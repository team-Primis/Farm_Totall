using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    public GameObject BuyChicken;
    public SpawnManager spawnManager;
    public GameManager GMScript;
    public GameObject Player;
    public float dis; // distance btw player & center

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {    
        if(Input.GetKey(KeyCode.Space)) // 스페이스바 눌렀을 때
        {   CallCenter();   } // 구매창 나타낼지 판단
        
        if(BuyChicken.activeSelf == true) // 구매창 켜져있는데, 멀어진다면
        {
            if(Vector2.Distance(Player.transform.position,transform.position)>2.0f)
            {
                BuyChicken.SetActive(false);
                GMScript.isTimerStoped = false; // 구매창 꺼지고 시간 정지 해제
            }
        }

        if(BuyChicken.activeSelf == true)
        {   GMScript.isBuyOpen = true;   }
        else
        {   GMScript.isBuyOpen = false;   }
    }

    void CallCenter()
    {
        dis = Vector2.Distance(Player.transform.position,transform.position);
        if(dis < 1.5f)
        {
            BuyChicken.SetActive(true); // player가 가까우면 구매창 나타남
            GMScript.isTimerStoped = true; // 구매창과 동시에 시간 정지
        }
    }

    // UI Buttons
    public void OnClickYesButton()
    {
        BuyChicken.SetActive(false);
        spawnManager.SpawnChicken(); // spawn chicken
        GMScript.isTimerStoped = false; // 구매창 꺼지고 시간 정지 해제
    }
    public void OnClickNoButton()
    {
        BuyChicken.SetActive(false); // nothing happens
        GMScript.isTimerStoped = false; // 구매창 꺼지고 시간 정지 해제
    }
}
