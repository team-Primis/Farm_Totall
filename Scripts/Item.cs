using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 말이 있어야 유니티 인스펙터에서 확인 가능
[System.Serializable]

//아이템이 가지고 있을 클래스
public class Item 
{
    public int id;
    public string Kname; //한국 이름
    public string Ename; //영어 이름 : 상점 등 때문에 한국 이름이 필요한데, 변수 처리 시 영어가 용이해서 둘 다 넣음
    public string description;
    public Sprite icon;
    public int count;
    public Category category;
    public enum Category
    {
        //item : 소모품, tool : 도구
        item,tool
    }

    //Dictionary ) key : value로 값 저장 -> 체력회복 : 5 , 속도  :2 이런 느낌
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    

    //아이템 이라는 클래스에 대한 생성자. 이걸로 선언 시 초기화 가능
    public Item(Item item)
    {
        this.id = item.id;
        this.Kname = item.Kname;
        this.Ename = item.Ename;
        this.description = item.description;
        this.stats = item.stats;
        this.count = item.count;
        this.category = item.category;

        //아이템의 아이콘을 가져오는 방법 : 스프라이트를 "Assets/Resources/Sprites/Items" 위치에서 item.name 이라는 이름의 스프라이트 가져옴
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.Ename);
    }

    public Item(int id, string Kname, string Ename, string description, Category cat, Dictionary<string,int> stat)
    {
        this.id = id;
        this.Kname = Kname;
        this.Ename = Ename;
        this.description = description;
        this.stats = stat;
        this.category = cat;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + Ename);

    }

    public Item(int id, string Kname, string Ename, string description, Category cat)
    {
        this.id = id;
        this.Kname = Kname;
        this.Ename = Ename;
        this.description = description;
        this.category = cat;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + Ename);

    }
}


