using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingTotal : MonoBehaviour
{
    private PlayerControll playerScript;
    private inventory inven;
    public Text seedNameText;
    public Text moneyText;
    public Text buyNumText;

    public int seedMoney; //씨앗 구매시 필요한 돈 (구매창에 뜨는 용)
    public int buyNum; // 현재 구매할 씨앗 창 (구매창 표시)
    public string seedName; //현재 구매할 씨앗 이름
    public int totalMoney;

    public Button plusBtn;
    public Button minusBtn;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        plusBtn.onClick.AddListener(AddBuyNum);
        minusBtn.onClick.AddListener(SubBuyNum);
      
    }
    void AddBuyNum()
    {
        if (buyNum < 99)
        {
            buyNum++; //총 살 갯수
            totalMoney = buyNum * seedMoney; //필요한 돈 총액
                                             //UI반영
            moneyText.text = totalMoney.ToString() + "원";
            buyNumText.text = buyNum.ToString();
        }
    }

    void SubBuyNum()
    {
        if (buyNum > 1)
        {
            buyNum--; //총 살 갯수
            totalMoney = buyNum * seedMoney; //필요한 돈 총액
                                             //UI반영
            moneyText.text = totalMoney.ToString() + "원";
            buyNumText.text = buyNum.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
