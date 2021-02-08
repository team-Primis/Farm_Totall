using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingUI : MonoBehaviour
{
    private inventory inven;
    private List<SUIItem> itemSlotList = new List<SUIItem>();
    private bool isEnrolled;
    public bool isItemChanged = true; //아이템이 추가, 삭제될 경우


    void Start()
    {
        inven= GameObject.Find("Inventory").GetComponent<inventory>();
    }

    //inventory의 characterItems에 아이템 보관이 되어 있음

    void Update()
    {
        if(!isEnrolled && gameObject.activeSelf)
        {
            //sellingSlot 태그가 들어있는 모든 GameObject를 특정 배열에 넣어서, 각각의 요소를 itemSlot이라는 이름으로 접근한다. (foreach)
            foreach(GameObject itemSlot in GameObject.FindGameObjectsWithTag("sellingSlot"))
            {
                //itemSlotList가 List라는 자료형이므로, Add를 이용해서 배열 끝에 새로운 SUIItem을 추가할 수 있다.
                itemSlotList.Add(itemSlot.GetComponentInChildren<SUIItem>());
            }
            isEnrolled = true;
        }
        //아이템이 바뀔때(추가, 삭제)만 인벤토리에서 보여주는 내용 갱신
        if(isItemChanged && gameObject.activeSelf)
        {
           // UpdateAllItem();
            isItemChanged = false;
        }
        
    }

    //모든 내용물을 인벤토리와 동일하게 바꿈
    public void UpdateAllItem()
    {
        // i번째의 itemSlotList의 item을 i번째의 inventory.characteritems의 item을 넣고 싶어!


        //아이템 객체를 inven에서 가져옴
        Item item = inven.characterItems[0];
        if (item.Ename == "") Debug.Log("이름 없음");
        Debug.Log(item.Ename);
        // itemSlotList 각각을 갱신함
       foreach(SUIItem itemUI in itemSlotList)
        {
            itemUI.updateItem(item);
        }
    }

    

}
