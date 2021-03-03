using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWater : MonoBehaviour
{
    public GameManager GMscript;
    // Start is called before the first frame update
    void Start()
    {
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GMscript.timer >=7*60+10 )
        {
            Destroy(this.gameObject);
        }
    }
}
