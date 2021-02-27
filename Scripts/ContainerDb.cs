﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerDb : MonoBehaviour
{
    // Start is called before the first frame update
    public containerDatabase db;
    public  ContainerUI conUI;
    private inventory inven;

    //빼려고 선택된 아이템
    public Item selectedItem;

    public Button PickUpBtn;

    public ContainerItems containerItem;


    Item emptyItem;
  

    void Start()
    {
        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

        db = GameObject.Find("CDatabase").GetComponent<containerDatabase>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();

        PickUpBtn = GameObject.Find("PickUpBtn").GetComponent<Button>();
        PickUpBtn.onClick.AddListener(PickUp);

        conUI = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerUI>();
        containerItem = GameObject.Find("ContainerItems").GetComponent<ContainerItems>();

    }
    //보관상자 -> 인벤 이동
    void PickUp()
    {
        if(selectedItem.Ename != "empty")
        {
            inven.putInventory(selectedItem.id, selectedItem.count);
            RemoveItem(selectedItem.id);
            selectedItem = emptyItem;
            ClearSlot();
        }
        else
        {
            Debug.Log("선택한 아이템이 없어서 꺼낼 수 없습니다.");
        }
    }
   
   

    public void MoveSlot(Transform trans)
    {
        conUI.MoveEmphasizedSlot(trans);
    }

    public void ClearSlot()
    {
        conUI.MakeSlotClear();
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = db.GetItem(id);
        if (itemToRemove != null )
        {
            containerItem.container.Remove(itemToRemove);
            conUI.RemoveItem(itemToRemove);
        }

    }

    //해당 id를 가진 아이템을 인벤에서 보관상자에 넣음 (인벤 스크립트에서 호출)
    public void PutInContainer(int id, int num=1)
    {
        Item item = db.GetItem(id);
         //해당 아아템이 존재하지 않음
        if (item == null) {
            Debug.Log(id + " id 를 가진 아이템은 데이터베이스에 없습니다.");
            return;
        }
        //아이템을 처음 넣을 경우
        if (CheckForItem(id) == null)
        {
            item.count = num;
            //containerItem이라는 보관상자 아이템 보관스크립트에 추가함
            containerItem.container.Add(item);
            conUI.AddNewItem(item);
            //Debug.Log(id + "라는 id를 가진 아이템을 보관상자에 추가합니다. ");
        }
        //아이템이 이미 보관상자에 존재할경우
        else
        {
            item.count += num;
            conUI.UpdateUI(item);
            //Debug.Log(id + "라는 id를 가진 아이템을 " + num + "개 더 추가합니다.");
        }
    }
   

    public Item CheckForItem(int id)
    {
        return containerItem.container.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return containerItem.container.Find(item => item.Ename == name);
    }

}
