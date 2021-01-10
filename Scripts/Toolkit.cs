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
        toolText.gameObject.SetActive(false);
    }

    public void GenerateToolTip(Item item)
    {
        string statText = "";
        if(item.stats.Count > 0)
        {
            foreach(var stat in item.stats)
            {
                statText += stat.Key.ToString() + " : " + stat.Value.ToString() + "\n";
            }
            string tooltip = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>", item.name, item.description, statText);
            toolText.text = tooltip;
            toolText.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
    }
}
