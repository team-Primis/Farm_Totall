using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Input Output
using System.Runtime.Serialization.Formatters.Binary; // binary 파일로 변환
using UnityEngine.SceneManagement; // scene 전환

public class SaveNLoad : MonoBehaviour
{
    // 현재 GameManager에 붙어있음
    // 근데 타이틀에도 있어야 함 - gamemanager를 파괴X
    // 단순히 기능만 확인하고 싶으면 CallNewGame의 첫 줄 주석 처리하면 됨!
    // 로딩화면 순서 >로로딩딩<으로 검색 (새게임/로딩1/로딩2)

    // 안내 메세지
    private NoticeText notice;

    // 로딩화면
    private LoadingBar theLoadingBar;

    void Start()
    {
        notice = GameObject.Find("Notice").GetComponent<NoticeText>(); // 안내 메세지
        theLoadingBar = GameObject.Find("Canvas2").transform.Find("Loading").
                                                    GetComponent<LoadingBar>(); // 로딩화면
    }

    public bool doLoadF1 = false;
    public bool doLoadF2 = false;
    public bool doNewGame = false;

     //그리고 합치니까 인게임 ui가 타이틀 화면 위로 올라오길래 canvas의 sortorder을 2로 바꾸었습니당
     //혹시 유니티 상에서 그렇게 뜬다면 바꿔주세용! (미해)

    void Update()
    {
        if(doNewGame == true)
        {
            doNewGame = false;
            Debug.Log("새 게임 로드 시작 - 1");
            Invoke("RealNewGame", 2); // 씬 바꿀 시간 기다림
        }
        else if(doLoadF1 == true)
        {
            doLoadF1 = false;
            Debug.Log("File 1 로드 시작 - 1");
            Invoke("RealLoadF1", 2); // 씬 바꿀 시간 기다림
        }
        else if(doLoadF2 == true)
        {
            doLoadF2 = false;
            Debug.Log("File 2 로드 시작 - 1");
            Invoke("RealLoadF2", 2); // 씬 바꿀 시간 기다림
        }
    }

    private PlayerMove thePlayerMove; // 플레이어 좌표, 씬 이름
    private GameManager theGameManager; // 날짜, 시간
    private Stemina theStemina; // stamina
    private PlayerControll thePlayerControll; // 쓴 돈, laborCount
    private SpawnManager theSpawnManager; // 닭 개수/행복도/checkEgg/좌표, 달걀 개수/좌표
    private inventory theInventory; // 인벤템 ID, 인벤템 개수, 장착템 ID, 장착템 인덱스
    private UIInventory theUIInventory; // 인벤 스프라이트 (for inventory) (+ 인벤 UI 상태: 수정 후 필요X)
    private ContainerUI theContainerUI; // 상자 스프라이트 (for ContainerItems) - UI 업데이트
    private ContainerItems theContainerItems; // 보관템 ID, 보관템 개수
    private MenuControl theMenuControl; // 일시정지 창 꺼놓기 위해서 - Load(+New)에만 사용
    private SpawningDirt theSpawningDirt; // dirt -------------------- load&save&새게임
    private SpawningPlant theSpawningPlant; // plant -------------------- load&save&새게임
    private PlantLoad thePlantLoad; // plant -------------------- load&save 내부

    public SNLData data; // SNLData 이용할 것임

    public void CallNewGame()
    {
        doNewGame = true; // 단순히 기능 확인하고 싶으면 이거 주석처리하면 됨!

        theLoadingBar.ResetLoading(); // 로로딩딩 새게임 - 리셋

        // 새로 시작하기 첫 씬은 OutSide
        Debug.Log("새 게임 씬 로드");
        SceneManager.LoadScene("OutSide");
        // 다른 씬의 것은 참조 불가, 씬 이동 후의 명령어는 실행 안 됨
    }

    public void RealNewGame() // 값 초기화
    {
        thePlayerMove = FindObjectOfType<PlayerMove>();
        theGameManager = FindObjectOfType<GameManager>();
        theStemina = FindObjectOfType<Stemina>();
        thePlayerControll = FindObjectOfType<PlayerControll>();
        theSpawnManager = FindObjectOfType<SpawnManager>();
        theInventory = FindObjectOfType<inventory>();
        theUIInventory = FindObjectOfType<UIInventory>();
        theContainerUI = GameObject.Find("Canvas2").transform.Find("containerPanel").
                                                                        GetComponent<ContainerUI>();
        theContainerItems = FindObjectOfType<ContainerItems>();
        theMenuControl = FindObjectOfType<MenuControl>();
        theSpawningPlant = FindObjectOfType<SpawningPlant>();
        theSpawningDirt = FindObjectOfType<SpawningDirt>();

        Debug.Log("새 게임 로드 시작 - 2");
        theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 새게임 - 1 (~0.2)

        theGameManager.isTimerStoped = true; // 로드할 동안 시간 정지 (맨 아래 참고)

        thePlayerMove.transform.position = new Vector2(0, 0); // 플레이어 좌표 초기화
        
        theGameManager.day = 1; // DAY 초기화
        theGameManager.timer = 420; // 타이머 초기화 (7시)
        
        theStemina.curHp = 200f; // stemina 초기화
        
        int pastMoney = thePlayerControll.money; // 돈 초기화 - 1
        thePlayerControll.playerMoneyChange(10000 - pastMoney, true); // 돈 초기화 - 2
        thePlayerControll.laborCount = 5; // laborCount 초기화
        
        theSpawnManager.chickenList.Clear(); // 닭 목록 초기화
        theSpawnManager.chickenCount = 0; // 닭 개수 초기화
        // ----------------------------------------------
        theSpawnManager.nEggList.Clear(); // 보통 달걀 목록 초기화
        theSpawnManager.nEggCount = 0; // 보통 달걀 개수 초기화
        // ----------------------------------------------
        theSpawnManager.gEggList.Clear(); // 좋은 달걀 목록 초기화
        theSpawnManager.gEggCount = 0; // 좋은 달걀 개수 초기화

        theLoadingBar.loadSlider.value += 0.3f; // 로로딩딩 새게임 - 2 (~0.5)
        
        for(int i = theInventory.characterItems.Count - 1; i >= 0 ; i--)
        {
            theInventory.RemoveAll(theInventory.characterItems[i].id);
        } // 인벤 아이템 비우기 (인벤 초기화)
        theUIInventory.MakeSlotNull(); // 인벤 UI 비우기 (인벤 UI 초기화)
        theInventory.putInventory(100); // 기본 인벤템
        theInventory.putInventory(101); // 기본 인벤템
        theInventory.putInventory(102); // 기본 인벤템 (호미 0316 추가)
        theUIInventory.MakeSlotClear(); // 장착템 UI 초기화
        theInventory.equipedItem = theInventory.emptyItem; // 장착템 초기화

        theLoadingBar.loadSlider.value += 0.3f; // 로로딩딩 새게임 - 3 (~0.8)

        theContainerItems.container.Clear(); // 보관상자 아이템 비우기 (보관상자 초기화)
        theContainerUI.isContainerChanged = true; // 보관상자 UI 업데이트

        // dirt, plant, water (0320 수정)
        for(int i = theSpawningDirt.DarkDirtXp.Count - 1; i >= 0; i--)
        {
            Destroy(theSpawningDirt.createdDarkDirt[i]);
        } // 돈디스트로이된 흙 전부 파괴
        theSpawningDirt.DarkDirtXp.Clear(); // 흙 위치 목록 초기화(x)
        theSpawningDirt.DarkDirtYp.Clear(); // 흙 위치 목록 초기화(y)
        theSpawningDirt.createdDarkDirt.Clear(); // 다시 확인 (흙 목록 초기화)
        // ----------------------------------------------
        for(int i = theSpawningPlant.PlantXp.Count - 1; i >= 0; i--)
        {
            Destroy(theSpawningPlant.createdPlant[i]);
        } // 돈디스트로이된 식물 전부 파괴
        theSpawningPlant.PlantXp.Clear(); // 식물 위치 목록 초기화(x)
        theSpawningPlant.PlantYp.Clear(); // 식물 위치 목록 초기화(y)
        theSpawningPlant.createdPlant.Clear(); // 다시 확인 (식물 목록 초기화)

        theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간 흐르기 시작 (맨 위 참고)

        theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 새게임 - 5 (~1.0)
        Debug.Log("새 게임 로드 완료");
        notice.WriteMessage("새 게임 시작!");
    }


    public void CallSaveF1()
    {
        thePlayerMove = FindObjectOfType<PlayerMove>();
        theGameManager = FindObjectOfType<GameManager>();
        theStemina = FindObjectOfType<Stemina>();
        thePlayerControll = FindObjectOfType<PlayerControll>();
        theSpawnManager = FindObjectOfType<SpawnManager>();
        theInventory = FindObjectOfType<inventory>();
        theUIInventory = FindObjectOfType<UIInventory>();
        theContainerItems = FindObjectOfType<ContainerItems>();
        theSpawningDirt = FindObjectOfType<SpawningDirt>();
        theSpawningPlant = FindObjectOfType<SpawningPlant>();

        data.playerX = thePlayerMove.transform.position.x;
        data.playerY = thePlayerMove.transform.position.y;
        data.sceneName = thePlayerMove.currentMapName;

        data.day = theGameManager.day;
        data.timer = theGameManager.timer;

        data.currentHp = theStemina.curHp;

        data.usedMoney = 10000 - thePlayerControll.money; // 쓴 금액으로 10000은 기본값
        data.laborCount = thePlayerControll.laborCount;

        data.chickenCount = theSpawnManager.chickenCount;
        data.happy.Clear();
        data.checkEgg.Clear();
        data.chickenXP.Clear();
        data.chickenYP.Clear();
        if(thePlayerMove.currentMapName == "OutSide")
        {   
            for(int i = 0; i < theSpawnManager.chickenCount; i++)
            {
                data.happy.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().happy);
                data.checkEgg.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg);
                data.chickenXP.Add(theSpawnManager.chickenList[i].transform.position.x);
                data.chickenYP.Add(theSpawnManager.chickenList[i].transform.position.y);
            }
        }
        else if(thePlayerMove.currentMapName == "Inside")
        {
            for(int i = 0; i < theSpawnManager.chickenCount; i++)
            {
                data.happy.Add(theSpawnManager.chickenHp[i]);
                data.checkEgg.Add(theSpawnManager.chickenCe[i]);
                data.chickenXP.Add(theSpawnManager.chickenXp[i]);
                data.chickenYP.Add(theSpawnManager.chickenYp[i]);
            }
        }
        // ----------------------------------------------
        data.nEggCount = theSpawnManager.nEggCount;
        data.nEggXP.Clear();
        data.nEggYP.Clear();
        if(thePlayerMove.currentMapName == "OutSide")
        {
            for (int i = 0; i < theSpawnManager.nEggCount; i++)
            {
                data.nEggXP.Add(theSpawnManager.nEggList[i].transform.position.x);
                data.nEggYP.Add(theSpawnManager.nEggList[i].transform.position.y);
            }
        }
        else if(thePlayerMove.currentMapName == "Inside")
        {
            for (int i = 0; i < theSpawnManager.nEggCount; i++)
            {
                data.nEggXP.Add(theSpawnManager.neggXp[i]);
                data.nEggYP.Add(theSpawnManager.neggYp[i]);
            }
        }
        // ----------------------------------------------
        data.gEggCount = theSpawnManager.gEggCount;
        data.gEggXP.Clear();
        data.gEggYP.Clear();
        if(thePlayerMove.currentMapName == "OutSide")
        {
            for (int i = 0; i < theSpawnManager.gEggCount; i++)
            {
                data.gEggXP.Add(theSpawnManager.gEggList[i].transform.position.x);
                data.gEggYP.Add(theSpawnManager.gEggList[i].transform.position.y);
            }
        }
        else if(thePlayerMove.currentMapName == "Inside")
        {
            for (int i = 0; i < theSpawnManager.gEggCount; i++)
            {
                data.gEggXP.Add(theSpawnManager.geggXp[i]);
                data.gEggYP.Add(theSpawnManager.geggYp[i]);
            }
        }

        data.characterItemsID.Clear();
        data.characterItemsCnt.Clear(); // 비우지 않으면 로드 할 때마다 똑같은 게 더 생김
        for(int i = 0; i < theUIInventory.uiitems.Count; i++)
        {
            if(theUIInventory.uiitems[i].item.Ename != "empty") // UI 칸이 비어있지 않으면
            {
                data.characterItemsID.Add(theUIInventory.uiitems[i].item.id);
                data.characterItemsCnt.Add(theUIInventory.uiitems[i].item.count);
            }
            else // UI 칸이 비어있으면
            {
                data.characterItemsID.Add(1000);
                data.characterItemsCnt.Add(0);
            }
        }
        if(theInventory.equipedItem.Ename != "empty")
        {
            for(int i = 0; i < theUIInventory.uiitems.Count; i++)
            {
                // 장착템에 해당하는 칸을 찾으면
                if(theUIInventory.uiitems[i].item == theInventory.equipedItem)
                {
                    data.equipedItemID = theInventory.equipedItem.id;
                    data.equipedItemIndex = i;
                }
            }
        }
        else // 장착템이 없다면 (empty)
        {
            data.equipedItemID = 1000;
            data.equipedItemIndex = -1;
        }

        data.containerItemsID.Clear();
        data.containerItemsCnt.Clear();
        for(int i = 0; i < theContainerItems.container.Count; i++)
        {
            if(theContainerItems.container[i].Ename != "empty")
            {
                data.containerItemsID.Add(theContainerItems.container[i].id);
                data.containerItemsCnt.Add(theContainerItems.container[i].count);
            }
        }

        data.dirtXP.Clear();
        data.dirtYP.Clear();
        for(int i = 0; i < theSpawningDirt.DarkDirtXp.Count; i++)
        {
            data.dirtXP.Add(theSpawningDirt.DarkDirtXp[i]);
            data.dirtYP.Add(theSpawningDirt.DarkDirtYp[i]);
        }

        data.plantXP.Clear();
        data.plantYP.Clear();
        data.plantTimer.Clear();
        data.plantWater.Clear();
        data.plantName.Clear();
        data.plantI.Clear();
        for(int i = 0; i < theSpawningPlant.PlantXp.Count; i++)
        {
            data.plantXP.Add(theSpawningPlant.PlantXp[i]);
            data.plantYP.Add(theSpawningPlant.PlantYp[i]);
            thePlantLoad = theSpawningPlant.createdPlant[i].GetComponent<PlantLoad>(); // 각각 찾기
            data.plantTimer.Add(thePlantLoad.plantTimer);
            data.plantWater.Add(thePlantLoad.iswatered);
            data.plantI.Add(thePlantLoad.i);
            if(theSpawningPlant.createdPlant[i].name == "BlueFlower(Clone)")
            {
                data.plantName.Add(0);
            }
            else if(theSpawningPlant.createdPlant[i].name == "Pumpkin(Clone)")
            {
                data.plantName.Add(1);
            }
            // 다른 작물 추가 필요
        }

        // 물리적인 파일로 저장
        BinaryFormatter bf = new BinaryFormatter(); // 2진 파일로 변환
        FileStream file = File.Create(Application.dataPath + "/SaveFile1.txt");
        // FileStream : 파일 입출력기 - 이 프로젝트가 설치된 폴더에 (= Asset 폴더)
        // 경로 + 파일 이름 (확장자는 아무렇게나 써도 됨)
        bf.Serialize(file, data); // data class에 담긴 정보를 file 파일에 기록하고 직렬화
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void CallSaveF2()
    {
        thePlayerMove = FindObjectOfType<PlayerMove>();
        theGameManager = FindObjectOfType<GameManager>();
        theStemina = FindObjectOfType<Stemina>();
        thePlayerControll = FindObjectOfType<PlayerControll>();
        theSpawnManager = FindObjectOfType<SpawnManager>();
        theInventory = FindObjectOfType<inventory>();
        theUIInventory = FindObjectOfType<UIInventory>();
        theContainerItems = FindObjectOfType<ContainerItems>();
        theSpawningDirt = FindObjectOfType<SpawningDirt>();
        theSpawningPlant = FindObjectOfType<SpawningPlant>();

        data.playerX = thePlayerMove.transform.position.x;
        data.playerY = thePlayerMove.transform.position.y;
        data.sceneName = thePlayerMove.currentMapName;
        data.day = theGameManager.day;
        data.timer = theGameManager.timer;
        data.currentHp = theStemina.curHp;
        data.usedMoney = 10000 - thePlayerControll.money;
        data.laborCount = thePlayerControll.laborCount;

        data.chickenCount = theSpawnManager.chickenCount;
        data.happy.Clear();
        data.checkEgg.Clear();
        data.chickenXP.Clear();
        data.chickenYP.Clear();
        if(thePlayerMove.currentMapName == "OutSide")
        {   
            for(int i = 0; i < theSpawnManager.chickenCount; i++)
            {
                data.happy.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().happy);
                data.checkEgg.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg);
                data.chickenXP.Add(theSpawnManager.chickenList[i].transform.position.x);
                data.chickenYP.Add(theSpawnManager.chickenList[i].transform.position.y);
            }
        }
        else if(thePlayerMove.currentMapName == "Inside")
        {
            for(int i = 0; i < theSpawnManager.chickenCount; i++)
            {
                data.happy.Add(theSpawnManager.chickenHp[i]);
                data.checkEgg.Add(theSpawnManager.chickenCe[i]);
                data.chickenXP.Add(theSpawnManager.chickenXp[i]);
                data.chickenYP.Add(theSpawnManager.chickenYp[i]);
            }
        }
        
        data.nEggCount = theSpawnManager.nEggCount;
        data.nEggXP.Clear();
        data.nEggYP.Clear();
        if(thePlayerMove.currentMapName == "OutSide")
        {
            for (int i = 0; i < theSpawnManager.nEggCount; i++)
            {
                data.nEggXP.Add(theSpawnManager.nEggList[i].transform.position.x);
                data.nEggYP.Add(theSpawnManager.nEggList[i].transform.position.y);
            }
        }
        else if(thePlayerMove.currentMapName == "Inside")
        {
            for (int i = 0; i < theSpawnManager.nEggCount; i++)
            {
                data.nEggXP.Add(theSpawnManager.neggXp[i]);
                data.nEggYP.Add(theSpawnManager.neggYp[i]);
            }
        }

        data.gEggCount = theSpawnManager.gEggCount;
        data.gEggXP.Clear();
        data.gEggYP.Clear();
        if(thePlayerMove.currentMapName == "OutSide")
        {
            for (int i = 0; i < theSpawnManager.gEggCount; i++)
            {
                data.gEggXP.Add(theSpawnManager.gEggList[i].transform.position.x);
                data.gEggYP.Add(theSpawnManager.gEggList[i].transform.position.y);
            }
        }
        else if(thePlayerMove.currentMapName == "Inside")
        {
            for (int i = 0; i < theSpawnManager.gEggCount; i++)
            {
                data.gEggXP.Add(theSpawnManager.geggXp[i]);
                data.gEggYP.Add(theSpawnManager.geggYp[i]);
            }
        }

        data.characterItemsID.Clear();
        data.characterItemsCnt.Clear();
        for(int i = 0; i < theUIInventory.uiitems.Count; i++)
        {
            if(theUIInventory.uiitems[i].item.Ename != "empty")
            {
                data.characterItemsID.Add(theUIInventory.uiitems[i].item.id);
                data.characterItemsCnt.Add(theUIInventory.uiitems[i].item.count);
            }
            else
            {
                data.characterItemsID.Add(1000);
                data.characterItemsCnt.Add(0);
            }
        }
        if(theInventory.equipedItem.Ename != "empty")
        {
            for(int i = 0; i < theUIInventory.uiitems.Count; i++)
            {
                if(theUIInventory.uiitems[i].item == theInventory.equipedItem)
                {
                    data.equipedItemID = theInventory.equipedItem.id;
                    data.equipedItemIndex = i;
                }
            }
        }
        else
        {
            data.equipedItemID = 1000;
            data.equipedItemIndex = -1;
        }

        data.containerItemsID.Clear();
        data.containerItemsCnt.Clear();
        for(int i = 0; i < theContainerItems.container.Count; i++)
        {
            if(theContainerItems.container[i].Ename != "empty")
            {
                data.containerItemsID.Add(theContainerItems.container[i].id);
                data.containerItemsCnt.Add(theContainerItems.container[i].count);
            }
        }

        data.dirtXP.Clear();
        data.dirtYP.Clear();
        for(int i = 0; i < theSpawningDirt.DarkDirtXp.Count; i++)
        {
            data.dirtXP.Add(theSpawningDirt.DarkDirtXp[i]);
            data.dirtYP.Add(theSpawningDirt.DarkDirtYp[i]);
        }

        data.plantXP.Clear();
        data.plantYP.Clear();
        data.plantTimer.Clear();
        data.plantWater.Clear();
        data.plantName.Clear();
        data.plantI.Clear();
        for(int i = 0; i < theSpawningPlant.PlantXp.Count; i++)
        {
            data.plantXP.Add(theSpawningPlant.PlantXp[i]);
            data.plantYP.Add(theSpawningPlant.PlantYp[i]);
            thePlantLoad = theSpawningPlant.createdPlant[i].GetComponent<PlantLoad>(); // 각각 찾기
            data.plantTimer.Add(thePlantLoad.plantTimer);
            data.plantWater.Add(thePlantLoad.iswatered);
            data.plantI.Add(thePlantLoad.i);
            if(theSpawningPlant.createdPlant[i].name == "BlueFlower(Clone)")
            {
                data.plantName.Add(0);
            }
            else if(theSpawningPlant.createdPlant[i].name == "Pumpkin(Clone)")
            {
                data.plantName.Add(1);
            }
            // 다른 작물 추가 필요
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile2.txt");
        bf.Serialize(file, data);
        file.Close();
        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }


    public void CallLoadF1() // 씬 로드
    {
        // Save와 순서 반대로
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/SaveFile1.txt");
        if (fileInfo.Exists) // 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile1.txt", FileMode.Open);
            // 원래) if(file != null && file.Length > 0)
            if(file.Length > 0) // 내용이 있을 때
            {
                data = (SNLData)bf.Deserialize(file); // 직렬화된 것을 Data 형식으로 바꿈

                doLoadF1 = true;

                theLoadingBar.ResetLoading(); // 로로딩딩 로딩1 - 리셋

                // 씬 이동
                Debug.Log("File 1 씬 로드");
                SceneManager.LoadScene(data.sceneName);
                // 다른 씬의 것은 참조 불가, 씬 이동 후의 명령어는 실행 안 됨
            }
            else
            {
                Debug.Log("File 1 비었음");
                notice.WriteMessage("1번 데이터 없음!");
            }
            file.Close(); // 파일 닫기
        }
        else
        {
            Debug.Log("File 1 없음");
            notice.WriteMessage("1번 파일이 없음!");
        }
    }

    public void RealLoadF1() // 파일 로드
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/SaveFile1.txt");
        if (fileInfo.Exists) // 파일이 존재하면
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile1.txt", FileMode.Open);
            // 원래) if(file != null && file.Length > 0)
            if(file.Length > 0) // 내용이 있을 때
            {
                data = (SNLData)bf.Deserialize(file); // 직렬화된 것을 Data 형식으로 바꿈

                thePlayerMove = FindObjectOfType<PlayerMove>();
                theGameManager = FindObjectOfType<GameManager>();
                theStemina = FindObjectOfType<Stemina>();
                thePlayerControll = FindObjectOfType<PlayerControll>();
                theSpawnManager = FindObjectOfType<SpawnManager>();
                theInventory = FindObjectOfType<inventory>();
                theUIInventory = FindObjectOfType<UIInventory>();
                theContainerUI = GameObject.Find("Canvas2").transform.Find("containerPanel").
                                                                        GetComponent<ContainerUI>();
                theContainerItems = FindObjectOfType<ContainerItems>();
                theMenuControl = FindObjectOfType<MenuControl>();
                theSpawningDirt = FindObjectOfType<SpawningDirt>(); // 0304ing
                theSpawningPlant = FindObjectOfType<SpawningPlant>(); // 0304ing

                Debug.Log("File 1 로드 시작 - 2");
                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩1 - 1 (~0.2)

                theGameManager.isTimerStoped = true; // 로드할 동안 시간 정지 (맨 아래 참고)

                thePlayerMove.transform.position = new Vector2(data.playerX, data.playerY);

                theGameManager.day = data.day;
                theGameManager.timer = data.timer;

                theStemina.curHp = data.currentHp;

                thePlayerControll.money = 10000; // 돈 초기화
                thePlayerControll.playerMoneyChange(data.usedMoney, false); // 쓴 돈 빼서 업데이트
                thePlayerControll.laborCount = data.laborCount;
                
                theSpawnManager.chickenList.Clear();
                theSpawnManager.chickenCount = 0;
                theSpawnManager.chickenHp.Clear();
                theSpawnManager.chickenCe.Clear();
                theSpawnManager.chickenXp.Clear();
                theSpawnManager.chickenYp.Clear();
                if(data.sceneName == "OutSide")
                {
                    for(int i = 0; i < data.chickenCount; i++)
                    {
                        theSpawnManager.SpawnChicken();
                    } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                    for(int i = 0; i < data.chickenCount; i++)
                    {
                        theSpawnManager.chickenList[i].GetComponent<Chicken>().happy = data.happy[i];
                        theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg = data.checkEgg[i];
                        theSpawnManager.chickenList[i].transform.position =
                                                        new Vector2(data.chickenXP[i], data.chickenYP[i]);
                    }
                }
                else if(data.sceneName == "Inside")
                {
                    theSpawnManager.chickenCount = data.chickenCount;
                    for(int i = 0; i < data.chickenCount; i++)
                    {
                        theSpawnManager.chickenHp.Add(data.happy[i]);
                        theSpawnManager.chickenCe.Add(data.checkEgg[i]);
                        theSpawnManager.chickenXp.Add(data.chickenXP[i]);
                        theSpawnManager.chickenYp.Add(data.chickenYP[i]);
                    }
                }
                // ----------------------------------------------
                theSpawnManager.nEggList.Clear();
                theSpawnManager.nEggCount = 0;
                theSpawnManager.neggXp.Clear();
                theSpawnManager.neggYp.Clear();
                if(data.sceneName == "OutSide")
                {
                    for(int i = 0; i < data.nEggCount; i++)
                    {
                        theSpawnManager.SpawnNEgg();
                    } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                    for(int i = 0; i < data.nEggCount; i++)
                    {
                        theSpawnManager.nEggList[i].transform.position =
                                                        new Vector2(data.nEggXP[i], data.nEggYP[i]);
                    }
                }
                else if(data.sceneName == "Inside")
                {
                    theSpawnManager.nEggCount = data.nEggCount;
                    for(int i = 0; i < data.nEggCount; i++)
                    {
                        theSpawnManager.neggXp.Add(data.nEggXP[i]);
                        theSpawnManager.neggYp.Add(data.nEggYP[i]);
                    }
                }
                // ----------------------------------------------
                theSpawnManager.gEggList.Clear();
                theSpawnManager.gEggCount = 0;
                theSpawnManager.geggXp.Clear();
                theSpawnManager.geggYp.Clear();
                if(data.sceneName == "OutSide")
                {
                    for(int i = 0; i < data.gEggCount; i++)
                    {
                        theSpawnManager.SpawnGEgg();
                    } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                    for(int i = 0; i < data.gEggCount; i++)
                    {
                        theSpawnManager.gEggList[i].transform.position =
                                                        new Vector2(data.gEggXP[i], data.gEggYP[i]);
                    }
                }
                else if(data.sceneName == "Inside")
                {
                    theSpawnManager.gEggCount = data.gEggCount;
                    for(int i = 0; i < data.gEggCount; i++)
                    {
                        theSpawnManager.geggXp.Add(data.gEggXP[i]);
                        theSpawnManager.geggYp.Add(data.gEggYP[i]);
                    }
                }

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩1 - 2 (~0.4)

                // 인벤
                for(int i = theInventory.characterItems.Count - 1; i >= 0; i--)
                {
                    theInventory.RemoveAll(theInventory.characterItems[i].id);
                } // 인벤 아이템 비우기
                theUIInventory.MakeSlotNull(); // 인벤 UI 비우기
                bool isEquipedOK = false;
                int trashnum = 1001; // 초기값
                for(int i = 0; i < data.characterItemsID.Count; i++) // 인벤템 불러오기
                {
                    if(data.characterItemsID[i] != 1000) // 인벤칸이 비어있지 않았다면
                    {
                        if(theInventory.CheckForItem(data.characterItemsID[i]) == null) // 아직 없다면
                        {
                            theInventory.putInventory(data.characterItemsID[i], data.characterItemsCnt[i]);
                        }
                        else // 이미 있다면 split
                        {
                            theInventory.putInventory(data.characterItemsID[i], 1);
                            theInventory.PutSplitedItem(data.characterItemsID[i]); // 기존 거에 넣고 다시 분리
                            theInventory.characterItems[i].count += data.characterItemsCnt[i] - 1;
                            theUIInventory.uiitems[i].UpdateNumUI(theUIInventory.uiitems[i].item);
                        }
                        if(data.characterItemsID[i] == data.equipedItemID && i == data.equipedItemIndex)
                        {
                            theInventory.equipedItem = theInventory.characterItems[i];
                            theUIInventory.MoveEmphasizedSlot(theUIInventory.uiitems[i].transform); // 강조
                            isEquipedOK = true;
                        } // 인벤템과 저장된 장착템 ID, Index가 같으면 장착 (인벤에 있는 거라면)
                    }
                    else // 인벤칸이 비어있다면
                    {
                        theInventory.putInventory(trashnum, 1);
                        trashnum++;
                    }
                } // 저장된 아이템 목록 하나씩 넣기 (개수 고려됨)
                if(trashnum >= 1002) // trash가 1개 이상
                {
                    for(int i = 1001; i < trashnum; i++)
                    {
                        theInventory.RemoveAll(i);
                    }
                } // 쓰레기템 지우기
                if(!isEquipedOK) // 인벤에 없다면 (= 클릭된 게 없음)
                {
                    theUIInventory.MakeSlotClear(); // 장착템 UI 초기화 (02/28 추가)
                    theInventory.equipedItem = theInventory.emptyItem;
                } // 장착템 설정

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩1 - 3 (~0.6)

                // 보관상자
                theContainerItems.container.Clear(); // 보관상자 아이템 비우기
                if(data.containerItemsID.Count > 0)
                {
                    for(int i = 0; i < data.containerItemsID.Count; i++)
                    {
                        if(data.containerItemsID[i] > 0)
                        {
                            theContainerItems.PutInContainer(data.containerItemsID[i], data.containerItemsCnt[i]);
                        }
                    }
                } // 보관상자 저장템 넣기
                theContainerUI.isContainerChanged = true; // 보관상자 UI 업데이트

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩1 - 4 (~0.8)

                // for 흙, 식물
                GameObject dontDestroy = GameObject.Find("DonDestroyGameObject").gameObject;
                // 흙 초기화 및 로드
                for(int i = theSpawningDirt.DarkDirtXp.Count - 1; i >= 0; i--)
                {
                    Destroy(theSpawningDirt.createdDarkDirt[i]);
                }
                theSpawningDirt.DarkDirtXp.Clear();
                theSpawningDirt.DarkDirtYp.Clear();
                theSpawningDirt.createdDarkDirt.Clear(); // 돈디스트로이된 흙 제거
                for(int i = 0; i < data.dirtXP.Count; i++)
                {
                    GameObject dirtt = Instantiate(theSpawningDirt.DirtPrefab);
                    dirtt.transform.parent = dontDestroy.transform;
                    if(data.sceneName == "OutSide")
                    {   dirtt.transform.position = new Vector2(data.dirtXP[i], data.dirtYP[i]);   }
                    else if(data.sceneName == "Inside")
                    {   dirtt.transform.position = new Vector2(20f, -8f);   }
                    theSpawningDirt.createdDarkDirt.Add(dirtt);
                    theSpawningDirt.DarkDirtXp.Add(data.dirtXP[i]);
                    theSpawningDirt.DarkDirtYp.Add(data.dirtYP[i]);
                }
                // 식물 초기화 및 로드
                for(int i = theSpawningPlant.PlantXp.Count - 1; i >= 0; i--)
                {
                    Destroy(theSpawningPlant.createdPlant[i]);
                }
                theSpawningPlant.PlantXp.Clear();
                theSpawningPlant.PlantYp.Clear();
                theSpawningPlant.createdPlant.Clear(); // 돈디스트로이된 식물 제거
                for(int i = 0; i < data.plantXP.Count; i++)
                {
                    GameObject plantt = Instantiate(theSpawningPlant.PlantPrefabs[data.plantName[i]]);
                    plantt.transform.parent = dontDestroy.transform;
                    if(data.sceneName == "OutSide")
                    {   plantt.transform.position = new Vector2(data.plantXP[i], data.plantYP[i]);   }
                    else if(data.sceneName == "Inside")
                    {   plantt.transform.position = new Vector2(20f, -8f);   }
                    theSpawningPlant.createdPlant.Add(plantt);
                    theSpawningPlant.PlantXp.Add(data.plantXP[i]);
                    theSpawningPlant.PlantYp.Add(data.plantYP[i]);
                    thePlantLoad = plantt.GetComponent<PlantLoad>();
                    thePlantLoad.plantTimer = data.plantTimer[i];
                    thePlantLoad.iswatered = data.plantWater[i];
                    thePlantLoad.i = data.plantI[i];
                    thePlantLoad.DoPlantAni(data.plantI[i], data.plantWater[i]); // 이 함수 안에 water 포함
                }

                theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간 흐르기 시작 (맨 위 참고)

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩1 - 5 (~1.0)
                Debug.Log("File 1 로드 완료");
                notice.WriteMessage("File 1 이어하기!");
            }
            file.Close(); // 파일 닫기
        }
    }


    public void CallLoadF2()
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/SaveFile2.txt");
        if (fileInfo.Exists)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile2.txt", FileMode.Open);
            if(file.Length > 0)
            {
                data = (SNLData)bf.Deserialize(file);
                doLoadF2 = true;
                theLoadingBar.ResetLoading();
                Debug.Log("File 2 씬 로드");
                SceneManager.LoadScene(data.sceneName);
            }
            else
            {
                Debug.Log("File 2 비었음");
                notice.WriteMessage("2번 데이터 없음!");
            }
            file.Close();
        }
        else
        {
            Debug.Log("File 2 없음");
            notice.WriteMessage("2번 파일이 없음!");
        }
    }

    public void RealLoadF2()
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/SaveFile2.txt");
        if (fileInfo.Exists)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile2.txt", FileMode.Open);
            if(file.Length > 0)
            {
                data = (SNLData)bf.Deserialize(file);

                thePlayerMove = FindObjectOfType<PlayerMove>();
                theGameManager = FindObjectOfType<GameManager>();
                theStemina = FindObjectOfType<Stemina>();
                thePlayerControll = FindObjectOfType<PlayerControll>();
                theSpawnManager = FindObjectOfType<SpawnManager>();
                theInventory = FindObjectOfType<inventory>();
                theUIInventory = FindObjectOfType<UIInventory>();
                theContainerUI = GameObject.Find("Canvas2").transform.Find("containerPanel").
                                                                        GetComponent<ContainerUI>();
                theContainerItems = FindObjectOfType<ContainerItems>();
                theMenuControl = FindObjectOfType<MenuControl>();
                theSpawningDirt = FindObjectOfType<SpawningDirt>();
                theSpawningPlant = FindObjectOfType<SpawningPlant>();

                Debug.Log("File 2 로드 시작 - 2");
                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩2 - 1 (~0.2)
                theGameManager.isTimerStoped = true;
                thePlayerMove.transform.position = new Vector2(data.playerX, data.playerY);
                theGameManager.day = data.day;
                theGameManager.timer = data.timer;
                theStemina.curHp = data.currentHp;
                thePlayerControll.money = 10000;
                thePlayerControll.playerMoneyChange(data.usedMoney, false);
                thePlayerControll.laborCount = data.laborCount;
            
                theSpawnManager.chickenList.Clear();
                theSpawnManager.chickenCount = 0;
                theSpawnManager.chickenHp.Clear();
                theSpawnManager.chickenCe.Clear();
                theSpawnManager.chickenXp.Clear();
                theSpawnManager.chickenYp.Clear();
                if(data.sceneName == "OutSide")
                {
                    for(int i = 0; i < data.chickenCount; i++)
                    {
                        theSpawnManager.SpawnChicken();
                    } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                    for(int i = 0; i < data.chickenCount; i++)
                    {
                        theSpawnManager.chickenList[i].GetComponent<Chicken>().happy = data.happy[i];
                        theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg = data.checkEgg[i];
                        theSpawnManager.chickenList[i].transform.position =
                                                        new Vector2(data.chickenXP[i], data.chickenYP[i]);
                    }
                }
                else if(data.sceneName == "Inside")
                {
                    theSpawnManager.chickenCount = data.chickenCount;
                    for(int i = 0; i < data.chickenCount; i++)
                    {
                        theSpawnManager.chickenHp.Add(data.happy[i]);
                        theSpawnManager.chickenCe.Add(data.checkEgg[i]);
                        theSpawnManager.chickenXp.Add(data.chickenXP[i]);
                        theSpawnManager.chickenYp.Add(data.chickenYP[i]);
                    }
                }
                
                theSpawnManager.nEggList.Clear();
                theSpawnManager.nEggCount = 0;
                theSpawnManager.neggXp.Clear();
                theSpawnManager.neggYp.Clear();
                if(data.sceneName == "OutSide")
                {
                    for(int i = 0; i < data.nEggCount; i++)
                    {
                        theSpawnManager.SpawnNEgg();
                    } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                    for(int i = 0; i < data.nEggCount; i++)
                    {
                        theSpawnManager.nEggList[i].transform.position =
                                                        new Vector2(data.nEggXP[i], data.nEggYP[i]);
                    }
                }
                else if(data.sceneName == "Inside")
                {
                    theSpawnManager.nEggCount = data.nEggCount;
                    for(int i = 0; i < data.nEggCount; i++)
                    {
                        theSpawnManager.neggXp.Add(data.nEggXP[i]);
                        theSpawnManager.neggYp.Add(data.nEggYP[i]);
                    }
                }
                
                theSpawnManager.gEggList.Clear();
                theSpawnManager.gEggCount = 0;
                theSpawnManager.geggXp.Clear();
                theSpawnManager.geggYp.Clear();
                if(data.sceneName == "OutSide")
                {
                    for(int i = 0; i < data.gEggCount; i++)
                    {
                        theSpawnManager.SpawnGEgg();
                    } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                    for(int i = 0; i < data.gEggCount; i++)
                    {
                        theSpawnManager.gEggList[i].transform.position =
                                                        new Vector2(data.gEggXP[i], data.gEggYP[i]);
                    }
                }
                else if(data.sceneName == "Inside")
                {
                    theSpawnManager.gEggCount = data.gEggCount;
                    for(int i = 0; i < data.gEggCount; i++)
                    {
                        theSpawnManager.geggXp.Add(data.gEggXP[i]);
                        theSpawnManager.geggYp.Add(data.gEggYP[i]);
                    }
                }

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩2 - 2 (~0.4)

                for(int i = theInventory.characterItems.Count - 1; i >= 0; i--)
                {
                    theInventory.RemoveAll(theInventory.characterItems[i].id);
                }
                theUIInventory.MakeSlotNull();
                bool isEquipedOK = false;
                int trashnum = 1001;
                for(int i = 0; i < data.characterItemsID.Count; i++)
                {
                    if(data.characterItemsID[i] != 1000)
                    {
                        if(theInventory.CheckForItem(data.characterItemsID[i]) == null)
                        {
                            theInventory.putInventory(data.characterItemsID[i], data.characterItemsCnt[i]);
                        }
                        else
                        {
                            theInventory.putInventory(data.characterItemsID[i], 1);
                            theInventory.PutSplitedItem(data.characterItemsID[i]);
                            theInventory.characterItems[i].count += data.characterItemsCnt[i] - 1;
                            theUIInventory.uiitems[i].UpdateNumUI(theUIInventory.uiitems[i].item);
                        }
                        if(data.characterItemsID[i] == data.equipedItemID && i == data.equipedItemIndex)
                        {
                            theInventory.equipedItem = theInventory.characterItems[i];
                            theUIInventory.MoveEmphasizedSlot(theUIInventory.uiitems[i].transform);
                            isEquipedOK = true;
                        }
                    }
                    else
                    {
                        theInventory.putInventory(trashnum, 1);
                        trashnum++;
                    }
                }
                if(trashnum >= 1002)
                {
                    for(int i = 1001; i < trashnum; i++)
                    {
                        theInventory.RemoveAll(i);
                    }
                }
                if(!isEquipedOK)
                {
                    theUIInventory.MakeSlotClear();
                    theInventory.equipedItem = theInventory.emptyItem;
                }

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩2 - 3 (~0.6)

                theContainerItems.container.Clear();
                if(data.containerItemsID.Count > 0)
                {
                    for(int i = 0; i < data.containerItemsID.Count; i++)
                    {
                        if(data.containerItemsID[i] > 0)
                        {
                            theContainerItems.PutInContainer(data.containerItemsID[i], data.containerItemsCnt[i]);
                        }
                    }
                }
                theContainerUI.isContainerChanged = true;

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩2 - 4 (~0.8)
                
                GameObject dontDestroy = GameObject.Find("DonDestroyGameObject").gameObject;
                
                for(int i = theSpawningDirt.DarkDirtXp.Count - 1; i >= 0; i--)
                {
                    Destroy(theSpawningDirt.createdDarkDirt[i]);
                }
                theSpawningDirt.DarkDirtXp.Clear();
                theSpawningDirt.DarkDirtYp.Clear();
                theSpawningDirt.createdDarkDirt.Clear();
                for(int i = 0; i < data.dirtXP.Count; i++)
                {
                    GameObject dirtt = Instantiate(theSpawningDirt.DirtPrefab);
                    dirtt.transform.parent = dontDestroy.transform;
                    if(data.sceneName == "OutSide")
                    {   dirtt.transform.position = new Vector2(data.dirtXP[i], data.dirtYP[i]);   }
                    else if(data.sceneName == "Inside")
                    {   dirtt.transform.position = new Vector2(20f, -8f);   }
                    theSpawningDirt.createdDarkDirt.Add(dirtt);
                    theSpawningDirt.DarkDirtXp.Add(data.dirtXP[i]);
                    theSpawningDirt.DarkDirtYp.Add(data.dirtYP[i]);
                }
                
                for(int i = theSpawningPlant.PlantXp.Count - 1; i >= 0; i--)
                {
                    Destroy(theSpawningPlant.createdPlant[i]);
                }
                theSpawningPlant.PlantXp.Clear();
                theSpawningPlant.PlantYp.Clear();
                theSpawningPlant.createdPlant.Clear();
                for(int i = 0; i < data.plantXP.Count; i++)
                {
                    GameObject plantt = Instantiate(theSpawningPlant.PlantPrefabs[data.plantName[i]]);
                    plantt.transform.parent = dontDestroy.transform;
                    if(data.sceneName == "OutSide")
                    {   plantt.transform.position = new Vector2(data.plantXP[i], data.plantYP[i]);   }
                    else if(data.sceneName == "Inside")
                    {   plantt.transform.position = new Vector2(20f, -8f);   }
                    theSpawningPlant.createdPlant.Add(plantt);
                    theSpawningPlant.PlantXp.Add(data.plantXP[i]);
                    theSpawningPlant.PlantYp.Add(data.plantYP[i]);
                    thePlantLoad = plantt.GetComponent<PlantLoad>();
                    thePlantLoad.plantTimer = data.plantTimer[i];
                    thePlantLoad.iswatered = data.plantWater[i];
                    thePlantLoad.i = data.plantI[i];
                    thePlantLoad.DoPlantAni(data.plantI[i], data.plantWater[i]);
                }

                theMenuControl.WhenRestart();

                theLoadingBar.loadSlider.value += 0.2f; // 로로딩딩 로딩2 - 5 (~1.0)
                Debug.Log("File 2 로드 완료");
                notice.WriteMessage("File 2 이어하기!");
            }
            file.Close();
        }
    }
}
