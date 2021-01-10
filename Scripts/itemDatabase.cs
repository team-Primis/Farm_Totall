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
            new Item(0,"pumpkin","it grows after 5 days",
            new Dictionary<string, int>{
                //구매가격, 물 주기 , 판매가격
                {"cost",1000 },{"watering", 5},{"selling",2000}
            }),
            new Item(1,"blueFlower","it grows after 3 days",
            new Dictionary<string, int>
            {
                {"cost",500 },{"watering",1},{"selling",600}
            }),
            new Item(2,"potato","it grows after 10 days",
            new Dictionary<string, int>
            {
                {"cost",50 },{"watering",7},{"selling",100}
            }),
            new Item(3,"sweetPotato","it grows after 7 days",
            new Dictionary<string, int>
            {
                {"cost",70 },{"watering",7},{"selling",150}
            })
            

        };
    }
}
