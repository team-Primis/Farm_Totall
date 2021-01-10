using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item item;
    public Image spriteImage;

    private void Awake()
    {
        spriteImage = gameObject.GetComponent<Image>();
        UpdateItem(null); //처음엔 빈칸으로 시작하기!
    }

    //인벤의 보이는 부분을 업데이트 하는 함수.
    //item 객체를 전달해주면 그 아이템의 스프라이트로 바꾸고, 전달받은 객체가 null이면 그냥 투명하게 함
    public void UpdateItem(Item item)
    {
        this.item = item;
        //아이템 객체가 존재할 경우, 그 모습으로 바꾸고 잘보이게 하얀색~
        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        //전달받은 객체가 null일경우 그냥 투명하게
        else
        {
            spriteImage.color = Color.clear;
        }
    }
}
