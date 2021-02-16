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
        
        string costText = "";
        string tooltip;
        if (item != null &&item.Ename!= ""&&item.stats.Count>0&&item.stats.ContainsKey("sellingPrice") )
       
        {
            costText += "판매가는 ";
            //stat 전부 보여주는 코드

            foreach (var stat in item.stats)
            {
                if (stat.Key == "sellingPrice")
                    costText += stat.Value.ToString();
            }
            costText += "원 이다.";
             tooltip = string.Format("<b>{0}</b>\n\n{1}\n\n{2}", item.Kname, item.description, costText);

        }
        else
        {
            tooltip = string.Format("<b>{0}</b>\n\n{1}", item.Kname, item.description);

        }

        //<b></b> 하면 이 사이는 진한 글씨! 일종의 HTML임
        toolText.text = tooltip;
        toolText.gameObject.SetActive(true);
        gameObject.SetActive(true);

    }
}
