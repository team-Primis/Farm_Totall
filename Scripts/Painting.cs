using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Painting : MonoBehaviour
{
    public Stemina stM;//스태미나 스크립트.
    public GameManager GMscript;//게임매니져.
    public GameObject canvasPaint;//"기절" 캔버스
    public Image imagePaint;//"기절"캔버스의 이미지 컴포넌트.
    public GameObject canvass;//미해의 인게임 유아이.
    public float thisTime;//현재 시간.
    public float wantedPaintTime=2f;//얼마나 기절시켜놓을 건지 정할 수 있는 수.
    public bool plantIsGrowing = false;//식물 타이머에 기절 시간 반영시켜줄 bool.
    public AudioSource audioSource;//미해가 만든 오디오 소스 가져옴.
    public AudioClip paintingSound;//기절하는 걸 알리는 소리.
    public bool setSoundOn = false;//기절 소리 조정하는 bool.
    // Start is called before the first frame update
    void Start()
    {
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        GMscript= GameObject.Find("GameManager").GetComponent<GameManager>();
        imagePaint = GameObject.Find("Canvasspainted").transform.Find("PainLoading").GetComponent<Image>();
        canvasPaint = GameObject.Find("Canvasspainted").transform.Find("PainLoading").gameObject;
        canvass = GameObject.Find("Canvas2").gameObject;//게임 내 유아이 
        this.audioSource = GameObject.Find("SoundEffect").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stM.zeroHp==true)//스태미나에서 현 체력이 0일 때 기절 이벤트 함수가 동작함.
        {
            
            hpIsZero();
        }
    }

    public void hpIsZero()//기절 이벤트 동작 함수.
    {
        stM.zeroHp = false;//기절하고 나면 스태미나가 0 초과이기 때문에 펄스로 만들어줌.
        GMscript.isTimerStoped = true;//기절 캔버스가 켜지면 게임매니져의 타이머도 멈춤.
        GMscript.isSleepOpen = true;//기절 캔버스가 켜진 걸 알리는 bool.
        audioSource.clip = paintingSound;//오디오 소스에 기절 소리를 적용시켜주고
        audioSource.Play();//재생시킴.
        canvasPaint.SetActive(true);//"기절" 캔버스를 켬.
        StartCoroutine(Flickering());//깜빡깜빡하는 효과가 있는 코루틴 함수를 시작함.
        canvass.SetActive(false);//기절 캔버스가 켜지면 미해의 인게임 유아이 캔버스를 끔.
        
        thisTime = GMscript.timer;//현재시간에 게임매니져의 타이머를 반영시켜줌.
        thisTime += wantedPaintTime * 60 * GMscript.speedUp;//기절한 시간만큼 더해줌.

        if (thisTime < 60 * 24)//현재시간이 24시 이하이면
        {
            GMscript.timer = thisTime;//게임매니져 타이머에 현재시간을 반영해줌.
        }
        else if (thisTime >= 60 * 24)//현재 시간이 24시를 넘으면
        {
            GMscript.timer = (thisTime - 60 * 24);//그 차이만큼을 게임매니져 타이머에 반영해줌.
            GMscript.day += 1;//24시간이 지났으니 하루 증가시켜줌.

        }
        
       
        plantIsGrowing = true;//기절한만큼 지난 시간을 식물 타이머에 반영해줌.
        Invoke("wokeUp", 3f);//깨어나는 함수를 삼초 후에 동작시켜줌.
        stM.curHp = stM.maxHp / 4;//최대체력의 사분의 일만큼 현재체력을 회복함.


    }

    public void wokeUp()//일어나는 함수.
    {
        
        GMscript.isTimerStoped = false;//게임매니져 타이머가 동작하게 해줌.
        GMscript.isSleepOpen = false;//기절 캔버스가 꺼졌음을 알림.
        canvass.SetActive(true);//미해의 유아이 캔버스를 켬.
        canvasPaint.SetActive(false);//"기절" 캔버스를 끔.
       
    }

    public IEnumerator Flickering()//검은색 화면이 깜빡이는 효과를 주는 함수.
    {
        for(int i=0; i<3; i++)//총 네 번 반복.
        {
            imagePaint.color = Color.black;//"기절" 캔버스의 색깔을 검은색으로 해주고
            yield return new WaitForSeconds(0.5f);//0.5초 후에 
            imagePaint.color = new Color(0, 0, 0, 0.5f);//"기절" 캔버스 색깔을 회색으로 바꿔줌.
            yield return new WaitForSeconds(0.5f);//0.5초 동안 지속.
        }

        
    }
 

}
