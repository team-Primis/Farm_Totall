using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUIItem : MonoBehaviour
{
    public Item item;
    public Image spriteImage;

    //슬롯의 내용(아이템 객체) + 눈에 보이는 부분(이미지) 변경
    public void updateItem(Item item)
    {
        this.item = item;

        //null로 바꿀 시 ( 아이템 삭제랑 같은 의미 )
        if (this.item == null)
        {
            spriteImage.color = Color.clear;
            return;
        }
        //아이템이 null 이 아닐 시 : 눈에 보이는 모습을 해당 아이콘 모양으로 바꿔줌
        spriteImage.sprite = item.icon;
        spriteImage.color = Color.white;

    }
}
