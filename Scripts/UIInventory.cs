﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    //인벤토리에 뭐가 있는지 keep track 하고, visible 여부 판단

    //private List<UIItem> uiitems = new List<UIItem>();
    public List<UIItem> uiitems = new List<UIItem>(); // 저장할 때 끌어오려고 public으로 바꿨음 (성현)
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 14;
    public GameObject slot;
    GameObject newSlot;
    public Transform EmpTrans; //강조 UI가 있을 위치
    Item emptyItem;

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
        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

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
            if (uiitem.item == emptyItem || uiitem.item.icon==null)
            {
                uiitem.UpdateItem(emptyItem);
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
        UpdateSlot(uiitems.FindIndex(i => i.item.Ename=="empty"), item);
    }


    //item이 있는 인덱스를 찾아서 null객체로 바꿔줌
    public void RemoveItem(Item item)
    {
        UpdateSlot(uiitems.FindIndex(i => i.item == item), emptyItem);
    }


    //item의 숫자가 바뀌면 반영해줌
    public void UpdateItemNumUI(Item item)
    {
       UpdateUI(uiitems.FindIndex(i => i.item.id== item.id), item);
    }

    //ui전부 카운트를 실제와 같게 맞춰주기
    public void UpdateItemNumUICount(Item item)
    {
        //같은 id를 가진 아이템리스트 전부 찾기
        List<UIItem> sameId = uiitems.FindAll(i => i.item.id == item.id);

        //아이템리스트의 카운트와 실제 카운트가 다르면, 같게 맞춰주기!
        for(int i = 0; i < sameId.Count; i++)
        {
            if(sameId[i].item.count != int.Parse(sameId[i].ItemCountText.text))
            {
                sameId[i].ItemCountText.text = sameId[i].item.count.ToString();
            }
        }
    }

}
