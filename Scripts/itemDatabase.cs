using System.Collections;
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

    private void BuildDataBase()
    {
        //items = new List<item>{new Item(new item),new Item()}
        items = new List<Item>()
        {
            //item 형식 : item(id, name, 설명, 아이콘, 스텟)
            new Item(0,"호박","아주 탐스럽다",
            new Dictionary<string, int>{
                //구매가격, 물 주기 , 판매가격, 성장기간
                {"가격",1000 },{"물 주기", 5},{"판매가",2000},{"성장 기간",5}
            }),
            new Item(1,"파란꽃","밤에 보면 더욱 예쁘다.",
            new Dictionary<string, int>
            {
                {"cost",500 },{"watering",1},{"selling",600},{"성장 기간",3}
            }),
            new Item(2,"감자","감쟈는 맛있다",
            new Dictionary<string, int>
            {
                {"cost",50 },{"watering",7},{"selling",100},{"성장 기간",10}
            }),
            new Item(3,"고구마","토종 밤고구마이다",
            new Dictionary<string, int>
            {
                {"cost",70 },{"watering",7},{"selling",150},{"성장 기간",7}
            })
            

        };
    }
}
