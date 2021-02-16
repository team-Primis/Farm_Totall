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
    public Toolkit tool;
    public Text ItemCountText;
    private inventory inven;



    void Awake()
    {
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        spriteImage = gameObject.GetComponent<Image>();
        UpdateItem( null); //처음엔 빈칸으로 시작하기!
        selectedItem = GameObject.Find("selectedItem").GetComponent<UIItem>();
        tool = GameObject.Find("Tooltip").GetComponent<Toolkit>();
    }

    //인벤의 보이는 부분을 업데이트 하는 함수.
    //item 객체를 전달해주면 그 아이템의 스프라이트로 바꾸고, 전달받은 객체가 null이면 그냥 투명하게 함
    public void UpdateItem(Item item)
    {
        this.item = item;
        //아이템 객체가 존재할 경우, a 모습으로 바꾸고 잘보이게 하얀색~
        if (this.item != null && this.item.Ename != "")
        {
            spriteImage.sprite = this.item.icon;
            spriteImage.color = Color.white;
            ItemCountText.text = this.item.count.ToString();

            //아이템의 카테고리면 수량이 보이지 않도록!
            if (this.item.category == Item.Category.tool)
            {
                ItemCountText.color = Color.clear;
            }
            else
            {
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
        if (this.item != null && this.item.Ename != "")
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

            if (this.item != null && this.item.Ename != "")
            {
                //장착된 아이템의 위치를 옮길 경우 , 해당 아이템 장착 해제
                if (inven.equipedItem == this.item)
                {
                    inven.equipedItem = null;
                    inven.ClearSlot();

                }
                //특정 아이템을 클릭한 다음 이 칸을 누른다면 둘이 모양 바꿔줌
                if (selectedItem.item != null && selectedItem.item.Ename != "")
                {
                    Item clone = selectedItem.item;
                    selectedItem.UpdateItem(this.item);
                    UpdateItem(clone);
                }
                //아무것도 안클릭하고 이 칸을 누르면, 새롭게 누른 칸에 아이템 옮겨주고, 원래칸은  null
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

        //우클릭을 하면, 장착된 아이템이 바뀌고, 그 아이템의 위치를 강조해준다.
        //누른 UIitem의 위치를 inven의 변수에 저장하고, 이에 따라서 슬롯 강조 이미지 위치 바뀜
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inven.equipedItem = this.item;
            if (this.item != null)
            {
                if (this.item.Ename == "")
                {
                    Debug.Log("이름ㄴㄴ");
                }

                Debug.Log("slot의 item은 " + this.item.Ename + "입니다");
                inven.MoveSlot(this.transform);

            }

            else
            {
                inven.ClearSlot();
                Debug.Log("빈 슬롯");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null && this.item.Ename != "")
        {
            //  tool.GenerateToolTip(this.item);
            tool.gameObject.SetActive(true);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tool.gameObject.SetActive(false);
    }


}
