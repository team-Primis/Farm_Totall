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
    private GameManager theGameManager; // 날짜, 시간, stamina
    private PlayerControll thePlayerControll; // 쓴 돈, laborCount
    private SpawnManager theSpawnManager; // 닭 개수/행복도/checkEgg/좌표, 달걀 개수/좌표
    private inventory theInventory; // 인벤템 ID, 인벤템 개수, 장착템 ID
    private UIInventory theUIInventory; // 인벤 스프라이트 (for inventory) + 인벤 UI 상태
    private ContainerDb theContainerDb; // 보관템 ID, 보관템 개수
    private ContainerUI theContainerUI; // 상자 스프라이트 (for ContainerDb)
    private MenuControl theMenuControl; // 일시정지 창 꺼놓기 위해서 - Load에만 사용

    public SNLData data; // SNLData 이용할 것임

    private Vector2 vector; // player 위치 불러올 곳

    public void CallNewGame()
    {
        //doNewGame = true; // 잠시 주석

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
        theContainerDb = GameObject.Find("Canvas2").transform.Find("보관상자").
                            transform.Find("ContainerPanel").GetComponent<ContainerDb>();
        /*theContainerUI = GameObject.Find("Canvas2").transform.Find("보관상자").
                            transform.Find("ContainerPanel").GetComponent<ContainerUI>();*/
        theMenuControl = FindObjectOfType<MenuControl>();

        Debug.Log("새 게임 로드 시작 - 2");

        theMenuControl.WhenRestart(); // 일시정지 창 끄기 & 시간아 흘러라

        thePlayerMove.transform.position = new Vector2(0, 0); // 플레이어 좌표 초기화

        theGameManager.day = 1; // DAY 초기화
        theGameManager.timer = 0; // 타이머 초기화
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
        theInventory.equipedItem = null; // 장착템 초기화

        for(int i = theContainerDb.container.Count -1; i >= 0; i--)
        {
            theContainerDb.RemoveItem(theContainerDb.container[i].id);
        } // 상자 아이템 비우기 (상자 초기화)

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
        theContainerDb = GameObject.Find("Canvas2").transform.Find("보관상자").
                            transform.Find("ContainerPanel").GetComponent<ContainerDb>();

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
        data.invenUIEmpty.Clear();
        for(int i = 0; i < theInventory.characterItems.Count; i++)
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
        {   data.equipedItemID = 0; }

        data.containerItemsID.Clear();
        data.containerItemsCnt.Clear();
        for(int i = 0; i < theContainerDb.container.Count; i++)
        {
            data.containerItemsID.Add(theContainerDb.container[i].id);
            data.containerItemsID.Add(theContainerDb.container[i].count);
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
                theContainerDb = GameObject.Find("Canvas2").transform.Find("보관상자").
                                    transform.Find("ContainerPanel").GetComponent<ContainerDb>();
                /*theContainerUI = GameObject.Find("Canvas2").transform.Find("보관상자").
                                    transform.Find("ContainerPanel").GetComponent<ContainerUI>();*/
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

                for(int i = theInventory.characterItems.Count - 1; i >= 0; i--)
                {
                    theInventory.RemoveAll(theInventory.characterItems[i].id);
                } // 인벤 아이템 비우기
                theUIInventory.MakeSlotNull(); // 인벤 UI 비우기
                bool isEquipedOK = false;
                int ind1 = 0;
                int trashID = 11; // 11, 12 (달걀) - 테스트
                List<int> trashList = new List<int>(); // 테스트
                for(int i = 0; i < data.invenUIEmpty.Count; i++) // 인벤템 불러오기
                {
                    if(data.invenUIEmpty[i] == false) // 인벤칸이 비어있지 않는다면
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
                } // 장착템 설정

                for(int i = theContainerDb.container.Count -1; i >= 0; i--)
                {
                    theContainerDb.RemoveItem(theContainerDb.container[i].id);
                } // 상자 아이템 비우기
                /*for(int i = 0; i < data.containerItemsID.Count; i++)
                {
                    theContainerDb.PutInContainer(data.containerItemsID[i], data.containerItemsCnt[i]);
                } // 상자에 넣기*/

                Debug.Log("File 1 로드 완료");
            }
            file.Close(); // 파일 닫기
        }
    }
}
