using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLoad : MonoBehaviour
{

    public float plantTimer;//식물타이머.
    public Animator anim;//식물 애니메이션.
    public int i = 1;//식물 애니메이션 변화에 반영해줄 int.
    public int didItBloomed;//식물이 다 자랐는지 확인.
    public int toMuchWilted;//식물이 시드는지 확인.
    public Transform thePlayer;//플레이어 위치 가져옴.
    public bool iswatered=false;//물을 줬는지 안 줬는지 확인할 bool.
    public inventory Inven;//인벤토리.
    public GameManager GMscript;//게임매니져.
    public int wantedGrowthValue;//식물이 다음 단계로 넘어갈 때까지 자라야 할 시간.
    public SpriteRenderer wsr;//물 스프라이트 렌더러.
    public float thisTime;//현재시간.
    public SleepTight sT;//잠잘 때 시간 반영해주려고 스크립트 가져옴.
    public Painting pT;//기절할 때 시간 반영해주려고 스크립트 가져옴.
    public bool plantGrow=true;//잠/기절할 때 시간 반영해주기 위한 bool.
    public float plantPauseTimer;//plantTimer로 기절/잠 시간 반영하니까 도중에 Timer.deltaTime으로 시간 반영이 애매해지는 것 같아서 지정해줌.
    public float PauseGmTimer;//얘도 위와 같은 이유로
    //일시정지 창 등등이 켜져 있지 않을 때만 돌아가도록 설정
    // Start is called before the first frame update
    public void Awake()
    {


        anim = GetComponent<Animator>();
        thePlayer = GameObject.Find("Player").GetComponent<Transform>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        plantTimer = GMscript.timer;
        thisTime = GMscript.timer;
        wsr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        sT = GameObject.Find("Canvassleep").transform.Find("SleepTight").GetComponent<SleepTight>();
        pT = GameObject.Find("Canvasspainted").GetComponent<Painting>();
       

    }

    // Update is called once per frame
    void Update()
    {

        //일시정지 창 등등이 켜져 있지 않을 때만 돌아가도록 설정
        if(GMscript.isMenuOpen == false && GMscript.isWillSellOpen == false && GMscript.isSleepOpen == false || GMscript.isBuyOpen)
        {
            if (plantTimer <thisTime + wantedGrowthValue * 60)
                plantTimer += Time.deltaTime * GMscript.speedUp;//식물타이머는 기본적으로 게임매니져의 타이머와 같은 속도로 증가함.

            if(pT.plantIsGrowing==true && plantGrow==true)//만약 기절한 경우
            {
                plantTimer += pT.wantedPaintTime * 60*GMscript.speedUp;//기절 시간을 반영해줌.
                plantGrow = false;//얘를 펄스로 해줘야 이후에 또 기절할 때 시간이 반영됨.
            }
          if(sT.plantIsGrowing==true && plantGrow==true)//만약 잔 경우
            {
                plantTimer += sT.wantedSleepTime * 60*GMscript.speedUp;//잔 시간을 반영해줌.
                plantGrow = false;//얘를 펄스로 해줘야 이후에 또 잘 때 시간이 반영됨.
            }

            if (plantTimer >= thisTime + wantedGrowthValue * 60)//만약 식물 타이머가 현재시간에서 식물 성장 시간을 더한 것보다 커지면 성장시켜줘야지.
            {
                plantPauseTimer = plantTimer;
                PauseGmTimer = GMscript.timer;
                plantPauseTimer = PauseGmTimer + (plantPauseTimer - thisTime - wantedGrowthValue * 60);//식물 타이머에 그 차이를 반영해줌.
                plantTimer = plantPauseTimer;//식물 타이머에 그 차이를 반영해줌.

                thisTime = GMscript.timer;//현재시간을 게임매니져의 현재시간으로 다시 선언해줌.
                wsr.enabled = false;//물 오브젝트는 다시 끔.

                if (i < didItBloomed)//이 모든 건 식물이 완전히 다 자라기 전에만 일어난다.
                {
                    if (iswatered == true)//만약 물을 준 상태면
                    {
                        i++;//하나 키워서
                        anim.SetInteger("One", i);//식물 애니메이션 변화를 시켜줌.
                        iswatered = false;//물 줬음을 알려주는 bool을 초기화함.
                    }
                }


                else if (i >= didItBloomed && i < toMuchWilted)//만약 식물이 다 자랐는데 아직 시들기 전이면 
                {

                    iswatered = false;//다 자란 후에는 물 안 줘도 됨.
                    i++;//시들 때까지 성장은 해야지.
                    anim.SetInteger("One", i);
                }

                else if (i >= toMuchWilted)//식물이 시들면
                {
                    Destroy(this.gameObject);//없어짐.
                }
            }



        }


    }

    // for SaveNLoad (0304 성현)
    private int goalNum = 0;
    private int nowNum = 0;
    private bool addWater = false;
    public void DoPlantAni(int goall, bool waterr)
    {
        addWater = waterr;
        if(goall > 0) // 저장 전 한 단계라도 진행됐을 때
        {
            goalNum = goall;
            nowNum = 1; // nowNum 초기값은 1
            DoAgain();
        }
        else if(addWater) // 초기 그대로 but 물만 줌
        {
            Invoke("BringWater", 0.2f);
        }
    }
    void DoAgain()
    {
        anim.SetInteger("One", nowNum);
        if(goalNum > nowNum)
        {
            nowNum += 1;
            Invoke("DoAgain", 0.2f);
        }
        else if(addWater) // 상태 반영 OK → 물 반영
        {
            Invoke("BringWater", 0.2f);
        }
    }
    void BringWater()
    {
        wsr.enabled = true;
    }


}
