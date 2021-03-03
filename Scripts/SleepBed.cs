using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepBed : MonoBehaviour
{
    [SerializeField] public GameObject sleepUI;
    public GameManager GMscript;
    public GameObject canvass;

    // Start is called before the first frame update
    void Start()
    {
        sleepUI = GameObject.Find("Canvassleep").transform.Find("SleepTight").transform.Find("SleepUI").gameObject;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvass = GameObject.Find("Canvas2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
                CallMenu();
                
        }

        else
        {
            CloseMenu();
            
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CloseMenu();
        }
    }

    private void CallMenu()
    {
        sleepUI.SetActive(true);
        GMscript.isTimerStoped = true;
        GMscript.isSleepOpen = true;


    }

    private void CloseMenu()
    {
        sleepUI.SetActive(false);
        GMscript.isTimerStoped = false;
       GMscript.isSleepOpen = false;

    }

}
