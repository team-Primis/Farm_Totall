using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stemina : MonoBehaviour
{

    public Slider hpbar;

    public float maxHp= 200;
    public float curHp = 200;
    // Start is called before the first frame update
    void Start()
    {
        hpbar = GetComponent<Slider>();
        hpbar.value = (float)curHp / (float)maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(curHp >0)
            {
                curHp -= 10;
            }
            else
            {
                curHp = 0;
            }
        }
        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, (float)curHp / (float)maxHp, Time.deltaTime*10);
    }

     
}
