using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    //인벤토리에 뭐가 있는지 keep track 하고, visible 여부 판단

    private List<UIItem> uiitems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 14;
    public GameObject slot;
    GameObject newSlot;
    public Transform EmpTrans; //강조 UI가 있을 위치

    private void Awake()
    {
        //슬롯 생성
        for(int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            //instance는 현재 slotPrefab을 가리키고, 이 아래에 UIitem이 위치함
            uiitems.Add(instance.GetComponentInChildren<UIItem>());
        }

        newSlot =  Instantiate(slot);
        newSlot.transform.SetParent(EmpTrans);
        newSlot.transform.position = EmpTrans.position;
        newSlot.gameObject.SetActive(false);
        
    }

   

    public void MoveEmphasizedSlot(Transform trans)
    {
        newSlot.SetActive(true);
        EmpTrans.transform.position = trans.position;
        
    }

    public void MakeSlotClear()
    {
        newSlot.SetActive(false);
    }

    //uiitem의 특정 slot을 원하는 item의 모양으로 바꿈
    public void UpdateSlot(int slot, Item item)
    {
        uiitems[slot].UpdateItem(item);

    }

    public void MakeSlotNull()
    {
        foreach(UIItem uiitem in uiitems)
        {
            if (uiitem.item == null || uiitem.item.icon==null)
            {
                uiitem.UpdateItem(null);
            }
        }
    }

    public void UpdateUI(int slot, Item item)
    {
        uiitems[slot].UpdateNumUI(item);
    }

    //uiitem의 요소 중, item 객체가 null인것을 찾아서 원하는 item의 객체의 모양으로 바꿈
    public void AddNewItem(Item item)
    {
        UpdateSlot(uiitems.FindIndex(i => i.item == null), item);
    }


    //item이 있는 인덱스를 찾아서 null객체로 바꿔줌
    public void RemoveItem(Item item)
    {
        UpdateSlot(uiitems.FindIndex(i => i.item == item), null);
    }


    //item의 숫자가 바뀌면 반영해줌
    public void UpdateItemNumUI(Item item)
    {
       UpdateUI(uiitems.FindIndex(i => i.item.id == item.id), item);
    }
}
