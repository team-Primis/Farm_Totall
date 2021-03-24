using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendingTotal : MonoBehaviour
{
    private PlayerControll playerScript;
    private inventory inven;
    public Text seedNameText;
    public Text moneyText;
    public Text buyNumText;
    public TMP_Text leftMoneyText;

    public int seedMoney; //씨앗 구매시 필요한 돈 (구매창에 뜨는 용)
    public int buyNum; // 현재 구매할 씨앗 창 (구매창 표시)
    public string seedName; //현재 구매할 씨앗 이름
    public int totalMoney;

    public Button plusBtn;
    public Button minusBtn;
    public Button buyBtn;
    private CoinText coinTextScript;

    private NoticeText notice;

    public GameObject rightSide; // 구매창이 있는 왼쪽부분

    private AudioSource audioSource;
    public AudioClip buySound;
    public AudioClip pushSound;
    public AudioClip notEnoughSound;
    public AudioClip notChosen;

    public Image buyImg;
    public Sprite emptyImage;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        plusBtn.onClick.AddListener(AddBuyNum);
        minusBtn.onClick.AddListener(SubBuyNum);
        buyBtn.onClick.AddListener(buySeed);
        coinTextScript = GameObject.Find("haveMoney").GetComponent<CoinText>();
        notice = GameObject.Find("Notice").GetComponent<NoticeText>();
        audioSource = GameObject.Find("SoundEffect").GetComponent<AudioSource>();
        buyImg = GameObject.Find("seedImage").GetComponent<Image>();

        //처음 자판기 UI 초기화
        ResetUI();



    }



    void OnDisable()
    {
        ResetUI();
    }
    public void ResetUI()
    {
        //처음 자판기 UI 초기화
        seedNameText.text = "선택안함";
        seedName = "none";
        seedMoney = 0;
        totalMoney = 0;
        moneyText.text = 0.ToString() + "원";
        buyNum = 1;
        buyNumText.text = 1.ToString();
        buyImg.sprite = emptyImage;

    }
    void AddBuyNum()
    {
        if (seedName != "none")
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
        else
        {
            notice.WriteMessage("아이템을 선택해주십시오");
            audioSource.clip = notChosen;
            audioSource.Play();
        }
    }

    void SubBuyNum()
    {
        if (seedName != "none")
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
        else
        {
            notice.WriteMessage("아이템을 선택해주십시오");
            audioSource.clip = notChosen;
            audioSource.Play();
        }
    }

    void buySeed()
    {
        if (seedName != "none")
        {
            //잔액이 충분히 있다면, 잔액 감소해서 물품 구매, 감소한 돈 반영
            if (playerScript.money >= totalMoney)
            {
                playerScript.playerMoneyChange(totalMoney, false);
                //인벤에 아이템 추가
                //Debug.Log(totalMoney + "원을 사용하여 " + seedName + " " + buyNum + "개를 구매합니다. 잔액 : " + playerScript.money);
                inven.putInventory(seedName, buyNum);
                audioSource.clip = buySound;
                audioSource.Play();
            }
            else
            {
                //playerScript.playerMoneyChange(totalMoney, false);
                notice.WriteMessage("잔액이 부족합니다.");
                audioSource.clip = notEnoughSound;
                audioSource.Play();
                
            }
        }
        else
        {
            notice.WriteMessage("아이템을 선택해주십시오");
            audioSource.clip =notChosen;
            audioSource.Play();
        }
    }

   
}
