using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvenNull : MonoBehaviour
{
    public inventory inven;
    public ContainerUI containerUI;

    void Start()
    {
        inven = GameObject.Find("Inventory").GetComponent<inventory>();
        containerUI = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerUI>();

       
        

    }

    

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("player touched");
            inven.renewSlot();
        }

        foreach (CUIItem items in containerUI.container)
        {
            if (items.item == null || items.item.Ename == "" ||items.item.count==0)
            {
                Debug.Log("해당 슬롯 초기화");
                items.UpdateItem(null);
            }
        }

    }

    void OnTriggeExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("player touched");
            inven.renewSlot();
        }

    }
}
