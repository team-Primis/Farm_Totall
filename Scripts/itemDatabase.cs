﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()
    {
        BuildDataBase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    //인게임의 모든 아이템을 여기에 쓸것. 특성은 마음대로 정해도 됨! 쓸게 없으면 특성은 비워도 됌!
    private void BuildDataBase()
    {
        items = new List<Item>()
        {
            //item 형식 : item(id, 한국이름, 미국이름, 설명, 아이콘, 스텟)
            /*
             *특성 설정 (영어로 추가하고, 설명 써주기!) 
             * 미해 : cost - 구매 시 가격 sellingPrice - 판매 시 가격 
             *        watering - 물 간격 dayToGrow - 성장기간 count - 수량
             * 
             * 
             */
            new Item(0,"호박 씨앗","pumpkinSeed","탐스러운 호박이 자란다",Item.Category.item,
            new Dictionary<string, int>{
                //구매가격, 물 주기 , 판매가격, 성장기간
                {"cost",1000 },{"watering", 5},{"sellingPrice",2000},{"dayToGrow",5}
            }),
            new Item(1,"파란꽃 씨앗","blueFlowerSeed","밤에 보면 더욱 예쁘다.",Item.Category.item,
            new Dictionary<string, int>
            {
                {"cost",500 },{"watering",1},{"sellingPrice",600},{"dayToGrow",3}
            }),
            new Item(2,"감자 씨앗","potatoSeed","감자 그라탕 하기에 적합한 감자다.",Item.Category.item,
            new Dictionary<string, int>
            {
                {"cost",50 },{"watering",7},{"sellingPrice",100},{"dayToGrow",10}
            }),
            new Item(3,"고구마 씨앗","sweetPotatoSeed","토종 밤고구마 품종이다",Item.Category.item,
            new Dictionary<string, int>
            {
                {"cost",70 },{"watering",7},{"sellingPrice",150},{"dayToGrow",7}
            }),
            

        };
    }
}