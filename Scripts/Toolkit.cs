using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolkit : MonoBehaviour
{
    private Text toolText;
    // Start is called before the first frame update
    void Start()
    {
        toolText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
        toolText.gameObject.SetActive(false);
    }

    public void GenerateToolTip(Item item)
    {
        string statText = "";
        if (item != null &&item.Ename!= "" && item.stats.Count > 0)
        {
            //stat 전부 보여주는 코드

            foreach (var stat in item.stats)
            {
                statText = "물 주기 : ";
                if (stat.Key == "watering")
                    statText += stat.Value.ToString();
            }
        }
            //<b></b> 하면 이 사이는 진한 글씨! 일종의 HTML임
            string tooltip = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>", item.Kname, item.description, statText);
            toolText.text = tooltip;
            toolText.gameObject.SetActive(true);
            gameObject.SetActive(true);
        
    }
}
