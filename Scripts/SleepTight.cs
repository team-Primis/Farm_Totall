using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SleepTight : MonoBehaviour
{
    public GameManager GMscript;//게임매니져 가져옴.
    public SpawningPlant sPP;//스포닝플랜트 스크립트 가져옴.
    public AudioSource audioSource;//미해가 만들어둔 오디오 소스 가져옴.
    public AudioClip sleepingSound;//잠잘 때 틀 소리 가져옴.
   [SerializeField] public GameObject sleepUI;//"잠을 잘까요?" 캔버스 가져옴.
    public GameObject thePlayer;//플레이어 오브젝트 가져옴.
    public GameObject sleepingLoadingUI;//"Sleeping..." 캔버스 가져옴.
    public GameObject canvass;//미해가 만든 인게임 유아이 가져옴.
    public Stemina stM;//스태미나 스크립트 가져옴.
    public int wantedSleepTime;//잠자는 시간 수정할 수 있게 퍼블릭으로 수정.
    public float thisTime;//현재 시간 선언.
    public bool plantIsGrowing = false;//식물이 자라는 순간을 알려주는 bool.
    public bool setSoundOn = false; //소리 트는 데 쓰는 bool.
    // Start is called before the first frame update
    void Start()
    {
        //sleepUI = GameObject.Find("SleepUI").gameObject; sleepUI가 처음에 setActive false라서 참조 못해서 null 에러뜸! 그래서 아래 방법 추천(미해)
        sleepUI = gameObject.transform.Find("SleepUI").gameObject;
        thePlayer = GameObject.Find("Player").gameObject;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        sleepingLoadingUI = GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject;
        canvass = GameObject.Find("Canvas2").gameObject;//게임 내 유아이 
        sPP = GameObject.Find("SpawningPlant").GetComponent<SpawningPlant>();
        this.audioSource = GameObject.Find("SoundEffect").GetComponent<AudioSource>();
       


    }

    // Update is called once per frame
    void Update()
    {
        SleepingSFX();
    }

    //"잠을 잘까요?" 캔버스에서 "예"를 누르면 "Sleeping..." 캔버스를 띄워주고 끄는 함수.
    public void ClickYes()
    {
        GMscript.isTimerStoped = true;//게임매니져의 타이머를 멈춤.
        GMscript.isSleepOpen = true;//잠자기 관련 캔버스가 켜졌음을 알리는 bool을 트루로 만듦.
        sleepUI.SetActive(false);//"잠을 잘까요?" 캔버스를 끔.
        sleepingLoadingUI.SetActive(true);//"Sleeping..." 캔버스를 켬.
        canvass.SetActive(false);//미해가 만든 인게임 유아이를 끔.
        setSoundOn = true;//잠잘 때 재생되는 소리를 켬.
        thisTime = GMscript.timer;//현재 시간을 게임매니져의 타이머로 지정해줌.
        thisTime += wantedSleepTime * 60 * GMscript.speedUp;//그리고 현재 시간에 잠잔 시간을 더해줌.
        if(thisTime<60*24)//자고 난 시간이 24시 이전이면
        {
            GMscript.timer = thisTime;//게임매니져의 타이머를 현재 시간에 잠잔 시간을 더해준 시간을 대입해줌.
        }
        else if(thisTime>=60*24)//만약에 자고 난 시간이 24시 이후이면
        {
            GMscript.timer = (thisTime - 60 * 24);//자고 난 시간에서 24시간을 빼주고 게임매니져의 타이머에 지정해줌.
            GMscript.day += 1;//24시가 지났으니 하루가 지난  걸 반영해줌.

        }
        plantIsGrowing = true;//잠잔 시간을 식물 성장 타이머에 반영하기 위한 bool을 트루로 해줌.
        Invoke("ClickNo", 3f);//"Sleeping..." 캔버스를 끄기 위한 함수를 삼초 후에 작동해줌.
        
        stM.curHp = stM.maxHp;//잠을 잤으니 Hp를 회복해야죠.
       thePlayer.transform.position=new Vector2(-7f, -3.6f);//침대 옆으로 플레이어의 위치를 옮겨줌.

    }

   //"Sleeping..." 캔버스를 끄는 함수.
    public void ClickNo()
    {
        
        sleepUI.SetActive(false);//"잠을 잘까요?" 캔버스를 꺼줌.
        sleepingLoadingUI.SetActive(false);//"Sleeping...." 캔버스를 꺼줌.
        GMscript.isTimerStoped = false;//게임매니져 타이머를 다시 작동시켜줌.
        GMscript.isSleepOpen = false;//잠자기 관련 캔버스가 꺼짐을 알려줌.
        canvass.SetActive(true);//미해가 만든 인게임 유아이를 켜줌.
        for(int i=0; i<sPP.createdPlant.Count; i++)//스포닝플랜트 스크립트에서 생성한 식물 리스트에 등록된 식물들의 타이머 변경을 알려주는 bool을 전부 트루로 만들어줌.
        {
            sPP.createdPlant[i].GetComponent<PlantLoad>().plantGrow = true;
        }
        plantIsGrowing = false;//식물 타이머에 시간 변화를 반영하는 bool을 다시 펄스로 만들어줌.
        setSoundOn = false;//잠자는 소리르 꺼줌.
    }

    //"Sleeping,," 캔버스가 켜져 있는 동안 잠자기 음악 켜는 함수.
    public void SleepingSFX()
    {
        audioSource.clip = sleepingSound;//잠자는 소리 할당해줌.
        audioSource.volume = 0.7f;//소리 변경.
        if (setSoundOn == true)//만약 이 bool이 트루인데
        {
            if (!audioSource.isPlaying)//미해가 만든 오디오 소스에서 음악이 재생되고 있지 않다면

                audioSource.Play();//잠자는 소리를 재생시켜줌.

        }
        else//그 외의 경우에는
        {
            audioSource.Stop();//잠자는 소리를 끔.
        }
    }
}
