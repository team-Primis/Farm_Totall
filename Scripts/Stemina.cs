using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stemina : MonoBehaviour
{

    public Slider hpbar;
    public SleepTight slT;
    public float maxHp= 200;
    public float curHp = 0;

    public PlayerMove thePlayer;
    public GameManager GMscript;
    public GameObject sleepingLoadingUI;
    public GameObject canvass;
    [SerializeField] public GameObject sleepUI;
    public bool zeroHp=false;
    // Start is called before the first frame update
    void Start()
    {
        hpbar = GetComponent<Slider>();
        hpbar.value = (float)curHp / (float)maxHp;
        slT = GameObject.Find("Canvassleep").transform.Find("SleepTight").GetComponent<SleepTight>();
        curHp = 200f;
        thePlayer = FindObjectOfType<PlayerMove>();
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        sleepUI = GameObject.Find("Canvassleep").transform.Find("SleepTight").transform.Find("SleepUI").gameObject;
        sleepingLoadingUI = GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject;
        canvass = GameObject.Find("Canvas2").gameObject;//게임 내 유아이 
    }

    // Update is called once per frame
    void Update()
    {
        HandleHp();
        if(curHp<=0)
        {
            
           if(zeroHp==true)
            {
                Invoke("hpIsZero", 2f);
                zeroHp = false;
            }
            
           
        }
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)curHp / (float)maxHp, Time.deltaTime*10);
    }

     public void UseHp(float usingRate)
    {
        if(curHp>0)
        {
            
            curHp -= usingRate;
        }
            
        if(curHp<=0)
        {
            
            curHp = 0f;
            zeroHp = true;
        }
    }

    public void FillHp(float fillingRate)
    {
        if(curHp>0)
        {
            curHp += fillingRate;
        }
           
        if (curHp <= 0)
        {
            curHp = 0f;
            zeroHp = true;
        }

        if(curHp>=maxHp)
        {
            curHp = maxHp;
        }
    }

    public void hpIsZero()
    {
        GMscript.isTimerStoped = true;
        GMscript.isSleepOpen = true;
        sleepUI.SetActive(false);
        sleepingLoadingUI.SetActive(true);
        canvass.SetActive(false);

        Invoke("wokeUp", 3f);
        GMscript.timer += 7 * 60 * GMscript.speedUp;
        curHp = maxHp;
        thePlayer.transform.position = new Vector2(23.24f, 7f);
    }

    public void wokeUp()
    {
        sleepUI.SetActive(false);
        sleepingLoadingUI.SetActive(false);
        GMscript.isTimerStoped = false;
        GMscript.isSleepOpen = false;
        canvass.SetActive(true);
    }
}
