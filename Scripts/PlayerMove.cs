using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관련 (저장 때문에)

public class PlayerMove : MonoBehaviour
{

    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.
    private BoxCollider2D boxCollider;//박스콜라이더.
    //충돌할 때 통과 불가능한 레이어를 설정해줌.
    public Animator anim;//애니메이터.
    public float speed = 1.0f;//움직이는 속도 조정하려고 가져옴.
    static public PlayerMove instance;//static: 이 스크립트를 사용하는 객체는 instance 변수를 공유하게 됨.
    public inventory Inven;//인벤토리.
    public GameManager GMScript;//게임매니져.
    public float movingSpeed = 1f;//움직이는 속도 조정하려고 가져옴.
    public AudioSource audioSource;//플레이어에 오디오 소스 부착해둠.
    public AudioClip walkingSound_OutSide;//밖에서 걷는 소리
    public AudioClip walkingSound_Inside;//안에서 걷는 소리.
    public bool setMoveSound = false;//움직이는 소리 키는 용도의 bool.

    // Start is called before the first frame update
    void Start()
    {
        GMScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        audioSource = GetComponent<AudioSource>();
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
        // 일시정지창, 닭판매창, 닭구매창(0324), 자는화면, 로딩화면 떠있으면 플레이어 정지
        // (0402 성현) SleepLoading 관련 조건 추가 (찐 자는 화면)
        // (0402 성현) isSleepOpen은 기절 및 잠 선택창 관련
        // (0402 성현) Title 화면에서도 플레이어 정지
        if(GMScript.isMenuOpen == false && GMScript.isWillSellOpen == false && GMScript.isBuyOpen == false
                        && GMScript.isSleepOpen == false && GMScript.isLoadingOpen == false
                        && GameObject.Find("Canvassleep").transform.Find("SleepLoading").gameObject.activeSelf == false
                        && SceneManager.GetActiveScene().name != "Title")
        {
            Move();//플레이어 이동하는 함수.
            MovingSFX();//이동할 때 음악 재생.
        }
        else
        {
            audioSource.Stop();//일시정지 창이나 씬 로딩 등에서도 자꾸 소리가 재생되길래 아예 소리를 꺼놓고 조건을 만족할 때에만 재생하도록 해둠.
            anim.SetBool("BackWalk", false);
            anim.SetBool("FrontWalk", false);
            anim.SetBool("LeftWalk", false);
            anim.SetBool("RightWalk", false);
        }
        Watering();//물 주는 모션인데 인게임에선.... 안 보여...
    }

    void Move()//플레이어 움직이게 하는 함수.
    {
        float moveX = 0f;//초기화.
        float moveZ = 0f;//초기화.

    
        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            setMoveSound = true;//움직일 때 소리 켬.
        }
        else
        {
            setMoveSound = false;//아닌 경우에는 소리 끔.
        }
       
        if (Input.GetKey(KeyCode.W))//입력이 W이면
        {
            
            anim.SetBool("BackWalk", true);//위로 걷는 애니메이션 재생
            moveZ += movingSpeed;//위로 좌표값 증가.

        }
        else//아닌 경우
        {
            
            anim.SetBool("BackWalk", false);//위로 걷는 애니메이션 중단.
        }
            


        if (Input.GetKey(KeyCode.S))//입력이 S인 경우
        {
            
            
            anim.SetBool("FrontWalk", true);//아래로 걷는 애니메이션 재생.
            moveZ -= movingSpeed;//좌표값 위로 감소==아래로 증가.


        }
        else//아닌 경우
        {
            
            anim.SetBool("FrontWalk", false);//아래로 걷는 애니메이션 중단.
        }
            



        if (Input.GetKey(KeyCode.A))//입력이 A인 경우
        {
            
            anim.SetBool("LeftWalk", true);//왼쪽으로 걷는 애니메이션 재생.
            moveX -= movingSpeed;//왼쪽으로 좌표 증가==오른쪽으로 좌표 감소.


        }
        else//아닌 경우
        {
     
            anim.SetBool("LeftWalk", false);//왼쪽으로 걷는 애니메이션 중단.
        }
            

        if (Input.GetKey(KeyCode.D))//입력이 D인 경우
        {
           
            anim.SetBool("RightWalk", true);//오른쪽으로 걷는 애니메이션 재생.
            moveX += movingSpeed;//오른쪽으로 좌표값 증가.

        }
        else//아닌 경우
        {
            
            anim.SetBool("RightWalk", false);//오른쪽으로 걷는 애니메이션 중단.
        }
   
        transform.Translate(new Vector2(moveX, moveZ) * Time.deltaTime * speed);//플레이어의 좌표값 변경. 스피드 변수랑 타임 델타타임으로 속도 조정.

    }

    public void MovingSFX()//걸을 때 소리 재생하는 함수.
    {
        if (SceneManager.GetActiveScene().name == "OutSide")//집밖에선 흙 밟는 소리남.

        {

            audioSource.clip = walkingSound_OutSide;
        }

        else if (SceneManager.GetActiveScene().name == "Inside")//집안에선 바닥 밟는 소리남.
        {
            audioSource.clip = walkingSound_Inside;
        }
        if (setMoveSound==true)//이 bool이 트루일 때
        {
    

            if(!audioSource.isPlaying)//플레이어에 부착된 오디오 소스에서 소리가 재생되지 않는 경우면
            audioSource.Play();//걷는 소리 재생.
  
        }
        else//bool이 펄스일 때
        {
            audioSource.Stop();//걷는 소리 중단.
        }
    }

 

    void Watering()//물 주는 모션.
    {

        anim.SetBool("Watering", false);//기본값은 물 주는 애니메이션 중단.
        Vector2 theplayerPosition = this.transform.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 게임에디터에서의 Vector2 타입의 마우스 위치에 배정.

        if (Inven.equipedItem != null)//장비한 아이템이 빈칸이 아니고
        {
            if (Inven.equipedItem.Ename == "waterSprinkle")//물뿌리개를 장착한 경우
            {
                if (Input.GetMouseButton(0))//클릭하면 물 주는 애니메이션 재생.
                {
                    anim.SetBool("Watering", true);

                }
            }

        }
    }
}
