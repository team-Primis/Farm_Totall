using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SleepingLoad : MonoBehaviour
{
    public Slider progressbar;
    
    [SerializeField] public GameObject thePlayer;
    public GameObject sleepingLoadingUI;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").gameObject;
        sleepingLoadingUI = GameObject.Find("SleepLoading").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (progressbar.value < 1f)
        {
            progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
        }
        
        if(progressbar.value >1f)
        {
            sleepingLoadingUI.SetActive(false);
            thePlayer.transform.position = new Vector2(-5.3f, -4f);
        }


        }

   
}
