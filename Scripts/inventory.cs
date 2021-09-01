using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    //실제 유저가 가지고 있는 내부 인벤토리에 대한 코드
    private itemDatabase db;
    private ContainerDb container;
    private SellingUI sellingUI;


    public List<Item> characterItems = new List<Item>();
    public UIInventory inventoryUI;

    public Button putInBtn;

    public Item equipedItem;

    public bool isUIItemClickChanged = false;
    public Transform emptrans;

    public NoticeText notice;


    public Stemina stM;
    public static inventory instance = null;

    //Item emptyItem;
    public Item emptyItem; // 장착템 로드 때문에 public으로 바꿈 (0304 성현)

    private UIItem selectedItem;
    private ContainerItems containerItemAddScript; //컨테이너의 아이템과, 아이템 추가 스크립트
    //소리추가
    AudioSource audioSource;
    public AudioClip eatingSound;

    private ContainerUI conUI;

    public int invenCount = 14;



    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        container = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerDb>();
        sellingUI = GameObject.Find("Canvas2").transform.Find("sellingPanel").GetComponent<SellingUI>();
        stM = GameObject.Find("Canvas2").transform.Find("Slider").GetComponent<Stemina>();
        putInBtn.onClick.AddListener(PutInContainer);

        audioSource = GameObject.Find("SoundEffect").GetComponent<AudioSource>();
       
      
        

        //안내사항 띄우고 싶을땐 이렇게 쓰기
        notice = GameObject.Find("Notice").GetComponent<NoticeText>();

        emptyItem = new Item(1000, "없음", "empty", " ", Item.Category.empty);

        //아이템 획득한 걸 반영하고 싶다면 인벤토리 스크립트 참조하고 
        //인벤토리스크립트이름.putInventory(아이템코드) 쓰면돼!

        selectedItem = GameObject.Find("selectedItem").GetComponent<UIItem>();

        //보관상자에 아이템 넣기 관련 스크립트
        containerItemAddScript = GameObject.Find("ContainerItems").GetComponent<ContainerItems>();
        equipedItem = emptyItem;

        conUI = GameObject.Find("Canvas2").transform.Find("containerPanel").GetComponent<ContainerUI>();

    }

    void Awake()
    {
        //한개만 존재하기 위함 ( 싱글턴 )
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

 
     void PutInContainer()
    {
        if (equipedItem != emptyItem)
        {
            //컨테이너에 넣으면 그만큼 인벤db의 갯수가 줄어든다.
            //db의 갯수정보를 데려와서 장착된 아이템만큼 줄이기!
            //컨테이너아이템에 해당 갯수만큼 아이템 추가
            Item dbItem = db.GetItem(equipedItem.id);
            Debug.Log("현재 db에 아이템은 " + dbItem.count + "개 존재합니다");

            //컨테이너에 아이템 추가 : 새 아이템을 만들어서 넘겨야됨! (인벤토리의 아이템은 갯수를 0개로 바꿀거라서 / 별도의 객체라서)
            Item item = new Item(equipedItem.id, equipedItem.Kname, equipedItem.Ename, equipedItem.description, equipedItem.category,equipedItem.stats);
            item.count = equipedItem.count;

            containerItemAddScript.PutInContainer(item, item.count);
            //인벤에서 아이템 삭제
            characterItems.Remove(equipedItem);
            inventoryUI.RemoveItem(equipedItem);
            conUI.isContainerChanged = true;

               // RemoveAll(equipedItem); //여기서 equpedItem의 갯수를 0으로 바꿔버리더라..
               //인벤db에서 아이템 삭제
            dbItem.count -= equipedItem.count;
            equipedItem = emptyItem;
            ClearSlot();
            
        }
        else
        {
            Debug.Log("선택된 아이템이 없어 넣을 수 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //I키를 눌렀을때 열리고 닫히게 조절 가능
        if (Input.GetKeyDown(KeyCode.I))
        {
            //한번 누르면 열리고 한번 더 누르면 닫힘
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }

       
        //E키를 눌렀을떄 장착된 아이템이 아이템 카테고리 라면 ( 사용 가능한 아이템 ) 아이템을 사용한다.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (equipedItem != emptyItem) // 장착된 상태 
            {
                //아이템의 id가 10보다 클때만 사용가능 ( 즉 씨앗 빼고 사용가능)
                if (equipedItem.category == Item.Category.item &&equipedItem.id>10)
                {
                    UseItem();
                 
                }
            }
        }
        //장착한 아이템에서 f키를 누르면 1개 분리
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (equipedItem.category == Item.Category.item)
            {
                PutSplitedItem(equipedItem);
            }
        }

     


    }
    public void renewSlot()
    {
        inventoryUI.MakeSlotNull();
    }

    public void MoveSlot(Transform trans)
    {
        inventoryUI.MoveEmphasizedSlot(trans);
    }

    public void ClearSlot()
    {
        inventoryUI.MakeSlotClear();
    }

    public Item GetEqippedITem()
    {
        return equipedItem;
    }
    public void PutSplitedItem(int id)
    {
        //db 확인
        Item item = db.GetItem(id);

        //없을경우
        if (item == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        if(item.count <= 1)
        {
            Debug.Log("1개 미만이라 쪼갤 수 없습니다.");
            return;
        }
        //어차피 인벤토리는 변하지 않고, 눈에 보이고 만지는 UI쪽만 바뀌는것.

        
        item.count--;
        inventoryUI.UpdateItemNumUI(item); //item 1개 갯수 준거 반영

        Item itemToAdd = new Item(item.id, item.Kname, item.Ename, item.description, item.category,item.stats);
        itemToAdd.count = 1;
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        sellingUI.isItemChanged = true;


    }
    //바로 equippedItem을 split할거임 (DB 안건듦)
    public void PutSplitedItem(Item item)
    {
        
        if (item.count <= 1)
        {
            Debug.Log("1개 미만이라 쪼갤 수 없습니다.");
            return;
        }
        if(characterItems.Count >= invenCount)
        {
            Debug.Log("인벤토리에 빈 공간이 없어서 쪼갤 수 없습니다.");
            return;
        }


        //기존의 인벤토리 아이템 삭제
        characterItems.Remove(equipedItem);
        inventoryUI.RemoveItem(equipedItem);

        //clone만들어서 ( 1개 작은거) 원래 위치에 놓음
        Item itemToAdd = new Item(item.id, item.Kname, item.Ename, item.description, item.category, item.stats);
        itemToAdd.count = equipedItem.count-1;
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);

        //1개짜리 clone 만들어서 넣음
        Item itemSplited = new Item(item.id, item.Kname, item.Ename, item.description, item.category, item.stats);
        itemSplited.count = 1;
        characterItems.Add(itemSplited);
        inventoryUI.AddNewItem(itemSplited);

        //장착 아이템 초기화
        equipedItem = emptyItem;
        ClearSlot();

        sellingUI.isItemChanged = true;


    }


    //다시 로드용. id가 같아도 따로 생성
    public void putAgainInventory(int id, int num = 1)
    {
        //db 확인
        Item itemToAdd = db.GetItem(id);

        //없을경우
        if (itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        //db에 갯수추가
        itemToAdd.count += num;

     
        Item item = new Item(itemToAdd.id, itemToAdd.Kname, itemToAdd.Ename, itemToAdd.description, itemToAdd.category, itemToAdd.stats);
        item.count = num;
        characterItems.Add(item);
        inventoryUI.AddNewItem(item);
    }


    //이 아이디를 가진 아이템을 인벤에 넣을떄 쓰는 함수.
    //해당 id가 아이템 데이터베이스에 존재하는지 확인하고 존재 시 인벤(characterItems)에 넣는다
    public void putInventory(int id, int plusNum = 1)
    {
        //db 확인
        Item itemToAdd = db.GetItem(id);

        //없을경우
        if (itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        //해당 아이템을 처음 추가하는 경우 : 인벤토리를 확인했을 때 해당 객체가 없음
        if (CheckForItem(id) == null)
        {
            itemToAdd.count += plusNum;
            Item item = new Item(itemToAdd.id, itemToAdd.Kname, itemToAdd.Ename, itemToAdd.description, itemToAdd.category, itemToAdd.stats);
            item.count = plusNum;
            characterItems.Add(item);
            inventoryUI.AddNewItem(item);

            // Debug.Log("아이템 " + itemToAdd.Kname+"을 "+itemToAdd.count+"개 추가합니다.");
        }
        //해당 아이템 객체가 이미 있음
        else
        {
            //db에 갯수추가
            itemToAdd.count += plusNum;

            //첫번째로 위치한 아이템의 카운트 추가
            Item item = CheckForItem(id);
            item.count += plusNum;
            Debug.Log(item.Kname + "을 " + item.count + "개로 만듭니다");
            inventoryUI.UpdateItemNumUICount(item);
            //   Debug.Log("아이템" + itemToAdd.Kname + "의 갯수를 "+plusNum+"개 추가해서 현재 갯수는 " + itemToAdd.count+"개 입니다.");
        }
        sellingUI.isItemChanged = true;

    }
    //자판기에서 씨앗 이름으로 접근해야 해서 overloading
    public void putInventory(string name, int plusNum = 1)
    {
        //db 확인
        Item itemToAdd = db.GetItem(name);

        //없을경우
        if (itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        //있을 경우
        //캐릭터 인벤에 아이템 추가, 외부 인벤에 그림 바꿈(빈 객체가 있는 칸을 해당 객체의 그림으로 바꿈)

        //해당 아이템을 처음 추가하는 경우 : 인벤토리를 확인했을 때 해당 객체가 없음
        if (CheckForItem(name) == null)
        {
            itemToAdd.count += plusNum;
            Item item = new Item(itemToAdd.id, itemToAdd.Kname, itemToAdd.Ename, itemToAdd.description, itemToAdd.category, itemToAdd.stats);
            item.count = plusNum;
            characterItems.Add(item);
            inventoryUI.AddNewItem(item);

            // Debug.Log("아이템 " + itemToAdd.Kname+"을 "+itemToAdd.count+"개 추가합니다.");
        }
        //해당 아이템 객체가 이미 있음
        else
        {
            //db에 갯수추가
            itemToAdd.count += plusNum;

            //첫번째로 위치한 아이템의 카운트 추가
            Item item = CheckForItem(name);
            item.count+= plusNum;
            Debug.Log(item.Kname + "을 " + item.count + "개로 만듭니다");
            inventoryUI.UpdateItemNumUICount(item);
            //   Debug.Log("아이템" + itemToAdd.Kname + "의 갯수를 "+plusNum+"개 추가해서 현재 갯수는 " + itemToAdd.count+"개 입니다.");
        }
        sellingUI.isItemChanged = true;

    }


    //컨테이너에서 인벤토리로 아이템 보내기
    public void putInventory(Item item, int plusNum = 1)
    {
        Item invenItem = db.GetItem(item.id);

        //혹시 해당 아이템이 db에 아예 id조차 없다면 실행 안됨!
        if (invenItem != null)
        {
            //해당 아이템이 인벤에 있든 없든 새로 추가하기.
           
            //db에 갯수 반영
            //인벤토리 list에 다시 추가
            //UI에도 반영
                invenItem.count += plusNum;
            Item newItem = new Item(item.id, item.Kname, item.Ename, item.description, item.category,item.stats);
            newItem.count = plusNum;
                characterItems.Add(newItem);
                inventoryUI.AddNewItem(newItem);

            //판매창에 인벤토리 UI변경 모습 반영
            sellingUI.isItemChanged = true;
        }
        else
        {
            Debug.Log("해당 아이템은 db에 존재하지 않습니다.");
        }
    }


    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return characterItems.Find(item => item.Ename == name);
    }
    
    public void RemoveAll(int id)
    {
        Item Item = CheckForItem(id);
        if (Item != null)
        {
            Item.count = 0;
            characterItems.Remove(Item);
            //Debug.Log("아이템 " + Item.Kname + "을 인벤토리에서 제거합니다.");
            inventoryUI.RemoveItem(Item);
            sellingUI.isItemChanged = true;
            //만약 삭제한 아이템이 장착하고 있던 아이템이었으면, 장착된 곳에서 삭제하기
            if(equipedItem != emptyItem && equipedItem.id == id)
            {
                equipedItem = emptyItem;
                ClearSlot();
            }
        }
        else
        {
            Debug.Log("아이디가 " + id + "인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }

    //바로 그 아이템 지우기 용
    public void RemoveAll(Item item)
    {
        if(item != emptyItem)
        {
            //item.count = 0;
            characterItems.Remove(item);
            inventoryUI.RemoveItem(item);
            sellingUI.isItemChanged = true;
        }
        if (equipedItem != emptyItem && equipedItem.id == item.id)
        {
            equipedItem = emptyItem;
            ClearSlot();
        }
    }

    //해당 id를 가진 아이템 1개 제거 : 아이템의 갯수를 1개씩 줄이기. 아이템이 1개-> 0개가 되었을때는 제거.
    public void RemoveItem(int id)
    {
        Item ItemToRemove = CheckForItem(id);
        if (ItemToRemove != null)
        {
            //아이템이 하나밖에 없었는데 제거함 -> 아예 객체 제거
            if (ItemToRemove.count == 1)
            {
                RemoveAll(id);

            }
            else
            {
                ItemToRemove.count--;
                inventoryUI.UpdateItemNumUI(ItemToRemove);
                //Debug.Log("Item 하나를 제거합니다. 현재 남은 갯수 : " + ItemToRemove.count+"개");
            }

        }
        else
        {
            Debug.Log("아이디가 " + id + "인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }
    //바로 그 아이템을 지우기 위한 함수
    private void RemoveItem(Item item)
    {
        if(item != emptyItem)
        {
            db.GetItem(item.id).count--;
            if (item.count == 1)
            {
                RemoveAll(item);

            }
            else
            {
                item.count--;
                inventoryUI.UpdateItemNumUICount(item);
                //Debug.Log("Item 하나를 제거합니다. 현재 남은 갯수 : " + ItemToRemove.count+"개");
            }
        }
    }
    

    public Item GetItem(int index)
    {
        if (index <= characterItems.Count - 1)
        {
            return characterItems[index];
        }
        else
        {
            return emptyItem;
        }
    }

    //아이템 사용 : 아이템일 경우 하나 사라짐
    public void UseItem(Item item)
    {
        if (item != null&&item.Ename!= "empty" && item.count > 0)
        {
            RemoveItem(item.id);
            
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");
            if (item.stats.ContainsKey("recovery"))
            {
                int recoveryint = item.stats["recovery"];
                stM.FillHp((float)recoveryint);
                Debug.Log("체력이 " + recoveryint + "만큼 회복됩니다.");
            }
        }
        
    }
    //들고 있는 아이템 사용 용
    //이거 원래 방식은, 같은 id를 가진 객체 찾아서 1개 사용하기였음.
    //근데 분리기능을 넣으니까, 내가 선택한 바로 그 객체 말고 다른 객체가 없어질 수도 있다.
    public void UseItem()
    {
        Item item = equipedItem;
        if(item != emptyItem && item.count > 0)
        {
           
            RemoveItem(item);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");
            if (item.stats.ContainsKey("recovery"))
            {
                int recoveryint = item.stats["recovery"];
                stM.FillHp((float)recoveryint);
                Debug.Log("체력이 " + recoveryint + "만큼 회복됩니다.");
            }
            audioSource.clip = eatingSound;
            audioSource.Play();
        }

    }
   
    // 0개 이상이면 할거
    public void UseItem(string name)
    {
        Item item = db.GetItem(name);
        if (item != null && item.Ename != "empty" && item.count>0)

        {
            RemoveItem(item);
            
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");
            if(item.stats.ContainsKey("recovery"))
            {
                int recoveryint = item.stats["recovery"];
                stM.FillHp((float)recoveryint);
                Debug.Log("체력이 " + recoveryint + "만큼 회복됩니다.");
            }
            
        }
    }

    public void UseItem(int id)
    {
        Item item = db.GetItem(id);
        if (item != null && item.Ename != "empty" && item.count > 0)
        {
            RemoveItem(item);
            //아이템 사용 효과 넣기~~
            Debug.Log(item.Kname + "을 사용하셨습니다.");
            sellingUI.isItemChanged = true;
            notice.WriteMessage(item.Kname + "을 사용하셨습니다.");
            if (item.stats.ContainsKey("recovery"))
            {
                int recoveryint = item.stats["recovery"];
                stM.FillHp((float)recoveryint);
                Debug.Log("체력이 " + recoveryint + "만큼 회복됩니다.");
            }

        }
        else
        {
            Debug.Log("db에 해당 id가 없습니다.");
        }
    }

    public void AddItem(int id)
    {
        Item item = db.GetItem(id);
        if(item != null)
        {
            if(item.count > 1)
            {
                putInventory(id, 1);
            }
           
        }
        else
        {
            Debug.Log("id가 " + id + "인 아이템이 데이터베이스에 존재하지 않습니다.");
        }
    }

   
    
}
