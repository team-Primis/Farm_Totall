using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    public GameObject BuyChicken;
    public SpawnManager spawnManager;
    public GameObject Player;
    public float dis; // distance btw player & center

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {    
        if(Input.GetKey(KeyCode.Space)) // 스페이스바 눌렀을 때
        {   CallCenter();   } // 구매창 나타낼지 판단
    }

    void CallCenter()
    {
        dis = Vector2.Distance(Player.transform.position,transform.position);
        if(dis < 1.2f)
        {    BuyChicken.SetActive(true);    } // player가 가까우면 구매창 나타남
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
