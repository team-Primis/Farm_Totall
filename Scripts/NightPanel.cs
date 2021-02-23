using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightPanel : MonoBehaviour
{
    private Image backgroundImage;
    private GameManager GMScript;
    public Image WeatherIcon;

    void Start()
    {
        backgroundImage = gameObject.GetComponent<Image>();
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        WeatherIcon = GameObject.Find("Image").GetComponent<Image>();
    }

    void Update()
    {
        //밤 7시~ 새벽 5시까지는 어둡다.
        if(GMScript.minute >= 24 || GMScript.minute <= 3)
        {
            backgroundImage.color = new Color(0, 0, 0, 0.5f);
            WeatherIcon = Resources.Load<Image>("Farm_Totall/UI/UIReal_12");

        }
        else if (GMScript.minute >= 19 || GMScript.minute <=5)
        {
            backgroundImage.color = new Color(0, 0, 0, 0.3f);
        }
 
        else
        {
            backgroundImage.color = new Color(0,0,0,0);
        }
    }
}
