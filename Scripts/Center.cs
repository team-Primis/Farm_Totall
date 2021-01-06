using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    public GameObject BuyChicken;
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "Player") // Player가 닿으면
        {
            BuyChicken.SetActive(true); // 구매창 나타남
        }
    } // 전화기를 trigger로 설정 (rigidbody + collider)

    // UI Buttons
    public void OnClickYesButton()
    {
        BuyChicken.SetActive(false);
        spawnManager.SpawnChicken(); // spawn chicken
    }
    public void OnClickNoButton()
    {
        BuyChicken.SetActive(false); // nothing happens
    }
}
