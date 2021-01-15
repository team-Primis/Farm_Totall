using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    //실제 유저가 가지고 있는 내부 인벤토리에 대한 코드
    public List<Item> characterItems = new List<Item>();
    private itemDatabase db;
    public UIInventory inventoryUI;

    public Item equipedItem;

    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        putInventory(0);
        putInventory(1);
        putInventory(1);
        putInventory(1);
        RemoveItem(1);
        putInventory(2);
        putInventory(3);
        RemoveItem(0);
        Debug.Log("인벤에서 본 모습 : " + characterItems.Count);
        //아이템 획득한 걸 반영하고 싶다면 인벤토리 스크립트 참조하고 
        //인벤토리스크립트이름.putInventory(아이템코드) 쓰면돼!
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //한번 누르면 열리고 한번 더 누르면 닫힘
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }

    //이 아이디를 가진 아이템을 인벤에 넣을떄 쓰는 함수.
    //해당 id가 아이템 데이터베이스에 존재하는지 확인하고 존재 시 인벤(characterItems)에 넣는다
    public void putInventory(int id)
    {
        //db 확인
        Item itemToAdd = db.GetItem(id);
        //없을경우
        if(itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        //있을 경우
        //캐릭터 인벤에 아이템 추가, 외부 인벤에 그림 바꿈(빈 객체가 있는 칸을 해당 객체의 그림으로 바꿈)

        //해당 아이템을 처음 추가하는 경우
        if (itemToAdd.count == 0)
        {
            itemToAdd.count++;
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);

            Debug.Log("Added item : " + itemToAdd.Kname);
        }
        else
        {
            itemToAdd.count++;
            inventoryUI.UpdateItemNumUI(itemToAdd);
            Debug.Log("Item " + itemToAdd.Ename + "count became " + itemToAdd.count);
        }

    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item ItemToRemove = CheckForItem(id);
        if(ItemToRemove != null)
        {
            //아이템이 하나밖에 없었는데 제거함 -> 아예 객체 제거
            if (ItemToRemove.count == 1)
            {
                characterItems.Remove(ItemToRemove);
                Debug.Log("Item removed : " + ItemToRemove.Kname);
                inventoryUI.RemoveItem(ItemToRemove);
            }
            else
            {
                ItemToRemove.count--;
                inventoryUI.UpdateItemNumUI(ItemToRemove);
                Debug.Log("Item 하나 제거. 현재 갯수 : " + ItemToRemove.count);
            }
        }
        else
        {
            Debug.Log("아이디가 "+id+"인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }


}
