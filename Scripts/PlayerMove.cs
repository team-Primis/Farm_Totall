using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    private BoxCollider2D boxCollider;
    //충돌할 때 통과 불가능한 레이어를 설정해줌.
   public Animator anim;
    public float speed = 1.0f;
    static public PlayerMove instance;//static: 이 스크립트를 사용하는 객체는 instance 변수를 공유하게 됨.

    GameManager GMScript;
  
    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(instance==null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            anim = GetComponent<Animator>();
            instance = this;//처음 생성된 경우에만 instance의 값이 Null이다. 생성된 이후에 this 값을 주었기 때문. 그러고 나서 해당 스크립트가 적용된 객체가 떠 생성될 경우, static으로 값을 공유한 instance의 값이 this이기 때문에 그 객체는 삭제됨.
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GMScript.isBuyOpen == false) // from playercontoller - 닭구매창 떠있으면 플레이어 정지
        {
            Move();
        }
    }

    void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;

    
        if (Input.GetKey(KeyCode.W))
        {
            
            anim.SetBool("BackWalk", true);
            moveZ += 1f;
            
        }
        else
            anim.SetBool("BackWalk", false);


        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("FrontWalk", true);
            moveZ -= 1f;
            
        }
        else
            anim.SetBool("FrontWalk", false);

       

        if (Input.GetKey(KeyCode.A))
        {
            
            anim.SetBool("LeftWalk", true); 
            moveX -= 1f;
            
        }
        else
            anim.SetBool("LeftWalk", false);

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("RightWalk", true);
            moveX += 1f;
           
        }
        else
            anim.SetBool("RightWalk", false);

      
        transform.Translate(new Vector2(moveX, moveZ) * Time.deltaTime * speed);

        


    }
}
