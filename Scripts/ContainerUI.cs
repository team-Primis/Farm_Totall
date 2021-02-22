using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public bool isItemEnrolled = false; //한번만 실행되게 하는 용 (containerUI랑 CUItem연결용) : 맨 처음에 시도할경우 CUItem이 setActive(false)라 반영이 안됨
    private List<CUIItem> container = new List<CUIItem>();

    public GameObject slot;
    GameObject newSlot;
    public Transform EmpTrans; //강조 UI가 있을 위치

    public GameObject[] itemSlotList;

    public ContainerItems containerItemScript;

    // Awake는 어떤 객체가 처음 setActive = true 될때 실행된다. ->따라서 처음 한번만 실행되야 하는 것들으나 여기에 넣었다.
    void Awake()
    {
        //강조슬롯 관련
        newSlot = Instantiate(slot);
        newSlot.transform.SetParent(EmpTrans);
        newSlot.transform.position = EmpTrans.position;
        newSlot.gameObject.SetActive(false);

        //여기에 컨테이너의 내용물 관련 스크립트가 들어있다.
        containerItemScript = GameObject.Find("ContainerItems").GetComponent<ContainerItems>();

        itemSlotList = GameObject.FindGameObjectsWithTag("containerSlot");

        //각 칸에 대한 스크립트를 쭉 가져와서 container에 저장한다.
        for (int i = 0; i < itemSlotList.Length; i++)
        {
            //GetComponent<>로 하니까 메소드를 바꿀 수 없습니다 뜸!
            container.Add(itemSlotList[i].GetComponentInChildren<CUIItem>());
        }

        //컨테이너 아이템 리스트에 접근해서, i번째 칸을 i번째 아이템의 모양으로 바꿔줌. ( 씬 이동하고 돌아오면 새롭게 로드되도록
        for (int i = 0; i < containerItemScript.container.Count; i++)
        {
            UpdateSlot(i, containerItemScript.container[i]);

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
        int slot = container.FindIndex(i => i.item == item);
        container[slot].UpdateUI(item);
    }

    public void AddNewItem(Item item)
    {
        if (item != null && item.Ename != "")
        {
            int slot = container.FindIndex(i => i.item == null);
            //Debug.Log(slot + "번째 슬롯이 비었으므로 아이템 " + item.Kname + "을 추가합니다"); //현재 인덱스 확인 용
            //빈 공간이 있으면
            if (slot != -1)
            {
                UpdateSlot(slot, item);
            }
            else
            {
                Debug.Log("보관상자에 저장공간이 부족합니다.");
            }
        }
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(container.FindIndex(i => i.item == item), null);
    }
}
