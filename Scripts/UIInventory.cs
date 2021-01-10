using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    //인벤토리에 뭐가 있는지 keep track 하고, visible 여부 판단

    public List<UIItem> uiitems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 32;

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
    }

    //uiitem의 특정 slot을 원하는 item의 모양으로 바꿈
    public void UpdateSlot(int slot, Item item)
    {
        uiitems[slot].UpdateItem(item);

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
}
