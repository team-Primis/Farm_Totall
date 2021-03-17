using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Painting : MonoBehaviour
{
    public Stemina stM;
    public GameManager GMscript;
    public GameObject canvasPaint;
    public Image imagePaint;
    public GameObject canvass;
    public float thisTime;
    public float wantedPaintTime=2f;
    public bool plantIsGrowing = false;
    // Start is called before the first frame update
    void Start()
    {
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        GMscript= GameObject.Find("GameManager").GetComponent<GameManager>();
        imagePaint = GameObject.Find("Canvasspainted").transform.Find("PainLoading").GetComponent<Image>();
        canvasPaint = GameObject.Find("Canvasspainted").transform.Find("PainLoading").gameObject;
        canvass = GameObject.Find("Canvas2").gameObject;//게임 내 유아이 
       
    }

    // Update is called once per frame
    void Update()
    {
        if(stM.zeroHp==true)
        {
            
            hpIsZero();
        }
    }

    public void hpIsZero()
    {
        stM.zeroHp = false;
        GMscript.isTimerStoped = true;
        GMscript.isSleepOpen = true;
      
        canvasPaint.SetActive(true);
        StartCoroutine(Flickering());
        canvass.SetActive(false);
        
        thisTime = GMscript.timer;
        thisTime += wantedPaintTime * 60 * GMscript.speedUp;

        if (thisTime < 60 * 24)
        {
            GMscript.timer = thisTime;
        }
        else if (thisTime >= 60 * 24)
        {
            GMscript.timer = (thisTime - 60 * 24);
            GMscript.day += 1;

        }
        
       
        plantIsGrowing = true;
        Invoke("wokeUp", 3f);
        stM.curHp = stM.maxHp / 4;


    }

    public void wokeUp()
    {
        
        GMscript.isTimerStoped = false;
        GMscript.isSleepOpen = false;
        canvass.SetActive(true);
        canvasPaint.SetActive(false);
       
    }

    public IEnumerator Flickering()
    {
        for(int i=0; i<3; i++)
        {
            imagePaint.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            imagePaint.color = new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        
    }

 
}
