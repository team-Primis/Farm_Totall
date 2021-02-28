using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public bool isItemEnrolled = false; //한번만 실행되게 하는 용 (containerUI랑 CUItem연결용) : 맨 처음에 시도할경우 CUItem이 setActive(false)라 반영이 안됨

    public List<CUIItem> container = new List<CUIItem>(); // 저장할 때 끌어오려고 public으로 바꿨음 (성현)

    public GameObject slot;
    GameObject newSlot;
    public Transform EmpTrans; //강조 UI가 있을 위치

    

    public ContainerItems containerItemScript;

    Item emptyItem;


    // Awake는 어떤 객체가 처음 setActive = true 될때 실행된다. ->따라서 처음 한번만 실행되야 하는 것들으나 여기에 넣었다.
    void Awake()
    {
        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

        //강조슬롯 관련
        newSlot = Instantiate(slot);
        newSlot.transform.SetParent(EmpTrans);
        newSlot.transform.position = EmpTrans.position;
        newSlot.gameObject.SetActive(false);

        //여기에 컨테이너의 내용물 관련 스크립트가 들어있다.
        containerItemScript = GameObject.Find("ContainerItems").GetComponent<ContainerItems>();


        //각 칸에 대한 스크립트를 쭉 가져와서 container에 저장한다.
        foreach (GameObject itemslots in GameObject.FindGameObjectsWithTag("containerSlot"))
        {
            //GetComponent<>로 하니까 메소드를 바꿀 수 없습니다 뜸!
            container.Add(itemslots.GetComponentInChildren<CUIItem>());
        }
        
      
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

    

    public void UpdateSlot(int slot, Item item)
    {
        container[slot].UpdateItem(item);
    }

    public void UpdateUI(Item item)
    {
        int slot = container.FindIndex(i => i.item.id == item.id);
        container[slot].UpdateUI(item);
    }

    public void AddNewItem(Item item)
    {
        if (item.Ename != "empty")
        {
            //빈 공간 찾기
            int slot = container.FindIndex(i => i.item.Ename == "empty");
            //Debug.Log(slot + "번째 슬롯이 비었으므로 아이템 " + item.Kname + "을 추가합니다"); //현재 인덱스 확인 용
            //빈 공간이 있으면 그 슬롯을 item의 아이콘으로 바꿔줌
            if (slot != -1)
            {
                UpdateSlot(slot, item);
            }
            else
            {
                Debug.Log("보관상자에 저장공간이 부족해서 UI에 반영되지 않았습니다.");
            }
        }
        else
        {
            Debug.Log("빈 객체를 넣을 순 없습니다");
        }
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(container.FindIndex(i => i.item == item), emptyItem);
    }
}
