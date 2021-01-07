using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    VendorDetect vendorDetectScript;
    public GameObject vendingImage;
    public bool isStaminaChanged = false;
    public int stamina = 10;

    public int day = 1;
    public int minute;
    public int  seconds;

    public TMP_Text TimeText;
    public TMP_Text DayText;

    public float timer = 0;
    public bool isTimerStoped = false;

    public bool isInvenOpen;
    public bool isInvenStateChanged;

    public GameObject[] staminaObj;

    public GameObject container;
    public GameObject inventory;

    // from playercontoller
    public GameObject BuyChicken; // for moving control
    public bool isBuyOpen;

    // Start is called before the first frame update
    void Start()
    {
        vendorDetectScript = GameObject.Find("Player").GetComponent<VendorDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        checkVendorDetect(); //자판기 들어갔다 나옴
        checkContainerDetect();
        InvenDetect();
        changeStaminaUI(); //스태미나 감소

        // from playercontoller
        BuyUIDetect();

        //24시간 지나면 하루 지남 + UI 켰을때는 시간 안감
        if (!isTimerStoped)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 60*24) //1시간 * 24 = 1일
        {
            timer = 0;
            day++;
            DayUI(day);
        }
        DateUI(timer); //시간 표시

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

    void InvenDetect() {
        if (isInvenStateChanged)
        {
            if (isInvenOpen) {
                inventory.SetActive(true);
                isTimerStoped = true;
            }
            else
            {
                inventory.SetActive(false);
                isTimerStoped = false;
            }
            isInvenStateChanged = false;
        }
    }
    //자판기에 가까이 가면 자판기 UI 띄워줌
    void checkVendorDetect() {
        if (vendorDetectScript.venderchanged == true)
        {
            if (vendorDetectScript.isVendTouched == true)
            {
                vendingImage.SetActive(true);
            }
            else
            {
                vendingImage.SetActive(false);

            }

            vendorDetectScript.venderchanged = false;

        }
    }

    void checkContainerDetect() {
        if (vendorDetectScript.containerChanged == true) {

            if(vendorDetectScript.isContainerTouched == true)
            {
                isTimerStoped = true;
                container.SetActive(true);
            }
            else
            {
                container.SetActive(false);
                isTimerStoped = false;
            }
            vendorDetectScript.containerChanged = false;
        }
    }

    // from playercontoller
    void BuyUIDetect()
    {
        if(BuyChicken.activeSelf == true){  isBuyOpen = true;  }
        else{  isBuyOpen = false;  }
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


