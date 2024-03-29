﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 직렬화 - 세이브와 로딩에 있어서 필수적인 속성! (컴터가 읽고 쓰기 쉽게)
[System.Serializable] // class 만들 때 직렬화 필요

public class SNLData // 모든 세이브 기록들 담을 곳
{
    // 포함 완료 ~
    // 플레이어 좌표, 씬 이름
    // 날짜(DAY), 시간(타이머)
    // stemina
    // 쓴 돈, laborCount
    // <닭> 닭 개수(theCount), 각 닭의 행복도, checkEgg, 닭 좌표
    // <달걀> 각 달걀 개수, 각 달걀 좌표
    // <인벤> 인벤템 ID, 인벤템 개수, 장착템 ID, 장착템 인덱스
    // <상자> 보관템 ID, 보관템 개수
    // <흙> dirt 좌표
    // <식물> plant 좌표, plant 타이머, plant iswatered(+물), plant 종류

    // Vector3 등의 class는 직렬화가 안 됨 (자료형만 직렬화 가능)
        
    public float playerX; // 플레이어 좌표 (X)
    public float playerY; // 플레이어 좌표 (Y)
    public string sceneName; // 씬 이름

    public int day; // 날짜(DAY)
    public float timer; // 시간(타이머)

    public float currentHp; // stemina

    public int usedMoney; // 쓴 돈
    public int laborCount; // laborCount

    public int chickenCount; // 닭 개수
    public List<int> happy; // 각 닭의 행복도
    public List<bool> checkEgg; // checkEgg
    public List<float> chickenXP = new List<float>(); // 닭 x좌표
    public List<float> chickenYP = new List<float>(); // 닭 y좌표
    // -----------------------------------------------------------------------------
    public int nEggCount; // 보통 달걀 개수
    public List<float> nEggXP = new List<float>(); // NE x좌표
    public List<float> nEggYP = new List<float>(); // NE y좌표
    // -----------------------------------------------------------------------------
    public int gEggCount; // 좋은 달걀 개수
    public List<float> gEggXP = new List<float>(); // GE x좌표
    public List<float> gEggYP = new List<float>(); // GE y좌표

    public List<int> characterItemsID; // 인벤템 ID
    public List<int> characterItemsCnt; // 인벤템 개수
    public int equipedItemID; // 장착템 ID
    public int equipedItemIndex; // 장착템 인덱스 (0322 추가)

    public List<int> containerItemsID; // 보관템 ID
    public List<int> containerItemsCnt; // 보관템 개수

    public List<float> dirtXP = new List<float>(); // dirt x좌표
    public List<float> dirtYP = new List<float>(); // dirt y좌표
    // -----------------------------------------------------------------------------
    public List<float> plantXP = new List<float>(); // plant x좌표
    public List<float> plantYP = new List<float>(); // plant y좌표
    public List<float> plantTimer = new List<float>(); // plant 타이머
    public List<bool> plantWater = new List<bool>(); // plant iswatered (물 포함)
    public List<int> plantName = new List<int>(); // plant 종류 (이름과 비교)
    public List<int> plantI = new List<int>(); // plant i값
}
