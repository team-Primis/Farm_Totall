using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//보관상자에 있는 한 칸의 아이템에 대한 코드
//어떤 아이템인지에 대한 정보 (ITem 객체) + 눈에 보이는 부분 (UI)로 구성

public class CUIItem : MonoBehaviour, IPointerClickHandler
{
    //한 칸에 담긴 아이템의 객체
    public Item item;
    private ContainerDb container;
    public Image spriteImage;
    public Text itemText;
    Item emptyItem;

    // Start is called before the first frame update
    void Start()
    {
        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

        container = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerDb>();
        spriteImage = gameObject.GetComponent<Image>();
        UpdateItem(emptyItem);
    }


    //해당 아이템이 클릭되면, 보관상자의 selectedItem에 저장 -> 보관상자에서 꺼내기 버튼 OR 단축키 누르면 해당 아이템 꺼내짐
    public void OnPointerClick(PointerEventData eventData)
    {

        container.selectedItem = this.item;

        //선택된 아이템이 무엇인지 알려줌
        if (this.item.Ename != "empty")
        {
            container.MoveSlot(this.transform);

        }
        else
        {
            Debug.Log("빈슬롯");
            container.ClearSlot();
        }

    }

    public void UpdateUI(Item item)
    {
        if (item.Ename != "empty")
        {
            itemText.text = this.item.count.ToString();
        }

    }

    public void UpdateItem(Item newItem)
    {
        //담긴 객체 변경
        this.item = newItem;
        //UI 변경 : 이미지 변경, 수량 변경. 카테고리가 아이템인것만 수량 표시
        if (this.item.Ename != "empty")
        {
            spriteImage.sprite = this.item.icon;
            spriteImage.color = Color.white;
            itemText.text = this.item.count.ToString();

            if (this.item.category == Item.Category.item)
            {
                itemText.color = Color.white;
            }
            else
            {
                itemText.color = Color.clear;
            }
        }
        //null이 들어오면 그냥 투명하게 유지한다.
        else
        {
            spriteImage.color = Color.clear;
            itemText.color = Color.clear;
        }
    }
}
