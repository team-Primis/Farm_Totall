using UnityEngine;
using System.Collections;

//ItemScript에 사용되는, 각 아이템의 속성을 정의한 클래스

[System.Serializable]
// 스크립트의 직렬화에 대한 소스코드
// 이렇게 써야만 Item에 대한것을 inspector 창에서 볼 수 있다
 
public class Item {
    public string itemName;         // 아이템의 이름
    public int itemID;              // 아이템의 고유번호
    public string itemDef;          // 아이템의 설명
    public Texture2D itemIcon;      // 아이템의 아이콘(2D)
    public ItemType itemType;       // 아이템의 속성 설정
    
    public enum ItemType            // 아이템의 속성 설정에 대한 갯수
    {
        Tool,                     // 도구류
        Quest,                      // 퀘스트 아이템류
        Use                           // 소모품류
       
    }

    public Item()
    {

    }

    public Item(string img, string name, int id, string def, ItemType type)
        // 생성자 : 만들면서 동시에 초기화 가능
    {
        itemName = name;
        itemID = id;
        itemDef = def;
        itemType = type;

        // itemIcon도 String으로 표현할 수 있다!
        itemIcon = Resources.Load<Texture2D>("item" + img);
        
    }
}
