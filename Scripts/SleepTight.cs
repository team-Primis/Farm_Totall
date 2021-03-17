using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SleepTight : MonoBehaviour
{
    public Animator anim;
   [SerializeField] public GameObject sleepUI;
    public GameObject thePlayer;
    public GameManager GMscript;
    public GameObject sleepingLoadingUI;
    public GameObject canvass;
    public Stemina stM;
    public int wantedSleepTime;
    public float theGap;
    public float thisTime;
    public bool plantIsGrowing = false;
    public SpawningPlant sPP;
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
        theGap = 60 * 24 - GMscript.timer;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickYes()
    {
        GMscript.isTimerStoped = true;
        GMscript.isSleepOpen = true;
        sleepUI.SetActive(false);
        sleepingLoadingUI.SetActive(true);
        canvass.SetActive(false);
        
        
        thisTime = GMscript.timer;
        thisTime += wantedSleepTime * 60 * GMscript.speedUp;
        if(thisTime<60*24)
        {
            GMscript.timer = thisTime;
        }
        else if(thisTime>=60*24)
        {
            GMscript.timer = (thisTime - 60 * 24);
            GMscript.day += 1;

        }
        plantIsGrowing = true;
        Invoke("ClickNo", 3f);
        
        stM.curHp = stM.maxHp;
       thePlayer.transform.position=new Vector2(-7f, -3.6f);
      
    }

    public void ClickNo()
    {
        
        sleepUI.SetActive(false);
        sleepingLoadingUI.SetActive(false);
        GMscript.isTimerStoped = false;
        GMscript.isSleepOpen = false;
        canvass.SetActive(true);
        for(int i=0; i<sPP.createdPlant.Count; i++)
        {
            sPP.createdPlant[i].GetComponent<PlantLoad>().plantGrow = true;
        }
        plantIsGrowing = false;
    }
}
