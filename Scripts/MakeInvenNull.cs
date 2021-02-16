using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvenNull : MonoBehaviour
{
    public inventory inven;

    void Start()
    {
        inven = GameObject.Find("Inventory").GetComponent<inventory>();    
    }

    

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("player touched");
            inven.renewSlot();
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
