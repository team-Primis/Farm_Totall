using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine2 : MonoBehaviour
{
    private PlayerControll playerScript;
    public inventory inven;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControll>();


       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            inven.putInventory(0);
        }
        
    }
}
