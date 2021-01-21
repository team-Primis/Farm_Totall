using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLoad : MonoBehaviour
{
    
    public float timer;
    public Animator anim;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //식물이 심어졌을 때만 실행되도록 if문 설정
        
            timer += Time.deltaTime;

            if (timer >= 6)
            {
            i++;
            anim.SetInteger("One", i);
                
                timer = 0;
            }


        
    }
}
