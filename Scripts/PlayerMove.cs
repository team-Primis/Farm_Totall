using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관련 (저장 때문에)

public class PlayerMove : MonoBehaviour
{

    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    private BoxCollider2D boxCollider;
    //충돌할 때 통과 불가능한 레이어를 설정해줌.
    public Animator anim;
    public float speed = 1.0f;
    static public PlayerMove instance;//static: 이 스크립트를 사용하는 객체는 instance 변수를 공유하게 됨.
    public inventory Inven;
    public GameManager GMScript;
    public float movingSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        if (instance == null)
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

        currentMapName = SceneManager.GetActiveScene().name; // 저장 때문에,,, (성현)
    }

    // Update is called once per frame
    void Update()
    {
        // from menucontrol - 일시정지 창 떠있으면 플레이어 정지
        if (GMScript.isMenuOpen == false && GMScript.isSleepOpen == false)
        {
            Move();
        }

        Watering();
    }

    void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;


        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("BackWalk", true);
            moveZ += movingSpeed;

        }
        else
            anim.SetBool("BackWalk", false);


        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("FrontWalk", true);
            moveZ -= movingSpeed;

        }
        else
            anim.SetBool("FrontWalk", false);



        if (Input.GetKey(KeyCode.A))
        {

            anim.SetBool("LeftWalk", true);
            moveX -= movingSpeed;

        }
        else
            anim.SetBool("LeftWalk", false);

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("RightWalk", true);
            moveX += movingSpeed;

        }
        else
            anim.SetBool("RightWalk", false);


        transform.Translate(new Vector2(moveX, moveZ) * Time.deltaTime * speed);




    }

    void Watering()//물 주는 모션.
    {

        anim.SetBool("Watering", false);
        Vector2 theplayerPosition = this.transform.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;


        if (Inven.equipedItem != null)
        {
            if (Inven.equipedItem.Ename == "waterSprinkle")
            {
                if (Input.GetMouseButton(0))//클릭하면 물 주는 애니메이션 재생.
                {
                    anim.SetBool("Watering", true);

                }
            }

        }
    }
}
