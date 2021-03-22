using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stemina : MonoBehaviour
{

    public Slider hpbar;//HP바 가져옴.
    public SleepTight slT;//잠자기 캔버스 가져옴.
    public float maxHp= 200;//최대 체력.
    public float curHp = 0;//현재 체력. 왜 0으로 해놨지.....?

    public PlayerMove thePlayer;//플레이어무브 스크립트.
    public GameManager GMscript;//게임매니져.
    public GameObject sleepingLoadingUI;//"Sleeping..." 캔버스.
    public GameObject canvass;//미해가 만든 인게임 유아이.
    [SerializeField] public GameObject sleepUI;//"잠을 잘까요?" 캔버스.
    public bool zeroHp=false;//현재 HP가 0인지 아닌지 알려주는 bool. 얘가 트루가 되면 Painting 이벤트가 발생함.
    
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
        HandleHp();//현재 체력 보여줌.
       
    }

    private void HandleHp()//현재 체력 보여주는 함수.
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)curHp / (float)maxHp, Time.deltaTime*10);//정규화? Lerp 쓰면 HP바가 좀 스무스하게 변화함. 
    }

     public void UseHp(float usingRate)//체력 소모하는 함수.
    {
        if(curHp>0)//현재 체력이 0보다 크면
        {
            
            curHp -= usingRate;//지정해둔 값만큼 체력을 소모함.
        }
            
        if(curHp<=0)//0보다 작거나 같으면
        {
            
            curHp = 0f;//현재 체력값은 계속 0으로 해주고
            zeroHp = true;//이 bool은 트루로 만들어줌.
        }
    }

    public void FillHp(float fillingRate)//체력 회복하는 함수.
    {
        if(curHp>0)//현재 체력이 0보다 크면 
        {
            curHp += fillingRate;//지정해준 값만큼 체력 회복해줌.
        }
           
        if (curHp <= 0)//이건 회복이랑 상관없지 않나..? 근데 넣어놨네. 암튼 Painting 이벤트 발생시켜주려고 해둠.
        {
            curHp = 0f;
            zeroHp = true;
        }

        if(curHp>=maxHp)//현재 체력이 최대치면
        {
            curHp = maxHp;//그냥 계속 최대치로 유지해줌.
        }
    }

   

    
}
