using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIIcon : MonoBehaviour
{
    private GameManager GMscript;

    void Start()
    {
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
