using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SpawningDirt : MonoBehaviour
{
    public GameObject DirtPrefab;//파낸 검은 흙 오브젝트 필드 설정.
    public GameObject thePlayer;
    public inventory Inven;
    public Stemina stM;
    public int number=0;
    public List<float> DarkDirtXp = new List<float>();
    public List<float> DarkDirtYp = new List<float>();
    public int nDarkDirt = 0;
    public List<GameObject> createdDarkDirt=new List<GameObject>();
    public static SpawningDirt instance = null;
    public bool GoIn=false;
    public bool GoOut = false;
    public GameObject dontDestroy;
    public GameManager GMscript;
    // Start is called before the first frame update
    private void Awake()
    {

        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        thePlayer = GameObject.Find("Player").gameObject;
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        dontDestroy = GameObject.Find("DonDestroyGameObject").gameObject;
        GMscript=GameObject.Find("GameManager").GetComponent<GameManager>();

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

    // Update is called once per frame


 void Update()
    {
      if(GMscript.isMenuOpen == false && GMscript.isWillSellOpen == false && GMscript.isSleepOpen == false)
        {
            if (SceneManager.GetActiveScene().name == "OutSide")
            {
                SpawnDirt();
            }
        }
       
        if (GoIn)
        {
            GoIn = false;
            mergePosition();
        }

        if (GoOut)
        {
            GoOut = false;
            backPosition();
        }

    }
   public void SpawnDirt()
    {
        Vector2 theplayerPosition = thePlayer.transform.position;//게임플레이화면에서의 마우스 위치를 Vector2 타입의 마우스 위치에 배정.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (Inven.equipedItem != null)//(Inven.equipedItem.Ename != "empty")로 하니까 눌레퍼런스 그거 뜨길래 다시 바꿈.
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (Inven.equipedItem.Ename == "sickle")
                {


                    if (hit.collider == null || hit.collider.CompareTag("Player"))
                    {

                        if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//마우스 왼클릭을 하는 중에는
                        {

                            GameObject DarkDirt = Instantiate(DirtPrefab);//식물 생성
                            DarkDirt.transform.parent = dontDestroy.transform;
                            DarkDirt.transform.position = themousePosition;//생성한 식물을 마우스 위치와 같은 곳에 배치함.

                            createdDarkDirt.Add(DarkDirt);
                            DarkDirtXp.Add(DarkDirt.transform.position.x);
                            DarkDirtYp.Add(DarkDirt.transform.position.y);
                            stM.UseHp(7f);
                        }

                    }
                }

                if(Inven.equipedItem.Ename=="Hoe")
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("DarkDirt"))
                        {
                            GameObject responsedDarkDirt = hit.collider.gameObject;
                            Destroy(responsedDarkDirt);
                            createdDarkDirt.Remove(responsedDarkDirt);
                            DarkDirtXp.Remove(responsedDarkDirt.transform.position.x);
                            DarkDirtYp.Remove(responsedDarkDirt.transform.position.y);
                            stM.UseHp(5f);
                        }
                    }
                }
                   
             
                
            }

        }
    }

   public void mergePosition()
    {
        if(createdDarkDirt.Count>0)
        {
            for(int i=0; i<createdDarkDirt.Count; i++)
            {
                createdDarkDirt[i].transform.position=new Vector2(20f, -8f);
            }
        }
    }

    public void backPosition()
    {
        if(createdDarkDirt.Count>0)
        {
            for(int i=0; i<createdDarkDirt.Count;i++)
            {
                createdDarkDirt[i].transform.position = new Vector2(DarkDirtXp[i], DarkDirtYp[i]);
            }
        }
    }
}