using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public Image spriteImage;
    private UIItem selectedItem;

    private void Awake()
    {
        spriteImage = gameObject.GetComponent<Image>();
        UpdateItem(null); //처음엔 빈칸으로 시작하기!
        selectedItem = GameObject.Find("selectedItem").GetComponent<UIItem>();
    }

    //인벤의 보이는 부분을 업데이트 하는 함수.
    //item 객체를 전달해주면 그 아이템의 스프라이트로 바꾸고, 전달받은 객체가 null이면 그냥 투명하게 함
    public void UpdateItem(Item item)
    {
        this.item = item;
        //아이템 객체가 존재할 경우, 그 모습으로 바꾸고 잘보이게 하얀색~
        if(this.item != null)
        {
            spriteImage.sprite = this.item.icon;
            spriteImage.color = Color.white;
        }
        //전달받은 객체가 null일경우 그냥 투명하게
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    //클릭해서 눈에 보이는 위치 바꾸기 = 눈에 보이는 이미지 바꾸기
    //실제 인벤의 내용이 바뀌진 않으므로 신경쓰지 않아도 됌.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            
            if (this.item != null)
            {
                //특정 아이템을 클릭한 다음 이 칸을 누른다면 둘이 모양 바꿔줌
                if (selectedItem.item != null)
                {
                    Item clone = new Item(selectedItem.item);
                    selectedItem.UpdateItem(this.item);
                    UpdateItem(clone);
                }
                else
                {
                    selectedItem.UpdateItem(this.item);
                    UpdateItem(null);
                }
            }
            //전에 뭔가 선택 후 빈공간 누름
            else if (selectedItem.item != null)
            {
                UpdateItem(selectedItem.item);
                selectedItem.UpdateItem(null);
            }
        }
    }

    public bool isObject;
 

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        
    }

   
}
