using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellingUI : MonoBehaviour
{
    private inventory inven;
    private List<SUIItem> itemSlotList = new List<SUIItem>();
    public bool isItemChanged = true; //아이템이 추가, 삭제될 경우

    public List<Item> sellingList = new List<Item>();
    public int totalMoney=0;
    public TextMeshProUGUI totalMoneyText;

    public Button sellBtn;
    private PlayerControll playerScript;

    public bool shouldMakeEmpClear = false; //장착하고 있는 물건 팔때 강조창 없애기 용

    void Awake() {
        foreach (GameObject itemSlot in GameObject.FindGameObjectsWithTag("sellingSlot"))
        {
            //itemSlotList가 List라는 자료형이므로, Add를 이용해서 배열 끝에 새로운 SUIItem을 추가할 수 있다.
            itemSlotList.Add(itemSlot.GetComponentInChildren<SUIItem>());
        }
    }
    void Start()
    {
        inven= GameObject.Find("Inventory").GetComponent<inventory>();
        totalMoneyTextUpdate();
        sellBtn.onClick.AddListener(sellItems);
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    void sellItems()
    {
        //판매 버튼을 누르면 플레이어 돈이 추가됨.
        playerScript.playerMoneyChange(totalMoney, true);

        //판매창 리셋
        //돈 다시 0원으로
        resetMoney();
        //아이템들을 구매 리스트에서 제거
        foreach(SUIItem sellingItems in itemSlotList)
        {
            sellingItems.ResetItemSlot();
        }
        //판매창에 바뀐 것 적용
        UpdateAllItem();

        //만약 판매 아이템 목록 중 장착한 아이템이 있다면, 장착한 아이템에서도 해제한다.
        if (shouldMakeEmpClear)
        {
            inven.ClearSlot(); //인벤 강조표시 제거
            inven.equipedItem = null;
            shouldMakeEmpClear = false;
        }



    }

    //inventory의 characterItems에 아이템 보관이 되어 있음
    public void totalMoneyTextUpdate()
    {
        totalMoneyText.text = totalMoney.ToString();
    }

    public void totalMoneyChange(int money, bool isPlus)
    {
        if (isPlus)
        {
            totalMoney += money;
        }
        else
        {
            totalMoney -= money;
        }
        totalMoneyTextUpdate();
    }

    public void resetMoney()
    {
        totalMoney = 0;
        totalMoneyTextUpdate();
    }


    void Update()
    {
        
        //아이템이 바뀔때(추가, 삭제)만 인벤토리에서 보여주는 내용 갱신
        if(isItemChanged && gameObject.activeSelf)
        {
            UpdateAllItem();
            isItemChanged = false;
        }
        
    }


    //모든 내용물을 인벤토리와 동일하게 바꿈
    public void UpdateAllItem()
    {

       for(int i = 0; i < 14; i++)
        {
            Item item = inven.GetItem(i);
            itemSlotList[i].updateItem(item);

        }
    }

   
}
