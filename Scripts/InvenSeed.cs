using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InvenSeed : MonoBehaviour
{
    public Button btn;
    public int num=0; //씨앗 수
    public TMP_Text numText;
    PlayerControll playerScript;
    public bool seedNumChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        btn.onClick.AddListener(haveSeed);    
    }

    // Update is called once per frame
    void Update()
    {
        changeSeedText();
        
    }
    void changeSeedText()
    {
        if (seedNumChanged) {
            seedNumChanged = false;
            numText.text = num.ToString();
        }
    }
    void haveSeed()
    {
        if (num > 0)
        {
            playerScript.item = gameObject.tag;
        }
        else {
            Debug.Log("you don't have this");
        }
    }
}
