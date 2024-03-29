﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MouseSelect : MonoBehaviour
{
    SpriteRenderer sr;//마우스 선택창 색깔 바꾸기 위한 필드.
    public GameObject thePlayer;//플레이어 오브젝트.
    public GameObject vendingUI;//미해의 vending 유아이.
    public GameObject containerUI;//미해의 컨테이너 유아이.
    public GameManager GMscript;//게임매니져.
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GMscript=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (vendingUI.gameObject==false && containerUI.gameObject==false)
        //{
        if(GMscript.isTimerStoped==false)//게임매니져 타이머가 계속 작동할 때만 마우스 선택창이 보임.
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이어화면에서의 마우스 위치로 Vector2 타입 마우스 위치 설정.
            mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기만큼 이동하는 것처럼 보이기 위해 반올림하여 마우스 위치 재설정.
            transform.position = mousePosition;//현 객체(마우스 선택창)의 위치를 마우스 위치로 설정.

            if (Mathf.Abs(transform.localPosition.x) > 1.5f || Mathf.Abs(transform.localPosition.y) > 2f)//플레이어 현재 위치를 기준으로 일정 범위를 벗어나면 마우스 선택창이 빨갛게 변함.
            { sr.color = Color.red; }


            else//플레이어 현재 위치를 기준으로 일정 범위 내에서는 마우스 선택창이 파란색임.
            {

                sr.color = Color.blue;

            }

        }

 
    }
}