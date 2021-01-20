using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderRotates : MonoBehaviour
{
    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    
    public Animator anim;
    public float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
            
            
            anim = GetComponent<Animator>();
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))//마우스 클릭중에는 낫이 돌아가는 애니메이션 재생.
        {
            anim.SetBool("GrinderOn", true);
            
        }
        else//마우스 클릭 안 하면 애니메이션 재생 안 함.
            anim.SetBool("GrinderOn", false);
    }

    
}
