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

    // 안내 메세지
    public NoticeText notice;

    void Start()
    {
        notice = GameObject.Find("Notice").GetComponent<NoticeText>(); // 안내 메세지
    }

    public bool doLoadF1 = false;
    public bool doLoadF2 = false;
    public bool doNewGame = false;

     //그리고 합치니까 인게임 ui가 타이틀 화면 위로 올라오길래 canvas의 sortorder을 2로 바꾸었습니당
     //혹시 유니티 상에서 그렇게 뜬다면 바꿔주세용!
     // 성현 - 그거 아마 scene 창에서만 그렇게 뜨고 플레이 창에선 괜찮았던 것 같아 나는 ㅜ

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
            //doLoadF2 = false;
            //Debug.Log("File 2 로드 중");
            //Invoke("RealLoadF2", 2); // 씬 바꿀 시간 기다림
        }
    }

    private PlayerMove thePlayerMove; // 플레이어 좌표, 씬 이름
    private GameManager theGameManager; // 날짜, 시간
    private Stemina theStemina; // stamina
    private PlayerControll thePlayerControll; // 쓴 돈, laborCount
    private SpawnManager theSpawnManager; // 닭 개수/행복도/checkEgg/좌표, 달걀 개수/좌표
    private inventory theInventory; // 인벤템 ID, 인벤템 개수, 장착템 ID
    private UIInventory theUIInventory; // 인벤 스프라이트 (for inventory) (+ 인벤 UI 상태: 수정 후 필요X)
    private ContainerUI theContainerUI; // 상자 스프라이트 (for ContainerItems) - UI 업데이트
    private ContainerItems theContainerItems; // 보관템 ID, 보관템 개수
    private MenuControl theMenuControl; // 일시정지 창 꺼놓기 위해서 - Load(+New)에만 사용
    private SpawningDirt theSpawningDirt; // dirt -------------------- load
    private SpawningPlant theSpawningPlant; // plant -------------------- load
    private PlantLoad thePlantLoad; // plant -------------------- load&save 내부
    private PourWater thePourWater; // water -------------------- (일단은)load

    public SNLData data; // SNLData 이용할 것임

    public void CallNewGame()
    {
        doNewGame = true; // 단순히 기능 확인하고 싶으면 이거 주석처리하면 됨!

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

        Debug.Log("새 게임 로드 시작 - 2");

        theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간아 흘러라

        thePlayerMove.transform.position = new Vector2(0, 0); // 플레이어 좌표 초기화

        theGameManager.day = 1; // DAY 초기화
        theGameManager.timer = 420; // 타이머 초기화 (7시)

        theStemina.curHp = 200f; // stemina 초기화

        int pastMoney = thePlayerControll.money; // 돈 초기화 - 1
        thePlayerControll.playerMoneyChange(2000 - pastMoney, true); // 돈 초기화 - 2
        thePlayerControll.laborCount = 5; // laborCount 초기화

        theSpawnManager.chickenList.Clear(); // 닭 목록 초기화
        theSpawnManager.chickenCount = 0; // 닭 개수 초기화
        // ----------------------------------------------
        theSpawnManager.nEggList.Clear(); // 보통 달걀 목록 초기화
        theSpawnManager.nEggCount = 0; // 보통 달걀 개수 초기화
        // ----------------------------------------------
        theSpawnManager.gEggList.Clear(); // 좋은 달걀 목록 초기화
        theSpawnManager.gEggCount = 0; // 좋은 달걀 개수 초기화
        
        for(int i = theInventory.characterItems.Count - 1; i >= 0 ; i--)
        {
            theInventory.RemoveAll(theInventory.characterItems[i].id);
        } // 인벤 아이템 비우기 (인벤 초기화)
        theUIInventory.MakeSlotNull(); // 인벤 UI 비우기 (인벤 UI 초기화)
        theInventory.putInventory(1,3); // 기본 인벤템
        theInventory.putInventory(2); // 기본 인벤템
        theInventory.putInventory(100); // 기본 인벤템
        theInventory.putInventory(101); // 기본 인벤템
        theInventory.putInventory(99,3); // 기본 인벤템
        theUIInventory.MakeSlotClear(); // 장착템 UI 초기화
        theInventory.equipedItem = theInventory.emptyItem; // 장착템 초기화

        theContainerItems.container.Clear(); // 보관상자 아이템 비우기 (보관상자 초기화)
        theContainerUI.isContainerChanged = true; // 보관상자 UI 업데이트

        // dirt, plant, water는 씬 바뀌면 파괴되어서 초기화할 필요 없음

        Debug.Log("새 게임 로드 완료");
        notice.WriteMessage("새 게임 시작!");
    }


    // 나중에 CallSaveF2, CallLoadF2, RealLoadF2 만들 거임
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

        data.playerX = thePlayerMove.transform.position.x;
        data.playerY = thePlayerMove.transform.position.y;
        data.sceneName = thePlayerMove.currentMapName;

        data.day = theGameManager.day;
        data.timer = theGameManager.timer;

        data.currentHp = theStemina.curHp;

        data.usedMoney = 2000 - thePlayerControll.money; // 쓴 금액으로 2000은 기본값
        data.laborCount = thePlayerControll.laborCount;

        data.chickenCount = theSpawnManager.chickenCount;
        data.happy.Clear();
        data.checkEgg.Clear();
        data.chickenXP.Clear();
        data.chickenYP.Clear();
        for(int i = 0; i < theSpawnManager.chickenCount; i++)
        {
            data.happy.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().happy);
            data.checkEgg.Add(theSpawnManager.chickenList[i].GetComponent<Chicken>().checkEgg);
            data.chickenXP.Add(theSpawnManager.chickenList[i].transform.position.x);
            data.chickenYP.Add(theSpawnManager.chickenList[i].transform.position.y);
        }
        // ----------------------------------------------
        data.nEggCount = theSpawnManager.nEggCount;
        data.nEggXP.Clear();
        data.nEggYP.Clear();
        for (int i = 0; i < theSpawnManager.nEggCount; i++)
        {
            data.nEggXP.Add(theSpawnManager.nEggList[i].transform.position.x);
            data.nEggYP.Add(theSpawnManager.nEggList[i].transform.position.y);
        }
        // ----------------------------------------------
        data.gEggCount = theSpawnManager.gEggCount;
        data.gEggXP.Clear();
        data.gEggYP.Clear();
        for (int i = 0; i < theSpawnManager.gEggCount; i++)
        {
            data.gEggXP.Add(theSpawnManager.gEggList[i].transform.position.x);
            data.gEggYP.Add(theSpawnManager.gEggList[i].transform.position.y);
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
        {   data.equipedItemID = theInventory.equipedItem.id;   }
        else
        {   data.equipedItemID = 1000; }

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

        GameObject[] obj1 = GameObject.FindGameObjectsWithTag("DarkDirt");
        data.dirtXP.Clear();
        data.dirtYP.Clear();
        for(int i = 0; i < obj1.Length; i++)
        {
            data.dirtXP.Add(obj1[i].gameObject.transform.position.x);
            data.dirtYP.Add(obj1[i].gameObject.transform.position.y);
        }

        GameObject[] obj2 = GameObject.FindGameObjectsWithTag("Plant");
        data.plantXP.Clear();
        data.plantYP.Clear();
        data.plantTimer.Clear();
        data.plantWater.Clear();
        data.plantName.Clear();
        data.plantI.Clear();
        for(int i = 0; i < obj2.Length; i++)
        {
            thePlantLoad = obj2[i].gameObject.GetComponent<PlantLoad>(); // 각각 찾기
            data.plantXP.Add(obj2[i].gameObject.transform.position.x);
            data.plantYP.Add(obj2[i].gameObject.transform.position.y);
            data.plantTimer.Add(thePlantLoad.plantTimer); //timer 변수가 plantTimer로 바뀐 것 같아 반영합니당( 0310 미해)
            data.plantWater.Add(thePlantLoad.iswatered);
            if(obj2[i].gameObject.name == "Flower(Clone)")
            {
                data.plantName.Add(0);
            }
            else if(obj2[i].gameObject.name == "Pumpkin(Clone)")
            {
                data.plantName.Add(1);
            }
            // 다른 작물 추가 필요
            data.plantI.Add(thePlantLoad.i);
        }

        GameObject[] obj3 = GameObject.FindGameObjectsWithTag("Water");
        data.waterXP.Clear();
        data.waterYP.Clear();
        data.waterTimer.Clear();
        for(int i = 0; i < obj3.Length; i++)
        {
            data.waterXP.Add(obj3[i].gameObject.transform.position.x);
            data.waterYP.Add(obj3[i].gameObject.transform.position.y);
            // data.waterTimer.Add(~) 추가 필요
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

                // 씬 이동
                Debug.Log("File 1 씬 로드");
                SceneManager.LoadScene(data.sceneName);
                // 다른 씬의 것은 참조 불가, 씬 이동 후의 명령어는 실행 안 됨
            }
            else
            {
                Debug.Log("File 1 비었음");
            }
            file.Close(); // 파일 닫기
        }
        else
        {
            Debug.Log("File 1 없음");
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
                thePourWater = FindObjectOfType<PourWater>(); //0304ing

                Debug.Log("File 1 로드 시작 - 2");

                theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간아 흘러라

                thePlayerMove.transform.position = new Vector2(data.playerX, data.playerY);

                theGameManager.day = data.day;
                theGameManager.timer = data.timer;

                theStemina.curHp = data.currentHp;

                thePlayerControll.money = 2000; // 돈 초기화
                thePlayerControll.playerMoneyChange(data.usedMoney, false); // 쓴 돈 빼서 업데이트
                thePlayerControll.laborCount = data.laborCount;
                
                theSpawnManager.chickenList.Clear();
                theSpawnManager.chickenCount = 0;
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
                // ----------------------------------------------
                theSpawnManager.nEggList.Clear();
                theSpawnManager.nEggCount = 0;
                for(int i = 0; i < data.nEggCount; i++)
                {
                    theSpawnManager.SpawnNEgg();
                } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                for(int i = 0; i < data.nEggCount; i++)
                {
                    theSpawnManager.nEggList[i].transform.position =
                                                    new Vector2(data.nEggXP[i], data.nEggYP[i]);
                }
                // ----------------------------------------------
                theSpawnManager.gEggList.Clear();
                theSpawnManager.gEggCount = 0;
                for(int i = 0; i < data.gEggCount; i++)
                {
                    theSpawnManager.SpawnGEgg();
                } // for문을 통해 스폰하면서 SM의 Count도 결정됨, 리스트에도 추가됨
                for(int i = 0; i < data.gEggCount; i++)
                {
                    theSpawnManager.gEggList[i].transform.position =
                                                    new Vector2(data.gEggXP[i], data.gEggYP[i]);
                }

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
                        theInventory.putInventory(data.characterItemsID[i], data.characterItemsCnt[i]);
                        if(data.characterItemsID[i] == data.equipedItemID)
                        {
                            // 인벤템과 저장된 장착템 ID가 같으면 장착 (인벤에 있는 거라면)
                            theInventory.equipedItem = theInventory.characterItems[i];
                            theUIInventory.MoveEmphasizedSlot(theUIInventory.uiitems[i].transform); // 강조
                            isEquipedOK = true;
                        }
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

                // dirt
                for(int i = 0; i < data.dirtXP.Count; i++)
                {
                    GameObject dirtt = Instantiate(theSpawningDirt.DirtPrefab);
                    dirtt.transform.position = new Vector2(data.dirtXP[i], data.dirtYP[i]);
                }
                
                // plant
                for(int i = 0; i < data.plantXP.Count; i++)
                {
                    GameObject plantt = Instantiate(theSpawningPlant.PlantPrefabs[data.plantName[i]]);
                    plantt.transform.position = new Vector2(data.plantXP[i], data.plantYP[i]);
                    thePlantLoad = plantt.GetComponent<PlantLoad>();
                    thePlantLoad.plantTimer = data.plantTimer[i]; //timer -> plantTimer로 바꾸었습니당 ( 지현이의 변수가 바뀐것 같아 따라서 바꾸었습니당) (0310 미해)
                    thePlantLoad.iswatered = data.plantWater[i];
                    thePlantLoad.i = data.plantI[i];
                    thePlantLoad.DoPlantAni(data.plantI[i]);
                }

                /* water을 더이상 prefab으로 생성하지 않아서 잠시 주석처리 했습니당! (0310 미해)
                // water
                for(int i = 0; i < data.waterXP.Count; i++)
                {
                    GameObject waterr = Instantiate(thePourWater.waterPrefab);
                    waterr.transform.position = new Vector2(data.waterXP[i], data.waterYP[i]);
                    // waterTimer 추가 필요
                }
                */

                Debug.Log("File 1 로드 완료");
            }
            file.Close(); // 파일 닫기
        }
    }

}
