using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 말이 있어야 유니티 인스펙터에서 확인 가능
[System.Serializable]

//아이템이 가지고 있을 클래스
public class Item 
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;

    //Dictionary ) key : value로 값 저장 -> 체력회복 : 5 , 속도  :2 이런 느낌
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    

    //아이템 이라는 클래스에 대한 생성자. 이걸로 선언 시 초기화 가능
    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.description = item.description;
        this.stats = item.stats;

        //아이템의 아이콘을 가져오는 방법 : 스프라이트를 "Assets/Resources/Sprites/Items" 위치에서 item.name 이라는 이름의 스프라이트 가져옴
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.name);
    }

    public Item(int id, string name, string description, Dictionary<string,int> stat)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.stats = stat;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + name);

    }
}


