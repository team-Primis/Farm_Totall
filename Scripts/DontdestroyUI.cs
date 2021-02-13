using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontdestroyUI : MonoBehaviour
{
    public static DontdestroyUI instance = null;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
