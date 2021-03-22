using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SpawningDirt : MonoBehaviour
{
    public GameObject DirtPrefab;//파낸 검은 흙 오브젝트 필드 설정.
    public GameObject thePlayer;//플레이어 가져옴.
    public inventory Inven;//인벤토리.
    public Stemina stM;//스태미나.
    public List<float> DarkDirtXp = new List<float>();//흙 x 좌표 넣을 리스트.
    public List<float> DarkDirtYp = new List<float>();//흙 y 좌표 넣을 리스트.
    public List<GameObject> createdDarkDirt=new List<GameObject>();//만든 흙 저장할 게임오브젝트 리스트.
    public static SpawningDirt instance = null;//싱글톤 기반용.
    public bool GoIn=false;//집안으로 들어갈 때 사용할 bool.
    public bool GoOut = false;//집밖으로 나갈 때 사용할 bool.
    public GameObject dontDestroy;//미해가 만들어둔 돈디스트로이용 오브젝트.
    public GameManager GMscript;//게임매니져.
   
    public AudioClip grindingDirt;//흙 파는 소리.
    // Start is called before the first frame update
    private void Awake()
    {

        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        thePlayer = GameObject.Find("Player").gameObject;
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        dontDestroy = GameObject.Find("DonDestroyGameObject").gameObject;
        GMscript=GameObject.Find("GameManager").GetComponent<GameManager>();

        if (instance == null)//스태틱 인스턴스가 눌일 때는
        {
            DontDestroyOnLoad(this.gameObject);//씬 이동 시에도 파괴하지 않음.
            instance = this;//그 이후 씬 이동을 통해 생성되는 모든 복사본의 인스턴스는 눌임.
        }
        else if (instance != this)//씬 이동을 통해 생성되는 모든  복사본의 인스턴스는 this가 아니므로
        {
            Destroy(this.gameObject);//씬 이동 시 없애줌.
        }

    }

    // Update is called once per frame


 void Update()
    {
      if(GMscript.isMenuOpen == false && GMscript.isWillSellOpen == false && GMscript.isSleepOpen == false)//일시정지창, 구매창, 잠자기 창이 모두 열려 있지 않을 때만 작동.
        {
            if (SceneManager.GetActiveScene().name == "OutSide")//현재 씬이 집 밖일 때만 작동.
            {
                SpawnDirt();//흙 생성 함수.
            }
        }
       
        if (GoIn)//집안으로 들어가는 경우
        {
            GoIn = false;//얘는 다시 펄스로 만들어주고
            mergePosition();//돈디스트로이에 Child로 만들어준 생성된 식물들을 집안 씬에서 눈에 안 들어오는 곳에 배정함.
        }

        if (GoOut)//집밖으로 나가는 경우
        {
            GoOut = false;//얘는 다시 펄스로 만들어주고
            backPosition();//돈디스트로이에 Child로 만들어준 생성된 식물들을 원래 심어둔 자리로 배치함.
        }

    }
   public void SpawnDirt()//흙 생성하는 함수.
    {
        Vector2 theplayerPosition = thePlayer.transform.position;//플레이어의 위치를 선언.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 게임에디터에서의 Vector2 타입의 마우스 위치에 배정.

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;//플레이어와 마우스 사이의 거리 선언.

        if (Input.GetMouseButtonDown(0))//마우스 왼클릭 시
        {
            if (Inven.equipedItem != null)//(Inven.equipedItem.Ename != "empty")로 하니까 눌레퍼런스 그거 뜨길래 다시 바꿈.
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);//이거 아마 마우스 위치에서/카메라가 보고 있는 방향으로/길이 무한인 레이져 쏘는 거일걸.
                if (Inven.equipedItem.Ename == "sickle")//장착한 아이템이 낫이고 
                {


                    if (hit.collider == null || hit.collider.CompareTag("Player"))//레이져에 맞은 오브젝트가 없거나 플레이어인 경우
                    {

                        if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//플레이어의 위치를 기준으로 x 거리는 타일 1.5칸, y 거리는 타일 2칸 이하에서
                        {

                            GameObject DarkDirt = Instantiate(DirtPrefab);//흙 생성.
                            DarkDirt.transform.parent = dontDestroy.transform;//돈디스트로이 오브젝트에 자식으로 배치함.
                            DarkDirt.transform.position = themousePosition;//생성한 흙을 마우스 위치와 같은 곳에 배치함.

                            createdDarkDirt.Add(DarkDirt);//생성한 흙을 리스트에 추가함.
                            DarkDirtXp.Add(DarkDirt.transform.position.x);//생성한 흙의 좌표값을 리스트에 추가함.
                            DarkDirtYp.Add(DarkDirt.transform.position.y);//생성한 흙의 좌표값을 리스트에 추가함.
                            SoundManager.instance.SFXPlay("Planting", grindingDirt);//흙 파는 소리 재생.
                            stM.UseHp(7f);//스태미나 소모.
                        }

                    }
                }

                if(Inven.equipedItem.Ename=="Hoe")//만약 장착한 아이템이 호미고
                {
                    if (hit.collider != null)//레이져에 맞은 아이템이
                    {
                        if (hit.collider.CompareTag("DarkDirt"))//어두운 흙이면
                        {
                            GameObject responsedDarkDirt = hit.collider.gameObject;//일단 레이져에 맞은 오브젝트를 게임오브젝트로 선언해주고
                            Destroy(responsedDarkDirt);//그 오브젝트를 없애준 뒤
                            createdDarkDirt.Remove(responsedDarkDirt);//리스트에서도 삭제.
                            DarkDirtXp.Remove(responsedDarkDirt.transform.position.x);//좌표 리스트에서도 삭제.
                            DarkDirtYp.Remove(responsedDarkDirt.transform.position.y);
                            SoundManager.instance.SFXPlay("Planting", grindingDirt);//흙 없애는 소리 재생.
                            stM.UseHp(5f);//스태미나 소모.
                        }
                    }
                }
                   
             
                
            }

        }
    }

   public void mergePosition()//집안으로 들어갈 때 식물 위치를 집안에서 보이지 않게 해주는 함수.
    {
        if(createdDarkDirt.Count>0)//스포닝플랜트 함수에서 저장한 리스트의 길이가 0 초과일 때
        {
            for(int i=0; i<createdDarkDirt.Count; i++)//그 길이만큼 반복
            {
                createdDarkDirt[i].transform.position=new Vector2(20f, -8f);//리스트 내 모든 식물의 위치를 한 곳으로 모아줌.
            }
        }
    }

    public void backPosition()//집밖으로 나갈 때 식물 위치를 다시 원래대로 배치해주는 함수.
    {
        if(createdDarkDirt.Count>0)//스포닝플랜트 함수에서 저장한 리스트의 길이가 0 초과일 때
        {
            for(int i=0; i<createdDarkDirt.Count;i++)//그 길이만큼 반복
            {
                createdDarkDirt[i].transform.position = new Vector2(DarkDirtXp[i], DarkDirtYp[i]);//리스트 내 모든 식물의 위치를 원래 위치로 되돌려줌.
            }
        }
    }
}