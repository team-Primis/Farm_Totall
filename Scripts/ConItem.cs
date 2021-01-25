using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConItem : MonoBehaviour, IPointerClickHandler
{

    //Image 안에 color, sprite등 요소가 다 들어있음. 따라서 Image를 가져오는 것

    public Item item;
    public Image spriteImage;

    private inventory inven;
    private UIItem selectedItem;

    void Awake() {
        // spriteImage = gameObject.GetComponent<Image>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        selectedItem = GameObject.Find("selectedItem").GetComponent<UIItem>();
        UpdateItem(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //왼쪽 클릭된 아이템을 ItemToAdd에 저장
        

        if (item != null)
        {
            Debug.Log(item.Ename + " 아이템을 넣을 예정이군요.");
        }
        else
        {
            Debug.Log("해당칸은 비었군요!");
        }
    }

    public void UpdateItem(Item item) {
        // 이 칸의 객체를 바꿔줌! ( 눈에 안보이는 부분)
        this.item = item;
        if(this.item != null) // 스프라이트 변경 ( 눈에 보이는 부분)
        {
            spriteImage.sprite = this.item.icon;
            spriteImage.color = Color.white;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }
}
