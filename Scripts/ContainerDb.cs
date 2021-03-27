using System.Collections;
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
  

    void Awake()
    {
        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

        db = GameObject.Find("CDatabase").GetComponent<containerDatabase>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();

        PickUpBtn = GameObject.Find("PickUpBtn").GetComponent<Button>();
        PickUpBtn.onClick.AddListener(PickUp);

        conUI = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerUI>();
        containerItem = GameObject.Find("ContainerItems").GetComponent<ContainerItems>();
        selectedItem = emptyItem; //0327 처음에 emptyItem으로 초기화!

    }
    //보관상자 -> 인벤 이동
    void PickUp()
    {
        if(selectedItem.Ename != "empty")
        {
            //인벤창에 아이템 추가
            inven.putInventory(selectedItem, selectedItem.count);
            //RemoveItem(selectedItem.id); //containerItem의 list에서 해당 아이템 제거
            containerItem.container.Remove(selectedItem);
            conUI.isContainerChanged = true;
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
            Debug.Log(itemToRemove.Kname + "아이템을 list에서 제거합니다");
            containerItem.container.Remove(itemToRemove);
            //conUI.RemoveItem(itemToRemove);
            conUI.isContainerChanged = true;
        }
        else
        {
            Debug.Log("해당 아이템은 db에 존재하지 않습니다");
        }

    }


}
