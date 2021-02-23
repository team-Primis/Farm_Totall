using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    [SerializeField] public GameObject sleepUI;
    public GameManager GMscript;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
                CallMenu();
                Debug.Log("닿아씀");
            GMscript.isTimerStoped = true;
            GMscript.isMenuOpen = true;
         
        }

        else
        {
            CloseMenu();
            GMscript.isTimerStoped = false;
            GMscript.isMenuOpen = false;
        }
            
    }
    private void CallMenu()
    {
        sleepUI.SetActive(true);
    }

    private void CloseMenu()
    {
        sleepUI.SetActive(false);
    }

}
