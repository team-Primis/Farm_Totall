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

    // Start is called before the first frame update
    void Awake()
    {
        newSlot = Instantiate(slot);
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

    void Update()
    {
        //한번도 실행된 적 없고, 현재 컨테이너가 멀쩡히 켜져있으면 한번 실행
        
        if (!isItemEnrolled && gameObject.activeSelf == true)
        {
            foreach (GameObject itemslots in GameObject.FindGameObjectsWithTag("containerSlot"))
            {
                //GetComponent<>로 하니까 메소드를 바꿀 수 없습니다 뜸!
                container.Add(itemslots.GetComponentInChildren<CUIItem>());
            }

            isItemEnrolled = true;
        }
        
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
