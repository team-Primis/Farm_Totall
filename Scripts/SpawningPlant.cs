using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPlant : MonoBehaviour//식물 spawning하는 클래스
    //!!!!!!!게임이 일시정지 아닐 때만 동작하게 설정해두기!!!!!!!
{
    public GameObject[] PlantPrefabs;//배열로 구현하여 인덱스가 0일 때 꽃, 1일 때 호박을 심도록 구현.
    public GameObject thePlayer;//플레이어 가져옴.
    public inventory Inven;//인벤토리.
    public Stemina stM;//스태미나 스크립트.
    public GameObject dontDestroy;//미해가 만들어둔 돈디스트로이오브젝트.
    public static SpawningPlant instance = null;//이건 싱글톤 기반용.
    public bool GoIn = false;//집에 들어갈 때 확인하는 bool.
    public bool GoOut = false;//집에서 나올 때 확인하는  bool.
    public List<GameObject> createdPlant = new List<GameObject>();//생성한 식물을 넣어둘 리스트.
    public List<float> PlantXp = new List<float>();//생성한 식물의 x 좌표값을 넣을 리스트.
    public List<float> PlantYp = new List<float>();//생성한 식물의 y 좌표값을 넣을 리스트.
    public GameManager GMscript;//게임매니져.

    public AudioClip plantingPlant;//수확할 때 들리는 소리.
    // Start is called before the first frame update
    void Start()
    {
        Inven = GameObject.Find("Inventory").GetComponent<inventory>();
        //By미해 ( 이거 없으면 건물 안으로 들어왔다 나가면 null pointer 에러)
        thePlayer = GameObject.Find("Player").gameObject;
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        dontDestroy = GameObject.Find("DonDestroyGameObject").gameObject;
        GMscript = GameObject.Find("GameManager").GetComponent<GameManager>();
        
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
        
        if(GMscript.isMenuOpen == false && GMscript.isWillSellOpen == false && GMscript.isSleepOpen == false || GMscript.isBuyOpen)//일시정지창, 구매창, 잠자기 창이 모두 열려 있지 않을 때만 작동.
        {
            if (Inven.equipedItem.Ename != "empty")//장비 칸의 아이템 이름이 "empty"가 아닌 경우
            {
                if (Inven.equipedItem.Ename == "blueFlowerSeed")//장비칸 이름이 파란꽃 씨앗이면 스포닝플랜트 함수로 파란꽃을 생성.
                { SpawnPlant(PlantPrefabs[0]); }
                if (Inven.equipedItem.Ename == "pumpkinSeed")//장비칸 이름이 호박 씨앗이면 스포닝플랜트 함수로 호박을 생성.
                { SpawnPlant(PlantPrefabs[1]); }
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

    void SpawnPlant(GameObject PlantPrefabs)//식물 심는 함수.
    {

        Vector2 theplayerPosition = thePlayer.transform.position;//플레이어의 위치를 선언.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//게임플레이화면에서의 마우스 위치를 게임에디터에서의 Vector2 타입의 마우스 위치에 배정.

        Vector2 themousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));//타일 크기마다 이동하는 것처럼 보이기 위해 올림하여 마우스 위치 재설정.
        Vector2 distance = theplayerPosition - mousePosition;//플레이어와 마우스 사이의 거리 선언.


        if (Input.GetMouseButtonDown(0))//마우스 왼클릭 시
        {
            //Debug.Log(distance);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//카메라에서 레이저를 스크린상에서의 마우스 위치에서 발사함.
            RaycastHit2D hit1 = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("PlantLayer"));//이거 아마 마우스 위치에서/카메라가 보고 있는 방향으로/길이 무한/"플랜트레이어" 레이어에 있는 오브젝트만 인식하는 레이져 쏘는 거일걸.
            RaycastHit2D hit2 = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("DarkDirt"));//이거 아마 마우스 위치에서/카메라가 보고 있는 방향으로/길이 무한/"다크덜트" 레이어에 있는 오브젝트만 인식하는 레이져 쏘는 거일걸.
            if (hit1.collider == null)//만약 플랜트레이어에 있는 오브젝트만 인식하는 레이저가 어디에 맞았는데 비었고
           
            {
                if(hit2.collider != null)//다크덜트 레이어에 있는 오브젝트만 인식하는 레어저가 어디에 맞았는데
                {
                    if (hit2.collider.CompareTag("DarkDirt"))//그 물체가 어두운 흙이면
                    {
                        if (Mathf.Abs(distance.x) <= 1.5f && Mathf.Abs(distance.y) <= 2f)//플레이어의 위치를 기준으로 x 거리는 타일 1.5칸, y 거리는 타일 2칸 이하에서
                        {

                            GameObject PlantedPlant = Instantiate(PlantPrefabs);//식물 생성.
                            PlantedPlant.transform.parent = dontDestroy.transform;//식물을 돈디스트로이 오브젝트의 자식으로 배치해줌.
                            PlantedPlant.transform.position = themousePosition;//생성한 식물을 마우스 위치와 같은 곳에 배치함.
                            createdPlant.Add(PlantedPlant);//리스트에 추가.
                            PlantXp.Add(PlantedPlant.transform.position.x);//좌표값 저장하는 리스트에 추가.
                            PlantYp.Add(PlantedPlant.transform.position.y);//좌표값 저장하는 리스트에 추가.
                            Inven.UseItem();//인벤에서 아이템 사용하는 함수로 씨앗을 써줌.
                            SoundManager.instance.SFXPlay("Planting", plantingPlant);//심을 때 소리나게 해줌.
                            stM.UseHp(4f);//스태미나 소모.
                        }
                    }
                



                }
            }

                            
                   



        }
    }

    public void mergePosition()//집안으로 들어갈 때 식물 위치를 집안에서 보이지 않게 해주는 함수.
    {
        if (createdPlant.Count > 0)//스포닝플랜트 함수에서 저장한 리스트의 길이가 0 초과일 때
        {
            for (int i = 0; i < createdPlant.Count; i++)//그 길이만큼 반복
            {
                createdPlant[i].transform.position = new Vector2(30f, -16f);//리스트 내 모든 식물의 위치를 한 곳으로 모아줌.
            }
        }
    }

    public void backPosition()//집밖으로 나갈 때 식물 위치를 다시 원래대로 배치해주는 함수.
    {
        if (createdPlant.Count > 0)//스포닝플랜트 함수에서 저장한 리스트의 길이가 0 초과일 때
        {
            for (int i = 0; i < createdPlant.Count; i++)//그 길이만큼 반복
            {
                createdPlant[i].transform.position = new Vector2(PlantXp[i], PlantYp[i]);//리스트 내 모든 식물의 위치를 원래 위치로 되돌려줌.
            }
        }
    }
}