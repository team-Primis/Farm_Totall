using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤토리 쓸때!
// 여기 밑 참고 + itemDatabase에 양식에 맞춰서 넣을 것

public class Test : MonoBehaviour
{
    private inventory Inven;
    // Start is called before the first frame update
    void Start()
    {

        //이렇게 인벤토리 가져오고
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();

        if(Inven.equipedItem.Ename == "pumpkin") //호박 장착
        {

        }


        //Inven.equiptedItem.원하는 특성 // 이렇게 접근 가능


        //Inven.putInventory(아이템아이디); 인벤토리에 넣기
        //Inven.RemoveItem(아이템아이디); 인벤토리에서 제거하기
        //if(Inven.equipedItem.Ename == "특정 아이템 이름") //특정 아이템 이름이 장착된 경우 ( 인벤에서 우클릭)
        // Inven.UseItem(아이템아이디) // 특정 아이템 사용하기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
