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

    // Start is called before the first frame update
    void Start()
    {
        container = GameObject.Find("ContainerPanel").GetComponent<ContainerDb>();
        spriteImage = gameObject.GetComponent<Image>();
        UpdateItem(null);
    }


    //해당 아이템이 클릭되면, 보관상자의 selectedItem에 저장 -> 보관상자에서 꺼내기 버튼 OR 단축키 누르면 해당 아이템 꺼내짐
    public void OnPointerClick(PointerEventData eventData)
    {
           container.selectedItem = this.item;
        
    }

    public void UpdateUI(Item item)
    {
        if (item.Ename != null)
        {
            itemText.text = this.item.count.ToString();
        }

    }

    public void UpdateItem(Item newItem)
    {
        //담긴 객체 변경
        this.item = newItem;
        //UI 변경
        if (this.item != null)
        {
            spriteImage.sprite = this.item.icon;
            spriteImage.color = Color.white;

            if (this.item.category == Item.Category.item)
            {
                itemText.text = this.item.count.ToString();
                itemText.color = Color.white;
            }
            else
            {
                itemText.color = Color.clear;
            }
        }
        else
        {
            spriteImage.color = Color.clear;
            itemText.color = Color.clear;
        }
    }
}
