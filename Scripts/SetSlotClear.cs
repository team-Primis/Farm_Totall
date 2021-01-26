using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSlotClear : MonoBehaviour
{
    private inventory Inven;
    // Start is called before the first frame update
    void Start()
    {
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Inven.equipedItem == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
    }

    
}
