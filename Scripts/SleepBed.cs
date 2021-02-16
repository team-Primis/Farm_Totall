using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : MonoBehaviour
{
    [SerializeField] public GameObject sleepUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
            CloseMenu();
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
