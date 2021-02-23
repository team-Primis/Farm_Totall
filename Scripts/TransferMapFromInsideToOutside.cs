﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMapFromInsideToOutside : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름.
    private PlayerMove thePlayer;
    // Start is called before the first frame update


    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.name == "Player")
            {
                thePlayer.currentMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
                thePlayer.transform.position = new Vector2(24, 8);
            }
        
    }
}

