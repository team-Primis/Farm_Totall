using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine2 : MonoBehaviour
{
    private PlayerControll playerScript;
    private inventory inven;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        inven = GameObject.Find("Inventory").GetComponent<inventory>();


        //반영 안되고 있음
        inven.putInventory(1);
        inven.putInventory(2);
        inven.RemoveItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
