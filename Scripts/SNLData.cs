﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 직렬화 - 세이브와 로딩에 있어서 필수적인 속성! (컴터가 읽고 쓰기 쉽게)
[System.Serializable] // class 만들 때 직렬화 필요

public class SNLData // 모든 세이브 기록들 담을 곳
{
    // 저장할 것 ~
    // 보관상자, 장착템 / 땅 상태, 심어진 것, 농작물 상태

    // 포함 완료 ~
    // 플레이어 좌표, 씬 이름
    // 날짜(DAY), 시간(타이머), stamina
    // 보유금액, laborCount
    // *** 랜덤스폰의 경우 따로 좌표를 저장하지 않고 불러올 때 새로 스폰함 ***
    // <닭> 닭 개수(theCount), 각 닭의 행복도, 닭 좌표(=랜덤스폰), checkEgg
    // <달걀> 각 달걀 개수, 달걀 좌표(=랜덤스폰)
    // <인벤> 인벤템 ID, 인벤템 개수

    // Vector3 등의 class는 직렬화가 안 됨 (자료형만 직렬화 가능)
        
    public float playerX; // 플레이어 좌표 (X)
    public float playerY; // 플레이어 좌표 (Y)
    public string sceneName; // 씬 이름

    public int day; // 날짜(DAY)
    public float timer; // 시간(타이머)
    public int stamina; // stamina

    public int money; // 보유금액
    public int laborCount; // laborCount

    public int chickenCount; // 닭 개수
    public List<int> happy; // 각 닭의 행복도
    public List<bool> checkEgg; // checkEgg

    public int nEggCount; // 보통 달걀 개수
    public int gEggCount; // 좋은 달걀 개수

    public List<int> characterItemsID; // 인벤템 ID
    public List<int> characterItemsCnt; // 인벤템 개수

    //public int added_atk;
    //public int added_def;
    //public int added_hpr;
    //public int added_mpr;

    //public List<int> playerItemInventory; // 아이템의 ID 값 넣으면 됨
    //public List<int> playerItemInventoryCount; // 몇 개 소지했는지
    //public List<int> playerEquipItem; // 아이템 ID

    // Database에 선언한 리스트들
    //public List<bool> swList;
    //public List<string> swNameList;
    //public List<string> varNameList;
    //public List<float> varNumberList;
}