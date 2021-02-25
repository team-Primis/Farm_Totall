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
    public Slider progressbar;
    public GameObject sleepingLoadingUI;
    public GameObject canvass;
    // Start is called before the first frame update
    void Start()
    {
        //sleepUI = GameObject.Find("SleepUI").gameObject; sleepUI가 처음에 setActive false라서 참조 못해서 null 에러뜸! 그래서 아래 방법 추천(미해)
        sleepUI = gameObject.transform.Find("SleepUI").gameObject;
        thePlayer = GameObject.Find("Player").gameObject;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        progressbar = GameObject.Find("Canvassleep").transform.Find("SleepLoading").transform.Find("Slider").GetComponent<Slider>();
        sleepingLoadingUI = GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject;
        canvass = GameObject.Find("Canvas2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickYes()
    {
        
        sleepUI.SetActive(false);
        sleepingLoadingUI.SetActive(true);
        canvass.SetActive(false);

        if (progressbar.value < 1f)
        {
            progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            
            GMscript.isMenuOpen = true;
        }

        if (progressbar.value > 1f)
        {
            sleepingLoadingUI.SetActive(false);
            thePlayer.transform.position = new Vector2(-5.3f, -4f);
            
            GMscript.isMenuOpen = false;
        }
    }

    public void ClickNo()
    {
        
        sleepUI.SetActive(false);
        GMscript.isTimerStoped = false;
        GMscript.isMenuOpen = false;
        canvass.SetActive(true);
    }
}
