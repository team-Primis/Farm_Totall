using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//눈에 보이는 외부 UI에 대한 함수

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public Image spriteImage;
    private UIItem selectedItem;
    private Toolkit tool;
    public Text ItemCountText;

    private void Awake()
    {
        spriteImage = gameObject.GetComponent<Image>();
        UpdateItem(null); //처음엔 빈칸으로 시작하기!
        selectedItem = GameObject.Find("selectedItem").GetComponent<UIItem>();
        tool = GameObject.Find("Tooltip").GetComponent<Toolkit>();
    }

    //인벤의 보이는 부분을 업데이트 하는 함수.
    //item 객체를 전달해주면 그 아이템의 스프라이트로 바꾸고, 전달받은 객체가 null이면 그냥 투명하게 함
    public void UpdateItem(Item item)
    {
        this.item = item;
        //아이템 객체가 존재할 경우, a 모습으로 바꾸고 잘보이게 하얀색~
        if(this.item != null)
        {
            spriteImage.sprite = this.item.icon;
            spriteImage.color = Color.white;
            ItemCountText.text = this.item.count.ToString();

            //아이템의 카테고리면 수량이 보이지 않도록!
            if (this.item.category == Item.Category.tool)
            {
                ItemCountText.color = Color.clear;
            }
            else {
                ItemCountText.color = Color.white;
            }
        }
        //전달받은 객체가 null일경우 그냥 투명하게
        else
        {
            spriteImage.color = Color.clear;
            ItemCountText.color = Color.clear;
        }
    }

    //오브젝트에 해당하는 갯수로 업데이트 해주는 함수
    public void UpdateNumUI(Item item)
    {
        if(this.item != null)
        {
            ItemCountText.text = this.item.count.ToString();
        }
    }

    //클릭해서 눈에 보이는 위치 바꾸기 = 객체 바꾸기 & 눈에 보이는 이미지 바꾸기
    //실제 인벤의 내용이 바뀌진 않으므로 신경쓰지 않아도 됌.
    public void OnPointerClick(PointerEventData eventData)
    {
        //오른 버튼이 클릭되면 드래그로 위치 바꾸기 가능
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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (this.item != null)
            {
                tool.GenerateToolTip(this.item);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            tool.GenerateToolTip(this.item);
            tool.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tool.gameObject.SetActive(false);
    }
}
