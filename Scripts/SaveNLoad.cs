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

    public bool doLoadF1 = false;
    public bool doLoadF2 = false;
    public bool doNewGame = false;

    //미해 : 보관상자의 내용이 들어있는 container의 위치가 ContainerItems으로 이동되었음.
    //이 값을 private으로 가져와서 container 객체의 내용을 참조해줄것..!
    //일단은 오류때문에 관련 내용 주석처리 해놓았습니당..!

     //그리고 합치니까 인게임 ui가 타이틀 화면 위로 올라오길래 canvas의 sortorder을 2로 바꾸었습니당
     //혹시 유니티 상에서 그렇게 뜬다면 바꿔주세용!
     // 성현 - 그거 아마 scene 창에서만 그렇게 뜨고 플레이 창에선 괜찮았던 것 같아 나는 ㅜ

    // 테스트용
    public List<float> XP = new List<float>(); // dirt, plant, water
    public List<float> YP = new List<float>(); // dirt, plant, water
    public List<float> pTimer = new List<float>(); // plant
    public List<bool> pWater = new List<bool>(); // plant
    public List<int> pName = new List<int>(); // plant
    public SpawningDirt theSpawningDirt; // dirt
    public SpawningPlant theSpawningPlant; // plant
    public PlantLoad thePlantLoad; // plant
    public PourWater thePourWater; // water

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

        // dirt 테스트용
        /*theSpawningDirt = FindObjectOfType<SpawningDirt>();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("DarkDirt");
            XP.Clear();
            YP.Clear();
            for(int i = 0; i < obj.Length; i++)
            {
                XP.Add(obj[i].gameObject.transform.position.x);
                YP.Add(obj[i].gameObject.transform.position.y);
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("DarkDirt");
            for(int i = obj.Length - 1; i >= 0; i--)
            {
                Destroy(obj[i].gameObject);
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            for(int i = 0; i < XP.Count; i++)
            {
                GameObject dirtt = Instantiate(theSpawningDirt.DirtPrefab);
                dirtt.transform.position = new Vector2(XP[i], YP[i]);
            }
        }*/

        // plant 테스트용
        /*theSpawningPlant = FindObjectOfType<SpawningPlant>();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Plant");
            XP.Clear();
            YP.Clear();
            for(int i = 0; i < obj.Length; i++)
            {
                thePlantLoad = obj[i].gameObject.GetComponent<PlantLoad>();
                XP.Add(obj[i].gameObject.transform.position.x);
                YP.Add(obj[i].gameObject.transform.position.y);
                pTimer.Add(thePlantLoad.timer);
                pWater.Add(thePlantLoad.iswatered);
                if(obj[i].gameObject.name == "Flower(Clone)")
                {
                    pName.Add(0);
                }
                else if(obj[i].gameObject.name == "Pumpkin(Clone)")
                {
                    pName.Add(1);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Plant");
            for(int i = obj.Length - 1; i >= 0; i--)
            {
                Destroy(obj[i].gameObject);
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            for(int i = 0; i < XP.Count; i++)
            {
                GameObject plantt = Instantiate(theSpawningPlant.PlantPrefabs[pName[i]]);
                plantt.transform.position = new Vector2(XP[i], YP[i]);
                thePlantLoad = plantt.GetComponent<PlantLoad>();
                thePlantLoad.timer = pTimer[i];
                thePlantLoad.iswatered = pWater[i];
            }
        }
        // 문제 : dirt 위가 아니어도 심어지고, dirt와 마찬가지로 중복 방지가 안 됨
        // 해결 방법 : 순서를 잘 고려해서 불러오자! (dirt → plant)
        */

        // water 테스트용
        /*thePourWater = FindObjectOfType<PourWater>();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Water");
            XP.Clear();
            YP.Clear();
            for(int i = 0; i < obj.Length; i++)
            {
                XP.Add(obj[i].gameObject.transform.position.x);
                YP.Add(obj[i].gameObject.transform.position.y);
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Water");
            for(int i = obj.Length - 1; i >= 0; i--)
            {
                Destroy(obj[i].gameObject);
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            for(int i = 0; i < XP.Count; i++)
            {
                GameObject dirtt = Instantiate(thePourWater.waterPrefab);
                dirtt.transform.position = new Vector2(XP[i], YP[i]);
            }
        }*/
    }

    private PlayerMove thePlayerMove; // 플레이어 좌표, 씬 이름
    private GameManager theGameManager; // 날짜, 시간, stamina
    private PlayerControll thePlayerControll; // 쓴 돈, laborCount
    private SpawnManager theSpawnManager; // 닭 개수/행복도/checkEgg/좌표, 달걀 개수/좌표
    private inventory theInventory; // 인벤템 ID, 인벤템 개수, 장착템 ID
    private UIInventory theUIInventory; // 인벤 스프라이트 (for inventory) (+ 인벤 UI 상태: 수정 후 필요X)
    private ContainerDb theContainerDb;
    private ContainerUI theContainerUI; // 상자 스프라이트 (for ContainerDb) - 로드 시 필요///
    private ContainerItems theContainerItems; // 보관템 ID, 보관템 개수
    private MenuControl theMenuControl; // 일시정지 창 꺼놓기 위해서 - Load(+New)에만 사용

    public SNLData data; // SNLData 이용할 것임

    private Vector2 vector; // player 위치 불러올 곳

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
        thePlayerControll = FindObjectOfType<PlayerControll>();
        theSpawnManager = FindObjectOfType<SpawnManager>();
        theInventory = FindObjectOfType<inventory>();
        theUIInventory = FindObjectOfType<UIInventory>();
        theContainerDb = GameObject.Find("Canvas2").transform.Find("containerPanel").
                                                                        GetComponent<ContainerDb>();
        theContainerItems = FindObjectOfType<ContainerItems>();
        theMenuControl = FindObjectOfType<MenuControl>();

        Debug.Log("새 게임 로드 시작 - 2");

        theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간아 흘러라

        thePlayerMove.transform.position = new Vector2(0, 0); // 플레이어 좌표 초기화

        theGameManager.day = 1; // DAY 초기화
        theGameManager.timer = 420; // 타이머 초기화 (7시)
        theGameManager.stamina = 10; // stamina 초기화

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
        theInventory.equipedItem = null; // 장착템 초기화

        // (0223 미해 merge 용 주석처리) : containerItems.container로 바꿔주세용!
        // 성현 - UI도 지워야 해서 이것도 그거 말고 condb로 접근해야 할 것 같아...ㅜㅜ
        for(int i = theContainerItems.container.Count - 1; i >= 0; i--)
        {
            if(theContainerItems.container[i] != null)
            {
                //Debug.Log(theContainerItems.container[i].Ename + " 제거");
                theContainerDb.RemoveItem(theContainerItems.container[i].id);
            }
        } // 보관상자 아이템 비우기 (보관상자 초기화)

        Debug.Log("새 게임 로드 완료");
    }


    // 나중에 CallSaveF2, CallLoadF2, RealLoadF2 만들 거임
    public void CallSaveF1()
    {
        thePlayerMove = FindObjectOfType<PlayerMove>();
        theGameManager = FindObjectOfType<GameManager>();
        thePlayerControll = FindObjectOfType<PlayerControll>();
        theSpawnManager = FindObjectOfType<SpawnManager>();
        theInventory = FindObjectOfType<inventory>();
        theUIInventory = FindObjectOfType<UIInventory>();
        //theContainerDb = GameObject.Find("Canvas2").transform.Find("보관상자").
                            //transform.Find("ContainerPanel").GetComponent<ContainerDb>();
        //theContainerUI = GameObject.Find("Canvas2").transform.Find("보관상자").
                            //transform.Find("ContainerPanel").GetComponent<ContainerUI>();
        theContainerItems = FindObjectOfType<ContainerItems>();

        data.playerX = thePlayerMove.transform.position.x;
        data.playerY = thePlayerMove.transform.position.y;
        data.sceneName = thePlayerMove.currentMapName;

        data.day = theGameManager.day;
        data.timer = theGameManager.timer;
        data.stamina = theGameManager.stamina;

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
        //data.invenUIEmpty.Clear(); // 수정 후 필요X
        /*for(int i = 0; i < theInventory.characterItems.Count; i++)
        {
            data.characterItemsID.Add(theInventory.characterItems[i].id);
            data.characterItemsCnt.Add(theInventory.characterItems[i].count);
            // 이거 저장할 때 따로 변수 파서 빈칸(null) 위치도 고려해야 함 - 저장템도
            // 그러려면 UIInventory 및 ContainerUI도 가져와야 함
        }
        for(int i = 0; i < theUIInventory.uiitems.Count; i++)
        {
            if (theUIInventory.uiitems[i].item != null) // UI 칸이 비어있지 않으면
            {
                data.invenUIEmpty.Add(false);
            }
            else{   data.invenUIEmpty.Add(true);   } // UI 칸이 비어있으면
        }
        if(theInventory.equipedItem != null)
        {   data.equipedItemID = theInventory.equipedItem.id;   }
        else
        {   data.equipedItemID = 0; }*/
        // 아래로 수정
        for(int i = 0; i < theUIInventory.uiitems.Count; i++)
        {
            if(theUIInventory.uiitems[i].item != null) // UI 칸이 비어있지 않으면
            {
                data.characterItemsID.Add(theUIInventory.uiitems[i].item.id);
                data.characterItemsCnt.Add(theUIInventory.uiitems[i].item.count);
            }
            else // UI 칸이 비어있으면
            {
                data.characterItemsID.Add(0);
                data.characterItemsCnt.Add(0);
            }
        }
        if(theInventory.equipedItem != null)
        {   data.equipedItemID = theInventory.equipedItem.id;   }
        else
        {   data.equipedItemID = 0; }

        data.containerItemsID.Clear();
        data.containerItemsCnt.Clear();
        /*for(int i = 0; i < theContainerDb.container.Count; i++)
        {
            data.containerItemsID.Add(theContainerDb.container[i].id);
            data.containerItemsID.Add(theContainerDb.container[i].count);
        }*/
        //for(int i = 0; i < theContainerUI.container.Count; i++)
        //{
        //    if(theContainerUI.container[i].item != null)
        //    {
        //        data.containerItemsID.Add(theContainerUI.container[i].item.id);
        //        data.containerItemsCnt.Add(theContainerUI.container[i].item.count);
        //    }
        //    else
        //    {
        //        data.containerItemsID.Add(0);
        //        data.containerItemsCnt.Add(0);
        //    }
        //}
        for(int i = 0; i < theContainerItems.container.Count; i++)
        {
            if(theContainerItems.container[i] != null)
            {
                data.containerItemsID.Add(theContainerItems.container[i].id);
                data.containerItemsCnt.Add(theContainerItems.container[i].count);
            }
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
                thePlayerControll = FindObjectOfType<PlayerControll>();
                theSpawnManager = FindObjectOfType<SpawnManager>();
                theInventory = FindObjectOfType<inventory>();
                theUIInventory = FindObjectOfType<UIInventory>();
                theContainerDb = GameObject.Find("Canvas2").transform.Find("containerPanel").
                                                                        GetComponent<ContainerDb>();
                theContainerUI = GameObject.Find("Canvas2").transform.Find("containerPanel").
                                                                        GetComponent<ContainerUI>();///
                theContainerItems = FindObjectOfType<ContainerItems>();
                theMenuControl = FindObjectOfType<MenuControl>();

                Debug.Log("File 1 로드 시작 - 2");

                theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간아 흘러라

                vector.Set(data.playerX, data.playerY);
                thePlayerMove.transform.position = vector;

                theGameManager.day = data.day;
                theGameManager.timer = data.timer;
                theGameManager.stamina = data.stamina;

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
                /*bool isEquipedOK = false;
                int ind1 = 0;
                int trashID = 11; // 11, 12 (달걀) - 테스트
                List<int> trashList = new List<int>(); // 테스트
                for(int i = 0; i < data.invenUIEmpty.Count; i++) // 인벤템 불러오기
                {
                    if(data.invenUIEmpty[i] == false) // 인벤칸이 비어있지 않다면
                    {
                        theInventory.putInventory(data.characterItemsID[ind1], data.characterItemsCnt[ind1]);
                        if(data.characterItemsID[ind1] == data.equipedItemID)
                        {
                            // 인벤템과 저장된 장착템 ID가 같으면 장착 (인벤에 있는 거라면)
                            theInventory.equipedItem = theInventory.characterItems[ind1];
                            isEquipedOK = true;
                        }
                        if(ind1 < data.characterItemsID.Count - 1)
                        {
                            ind1 += 1;
                        }
                        else{   break;    }
                    }
                    else // 테스트 대상
                    {
                        theInventory.putInventory(trashID, 1);
                        trashList.Add(trashID);
                        trashID += 1;
                        
                    }
                } // 저장된 아이템 목록 하나씩 넣기 (개수 고려됨)
                for(int i = 0; i < trashList.Count; i++)
                {   theInventory.RemoveAll(trashList[i]);    }
                if(!isEquipedOK) // 인벤에 없다면 (= null or 클릭된 게 없음)
                {
                    theInventory.equipedItem = null;
                } // 장착템 설정*/
                // 아래로 수정
                bool isEquipedOK = false;
                bool notFisrt12 = false;
                int trashID = 11; // 11, 12 (달걀) - 테스트
                List<int> trashList = new List<int>(); // 테스트
                for(int i = 0; i < data.characterItemsID.Count; i++) // 인벤템 불러오기
                {
                    if(data.characterItemsID[i] > 0) // 인벤칸이 비어있지 않았다면
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
                        theInventory.putInventory(trashID, 1);
                        if(trashID == 11)
                        {
                            trashList.Add(trashID);
                            trashID += 1;
                        }
                        else if(trashID == 12 && notFisrt12 == false)
                        {
                            trashList.Add(trashID);
                            notFisrt12 = true;
                        }
                        
                    }
                } // 저장된 아이템 목록 하나씩 넣기 (개수 고려됨)
                for(int i = 0; i < trashList.Count; i++)
                {   theInventory.RemoveAll(trashList[i]);    }
                if(!isEquipedOK) // 인벤에 없다면 (= null or 클릭된 게 없음)
                {
                    theUIInventory.MakeSlotClear(); // 장착템 UI 초기화 (02/28 추가)
                    theInventory.equipedItem = null;
                } // 장착템 설정

                // 보관상자
                //GameObject.Find("Canvas2").transform.Find("containerPanel").gameObject.SetActive(true);
                for(int i = theContainerItems.container.Count - 1; i >= 0; i--)
                {
                    if(theContainerItems.container[i] != null)
                    {
                        //Debug.Log(theContainerItems.container[i].Ename + " 제거");
                        theContainerDb.RemoveItem(theContainerItems.container[i].id);
                    }
                } // 보관상자 아이템 비우기
                containerDatabase condb = FindObjectOfType<containerDatabase>();///
                theContainerUI.container.Clear();///
                if(data.containerItemsID.Count > 0)
                {
                    for(int i = 0; i < data.containerItemsID.Count; i++)
                    {
                        if(data.containerItemsID[i] > 0)
                        {
                            //theContainerDb.PutInContainer(data.containerItemsID[i], data.containerItemsCnt[i]);
                            ///
                            Item thisitem = condb.GetItem(data.containerItemsID[i]);
                            // 해당 아아템이 존재하지 않음 (from condb)
                            if(thisitem == null)
                            {
                                Debug.Log(data.containerItemsID[i] + " id의 아이템이 데이터베이스에 없음");
                            }
                            // 아이템을 처음 넣을 경우 (from condb)
                            if(theContainerItems.container.
                                            Find(item => item.id == data.containerItemsID[i]) == null)
                            {
                                thisitem.count = data.characterItemsCnt[i];
                                theContainerItems.container.Add(thisitem);
                                theContainerUI.AddNewItem(thisitem);
                            }
                            // 아이템이 이미 보관상자에 존재할 경우 (from condb)
                            else
                            {
                                thisitem.count += data.characterItemsCnt[i];
                                theContainerUI.UpdateUI(thisitem);
                                Debug.Log("실행이 안 되야 함! 보관상자 오류!");
                            }
                            ///
                        }
                    }
                }
                //GameObject.Find("Canvas2").transform.Find("containerPanel").gameObject.SetActive(false);
                // 일단은 빈칸 고려 안 하고 로드
                
                /* // 2021.02.23 임시로 제거 (저장템 부분 제외)
                //bool notFisrt122 = false;
                //int trashID2 = 11; // 11, 12 (달걀) - 테스트
                List<int> trashList2 = new List<int>(); // 테스트
                for(int i = theContainerDb.container.Count - 1; i >= 0; i--)
                {
                    theContainerDb.RemoveItem(theContainerDb.container[i].id);
                } // 상자 비우기
                theContainerUI.container.Clear();////
                if(data.containerItemsID.Count > 0) // 저장템이 존재할 때
                {
                    GameObject.Find("Canvas2").transform.Find("보관상자").gameObject.SetActive(true);///
                    for(int i = 0; i < data.containerItemsID.Count; i++)
                    {
                        if(data.containerItemsID[i] > 0) // 저장칸이 비어있지 않았다면
                        {
                            Debug.Log("저장 상자 아이템 로드 중 - 1"); // 테스트용
                            //theContainerDb.PutInContainer(data.containerItemsID[i], data.containerItemsCnt[i]);

                            /////Item item = db.GetItem(id);
                            //해당 아아템이 존재하지 않음
                            if (item == null) {
                                Debug.Log(id + " id 를 가진 아이템은 데이터베이스에 없습니다.");
                                return;
                            }
                            //아이템을 처음 넣을 경우
                            if (CheckForItem(id) == null)
                            {
                                item.count = num;
                                container.Add(item);
                                conUI.AddNewItem(item);
                                //Debug.Log(id + "라는 id를 가진 아이템을 보관상자에 추가합니다. ");
                            }
                            //아이템이 이미 보관상자에 존재할경우
                            else
                            {
                                item.count += num;
                                conUI.UpdateUI(item);
                                //Debug.Log(id + "라는 id를 가진 아이템을 " + num + "개 더 추가합니다.");
                            }/////
                            Item item = FindObjectOfType<containerDatabase>().GetItem(data.containerItemsID[i]);
                            if(item != null)
                            {   
                                if(theContainerDb.CheckForItem(data.containerItemsID[i]) == null)
                                {
                                    item.count = data.containerItemsCnt[i];
                                    theContainerDb.container.Add(item);
                                    theContainerUI.AddNewItem(item);
                                }
                                else
                                {
                                    item.count += data.containerItemsCnt[i];
                                    theContainerUI.UpdateUI(item);
                                }
                            }

                            Debug.Log("저장 상자 아이템 로드 중 - 2"); // 테스트용
                        }
                        /////else // 저장칸이 비어있다면
                        {
                            theContainerDb.PutInContainer(trashID2, 1);
                            if(trashID2 == 11)
                            {
                                trashList2.Add(trashID2);
                                trashID2 += 1;
                            }
                            else if(trashID2 == 12 && notFisrt122 == false)
                            {
                                trashList2.Add(trashID);
                                notFisrt122 = true;
                            }
                        } /////임시로 제거 (꼭 다시 넣어야 함!)
                    } // 상자에 넣기
                    for(int i = 0; i < trashList2.Count; i++)
                    {   theContainerDb.RemoveItem(trashList2[i]);    } // 쓰레기템 제거
                    GameObject.Find("Canvas2").transform.Find("보관상자").gameObject.SetActive(false);///
                }
                */

                Debug.Log("File 1 로드 완료");
            }
            file.Close(); // 파일 닫기
        }
    }

}
