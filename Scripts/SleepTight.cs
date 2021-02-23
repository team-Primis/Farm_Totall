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
    // Start is called before the first frame update
    void Start()
    {
        sleepUI = GameObject.Find("SleepUI").gameObject;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
    public void ClickYes()
    {
        
        sleepUI.SetActive(false);
        sleepingLoadingUI.SetActive(true);

        if (progressbar.value < 1f)
        {
            progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
        }

        if (progressbar.value > 1f)
        {
            sleepingLoadingUI.SetActive(false);
            thePlayer.transform.position = new Vector2(-5.3f, -4f);
        }
    }

    public void ClickNo()
    {
        sleepUI.SetActive(false);
    }
}
