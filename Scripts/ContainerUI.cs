using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public bool isItemEnrolled = false; //한번만 실행되게 하는 용 (containerUI랑 CUItem연결용) : 맨 처음에 시도할경우 CUItem이 setActive(false)라 반영이 안됨
    public GameObject containerObj;
    public List<CUIItem> container = new List<CUIItem>();

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void AddNewItem(Item item)
    {
        UpdateSlot(container.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(container.FindIndex(i => i.item == item), null);
    }
}
