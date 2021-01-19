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

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();

      
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
