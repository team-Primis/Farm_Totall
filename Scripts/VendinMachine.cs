using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VendinMachine : MonoBehaviour
{
    public int buyNum = 1;
    public TMP_Text buyText;
    public TMP_Text moneyText;
    public TMP_Text seedNameText;
    public int seedNum;
    public bool isBuyNumChanged = false;
    public bool isSeedNameChanged = false;
    
    public Button plusBtn, minusBtn;
    public Button flowerButton, pumpkinButton, potatoButton, sweetpButton;
    public Button buyBtn;

    public int seedMoney= 200; //현재 선택된 씨앗의 가격
    public string seedName;

    public Image seedImage;
    public Sprite blueSprite;
    public Sprite PumpkinSprite;
    public Sprite potatoSprite;
    public Sprite sweetpSprite;

    public int[] seedBought;

    enum seedNUM { PUMKIN, BLUEFLOWER, POTATO, SWEETP};

    PlayerControll playerScript;

    // Start is called before the first frame update
    void Start()
    {
        plusBtn.onClick.AddListener(AddBuyNum);
        minusBtn.onClick.AddListener(SubBuyNum);
        flowerButton.onClick.AddListener(flowerBtn);
        pumpkinButton.onClick.AddListener(pumpkinBtn);
        potatoButton.onClick.AddListener(potatoBtn);
        sweetpButton.onClick.AddListener(sweetPotatoBtn);
        buyBtn.onClick.AddListener(BuySeed);

        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //살 갯수가 바뀌면 그에 따라서 text변경
        IfBuyNumChange();
        //누른 씨앗 이름 바뀌면 반영
        changeSeedName();
    }

    void IfBuyNumChange() {
        if (isBuyNumChanged)
        {
            isBuyNumChanged = false;
            buyText.text = buyNum.ToString();
            moneyText.text = (seedMoney * buyNum).ToString()+ " coin";

        }
    }

    void changeSeedName() {
        if (isSeedNameChanged) {
            seedNameText.text = seedName;
        }
    }
    //플러스 버튼
    void AddBuyNum()
    {
        buyNum++;
        isBuyNumChanged = true;
    }

    //마이너스 버튼
    void SubBuyNum()
    {
        Debug.Log("Minus Clicked");
        if (buyNum > 1)
        {
            buyNum--;
            isBuyNumChanged = true;
        }
    }

    //꽃 누름 : 돈 바꿔주고, 갯수도 1개로 초기화
    void flowerBtn() {
        seedMoney = 100;
        buyNum = 1;
        isBuyNumChanged = true;
        seedName = "blueFlower";
        isSeedNameChanged = true;
        seedImage.sprite = blueSprite;
        seedNum = (int)seedNUM.BLUEFLOWER;
    }

    void pumpkinBtn()
    {
        seedMoney = 200;
        buyNum = 1;
        isBuyNumChanged = true;
        seedName = "pumpkin";
        isSeedNameChanged = true;
        seedImage.sprite = PumpkinSprite;
        seedNum = (int)seedNUM.PUMKIN;
    }

    void potatoBtn()
    {
        seedMoney = 50;
        buyNum = 1;
        isBuyNumChanged = true;
        seedName = "potato";
        isSeedNameChanged = true;
        seedImage.sprite = potatoSprite;
        seedNum = (int)seedNUM.POTATO;
    }

    void sweetPotatoBtn()
    {
        seedMoney = 30;
        buyNum = 1;
        isBuyNumChanged = true;
        seedName = "sweetPotato";
        isSeedNameChanged = true;
        seedImage.sprite = sweetpSprite;
        seedNum = (int)seedNUM.SWEETP;
    }

    void BuySeed() {

        //다시 구현해야됌
    }
}
