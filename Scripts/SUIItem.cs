using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SUIItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public Image spriteImage;
    public Text countText;
    public SellingUI sellingUI;
    public bool isAdded = false;
    public Image spriteBackground;

    private inventory inven;

    void Start() {
        updateItem(null);
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //해당 칸에 아이템이 존재할때
        if (this.item != null && this.item.Ename != "")
        {
            int itemMoney; //해당 아이템의 가격
            itemMoney = this.item.stats["sellingPrice"];

            //한번 클릭되면 구매 리스트에 들어감 + 색깔 민트
            if (!isAdded)
            {
                
                sellingUI.totalMoneyChange(itemMoney, true);
                sellingUI.sellingList.Add(this.item);
                isAdded = true;
                spriteBackground.color = new Color32(0, 255, 142, 172);
            }
            //두번 클릭되면 구매 리스트에서 삭제 + 색깔 원상복귀
            else
            {
                sellingUI.totalMoneyChange(itemMoney, false);
                sellingUI.sellingList.Remove(this.item);
                isAdded = false;
                spriteBackground.color = new Color32(0, 0, 0, 100);


            }
        }
    }

    
    //아이템 선택된 것 -> 구매버튼 누르면 삭제되기
    public void ResetItemSlot()
    {
        if (isAdded)
        {
            inven.RemoveItem(this.item.id);
            //구매리스트에서 아이템 제거
            sellingUI.sellingList.Remove(this.item);
            //겉에 보이는 아이템 모습 제거 +item 객체 제거
            updateItem(null);
            isAdded = false;
            spriteBackground.color = new Color32(0, 0, 0, 100);
        }
    }


    //슬롯의 내용(아이템 객체) + 눈에 보이는 부분(이미지) 변경
    public void updateItem(Item item)
    {
        this.item = item;

        //null로 바꿀 시 ( 아이템 삭제랑 같은 의미 )
        if (this.item == null)
        {
            spriteImage.color = Color.clear;
            countText.color = Color.clear;
            return;
        }
        //아이템이 null 이 아닐 시 : 눈에 보이는 모습을 해당 아이콘 모양으로 바꿔줌
        spriteImage.sprite = item.icon;
        spriteImage.color = Color.white;
        if (this.item.category == Item.Category.item)
        {
            countText.text = item.count.ToString();
            countText.color = Color.white;
   
        }
        else
        {
            countText.color = Color.clear;
        }


    }
}
