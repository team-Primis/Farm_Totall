using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // (성현) scene 전환 - 초기 타이틀로의 이동 때문에

public class GameManager : MonoBehaviour
{
    public int speedUp=50; //이걸로 타이머 속도조절
    public bool isStaminaChanged = false;
    public int stamina = 10;

    public int day = 1;
    public int minute;
    public int  seconds;

    private TMP_Text TimeText;
    private TMP_Text DayText;

    public float timer = 420;
    public bool isTimerStoped = false;//true일 때 멈춤 flase는 작동하는중.
    public bool isSleepOpen = false;

    public GameObject[] staminaObj;

    // from playercontoller (center)
    public GameObject BuyChicken; // 닭구매창
    public bool isBuyOpen = false; // 닭구매창이 켜져있는가

    // from menucontrol
    public bool isMenuOpen = false; // 일시정지 메뉴 관련

    // (성현) 게임을 시작하고, didGameStart가 false이면 OutSide -> Title (맨 처음 시작 화면)
    // 즉 게임 자체의 처음 화면은 OutSide이지만, 플레이 전 Title로 바로 옮겨 주기
    public bool didGameStart = false; // 초기값이 false (게임 켰을 당시에만 false이고 그 후는 true)

    public static GameManager instance = null;

    void Awake()
    {
        //싱글턴패턴. 게임매니저가 한번만 생성되게 하기 위함
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        TimeText = GameObject.Find("HourText").GetComponent<TMP_Text>();
        DayText = GameObject.Find("DayText").GetComponent<TMP_Text>();


        // 성현
        BuyChicken = GameObject.Find("Canvas2").transform.Find("BuyChicken").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // 성현
        if (!didGameStart) // 게임 시작 전에는 타이틀로 이동
        {
            didGameStart = true;
            SceneManager.LoadScene("Title");
        }

        changeStaminaUI(); //스태미나 감소

        DayUI(day); // 로드 때문에 여기로 옮겼음 (성현)

        //24시간 지나면 하루 지남 + UI 켰을때는 시간 안감
        if (!isTimerStoped)
        {
            //timer += Time.deltaTime;
            timer += Time.deltaTime* speedUp; // 로드 등 확인 위해 임시로 시간 배속함 (성현)
        }
        if (timer >= 60*24) //1시간 * 24 = 1일
        {
            timer = 0;
            day++;
            // DayUI(day); // 위로 옮겼음 (성현)
        }
        DateUI(timer); //시간 표시

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("날짜 : " + getDay());
        }
    }

    //UI에서 스태미나 감소 : 관련 오브젝트를 일단 unable하게 만들고, 현재 스태미나 만큼 다시 setactive
    void changeStaminaUI() {
        if (isStaminaChanged)
        {

            for (int i = 0; i < staminaObj.Length; i++)
            {
                staminaObj[i].SetActive(false);
            }

            if (stamina >= 1)
            {
                staminaObj[0].SetActive(true);
            }
            if (stamina >= 2)
            {
                staminaObj[1].SetActive(true);
            }
            if (stamina >= 3)
            {
                staminaObj[2].SetActive(true);
            }
            if (stamina >= 4)
            {
                staminaObj[3].SetActive(true);
            }
            if (stamina >= 5)
            {
                staminaObj[4].SetActive(true);
            }
            if (stamina >= 6)
            {
                staminaObj[5].SetActive(true);
            }
            if (stamina >= 7)
            {
                staminaObj[6].SetActive(true);
            }
            if (stamina >= 8)
            {
                staminaObj[7].SetActive(true);
            }
            if (stamina >= 9)
            {
                staminaObj[8].SetActive(true);
            }
            if (stamina >= 10)
            {
                staminaObj[9].SetActive(true);
            }
            Debug.Log("your stamina is " + stamina);
            isStaminaChanged = false;
        }
    }
    
    //매개변수에 넣은 만큼 스태미나 줄여줌
    public void useStamina(int stam)
    {
        stamina -= stam;
        isStaminaChanged = true;
    }
    
    public int getDay()
    {
        return day;
    }

    //UI 시간 관련
    // 실제 1분당 게임 1시간 - 60초 -> 60분
    //실제 1초당 게임 1분
    void DateUI(float timeToDisplay) {
         minute = Mathf.FloorToInt(timeToDisplay/60);
         seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimeText.text = string.Format("{0:00}:{1:00}",minute, seconds);
        //{0:00}:{1:00} 이거에서 앞쪽 0은 minute, 뒤쪽 1은 seconds인듯
    }

    void DayUI(int dayDisplay) {
        DayText.text = "DAY " + dayDisplay;
    }

    
}
