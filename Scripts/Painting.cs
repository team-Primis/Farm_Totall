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

        GMscript.isTimerStoped = true;
        GMscript.isSleepOpen = true;
        canvasPaint.SetActive(true);
        canvass.SetActive(false);
        StartCoroutine(Flickering());
        Invoke("cancelFlickering", 2.5f);
        Invoke("wokeUp", 3f);
        
        GMscript.timer += 60 * GMscript.speedUp;
        stM.curHp = stM.maxHp / 4;


    }

    public void wokeUp()
    {
        stM.zeroHp = false;
        GMscript.isTimerStoped = false;
        GMscript.isSleepOpen = false;
        canvass.SetActive(true);
    }

    public IEnumerator Flickering()
    {
        
            imagePaint.color = Color.black;
            yield return new WaitForSeconds(1f);
            imagePaint.color = new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(1f);
        

    }

    public void cancelFlickering()
    {
       imagePaint.color = new Color(0, 0, 0, 0.5f);
        canvasPaint.SetActive(false);
    }
}
