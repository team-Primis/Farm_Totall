using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // text 편집

public class Center : MonoBehaviour
{
    public GameObject BuyChicken;
    //public SpawnManager SMScript;
    public GameManager GMScript;
    public GameObject Player;
    public float dis; // distance btw player & center

    //private PlayerControll thePlayerCtr; // for money
    //public Text howRich; // 없앰

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas2 = GameObject.Find("Canvas2");
        BuyChicken = canvas2.transform.Find("BuyChicken").gameObject;
        //howRich = canvas2.transform.Find("BuyChicken").transform.Find("HowRich").GetComponent<Text>();
        
        //SMScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        //thePlayerCtr = GameObject.Find("Player").GetComponent<PlayerControll>();
        // Find -> gameobject 찾기
        Player = GameObject.Find("Player");
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
            //howRich.GetComponent<Text>().text = "(보유 금액 : " + thePlayerCtr.money + "원)";
            BuyChicken.SetActive(true); // player가 가까우면 구매창 나타남
            GMScript.isTimerStoped = true; // 구매창과 동시에 시간 정지
        }
    }

    // UI Buttons - menucontrol로 옮김
}
