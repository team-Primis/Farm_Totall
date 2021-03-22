using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepBed : MonoBehaviour
{
    [SerializeField] public GameObject sleepUI; //잠자는용 캔버스의 sleepUI 게임오브젝트 가져옴.
    public GameManager GMscript;//게임매니져 가져옴.
    public GameObject canvass;//미해가 만든 인게임 캔버스 가져옴.
    public PlayerMove thePlayer;//플레이어 움직이게 해주는 스크립트 가져옴.
    // Start is called before the first frame update
    void Start()
    {
        sleepUI = GameObject.Find("Canvassleep").transform.Find("SleepTight").transform.Find("SleepUI").gameObject;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvass = GameObject.Find("Canvas2").gameObject;
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 //충돌이 일어나면 "잠을 잘까요?" 물어보는 캔버스가 뜨는 함수
    private void OnTriggerEnter2D(Collider2D collision)//충돌이 일어날 때
    {
        if (collision.gameObject.CompareTag("Player"))//충돌한 오브젝트의 태그가 "플레이어"인 경우
        {
            
                CallMenu();//"잠을 잘까요?" 캔버스를 뜨게 한다.
                
        }

        else//만약 충돌한 오브젝트의 태그가 "플레이어"가 아닌 경우
        {
            CloseMenu();//"잠을 잘까요?"란 캔버스는 뜨지 않는다.
            
        }
            
    }

    //충돌한 오브젝트가 빠져나가는 경우 "잠을 잘까요?" 캔버스를 닫는 함수
    private void OnTriggerExit2D(Collider2D collision)//충돌한 오브젝트가 빠져나가는 경우 
    {
        if(collision.gameObject.CompareTag("Player"))//충돌한 오브젝트의 태그가 "플레이어'인 경우
        {

            CloseMenu();//"잠을 잘까요?" 캔버스를 닫는다.
        }
    }

    //"잠을 잘까요?" 캔버스를 여는 함수
    private void CallMenu()
    {
        sleepUI.SetActive(true);//"잠을 잘까요?" 캔버스를 activate함.
        GMscript.isTimerStoped = true;//게임매니져의 시간을 멈춤.
        GMscript.isSleepOpen = true;//게임매니져의 잠자는 메뉴 여는 bool을 트루로 만듦.


    }

    //"잠을 잘까요?" 캔버스를 닫는 함수
    private void CloseMenu()
    {
        sleepUI.SetActive(false);//"잠을 잘까요?" 캔버스를 끔.
        GMscript.isTimerStoped = false;//"게임매니져의 시간을 멈춤.
       GMscript.isSleepOpen = false;//게임매니져의 잠자는 메뉴 여는 bool을 펄스로 만듦.

    }

}
