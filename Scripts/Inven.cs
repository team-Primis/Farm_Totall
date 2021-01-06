using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven : MonoBehaviour
{
    public Button pumpkinBtn;
    public Button blueFBtn;
    public Button potatoBtn;
    public Button sweetPBtn;

    public Button sickleBtn;
    public Button waterSprinkBtn;

    PlayerControll playerScript;

    // Start is called before the first frame update
    void Start()
    {
        sickleBtn.onClick.AddListener(changeToSickle);
        waterSprinkBtn.onClick.AddListener(changeToWaterSp);
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeToSickle() {
        playerScript.item = "sickle";
    }
    void changeToWaterSp() {
        playerScript.item = "waterSprinkler";
    }
   
}
