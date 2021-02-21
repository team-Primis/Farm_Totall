using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VendorItem : MonoBehaviour, IPointerClickHandler
{
    private VendingTotal vendingScript;
    private itemDatabase db;

    private Image buyImg;

    

    // Start is called before the first frame update
    void Start()
    {
        vendingScript = GameObject.Find("vendingPanel").GetComponent<VendingTotal>();
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        buyImg = GameObject.Find("seedImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //각 아이템이 클릭되면, 태그에 맞는 db에 저장된 객체 가져옴!
    public void OnPointerClick(PointerEventData eventData)
    {
        string itemTag = gameObject.tag;
        Item ClickedItem = db.GetItem(itemTag);
        if (ClickedItem == null)
        {
            Debug.Log("From 자판기 : 해당 이름의 아이템은 db에 존재하지 않습니다.");
        }
        else {
            //Debug.Log("item의 태그는 " + itemTag + "입니다.");
            vendingScript.seedName = itemTag;
            vendingScript.seedNameText.text = ClickedItem.Kname;

            vendingScript.buyNum = 1;
            vendingScript.buyNumText.text = 1.ToString();
            // 구매창에 뜬 이미지를 클릭한 이미지의 아이콘으로 바꿔주기
            buyImg.sprite = ClickedItem.icon; 
            

            foreach(KeyValuePair<string,int> keyValue in ClickedItem.stats)
            {
                if(keyValue.Key == "cost")
                {
                    //Debug.Log(itemTag + "의 가격은 " + keyValue.Value + "입니다.");
                    int itemPrice = keyValue.Value;

                    vendingScript.seedMoney = itemPrice;
                    vendingScript.totalMoney = itemPrice;
                    vendingScript.moneyText.text = itemPrice.ToString() + "원";

                }
            }
        }
    }
}
