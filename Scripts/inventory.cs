using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    //실제 유저가 가지고 있는 내부 인벤토리에 대한 코드
    public List<Item> characterItems = new List<Item>();
    private itemDatabase db;

    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        putInventory(0);
        putInventory(1);

        //아이템 획득한 걸 반영하고 싶다면 인벤토리 스크립트 참조하고 
        //인벤토리스크립트이름.putInventory(아이템코드) 쓰면돼!
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //이 아이디를 가진 아이템을 인벤에 넣을떄 쓰는 함수.
    //해당 id가 아이템 데이터베이스에 존재하는지 확인하고 존재 시 인벤(characterItems)에 넣는다
    public void putInventory(int id)
    {
        Item itemToAdd = db.GetItem(id);
        if(itemToAdd == null)
        {
            Debug.Log("해당 item id가 데이터베이스에 없습니다.");
            return;
        }
        characterItems.Add(itemToAdd);
        Debug.Log("Added item : " + itemToAdd.name);

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
            characterItems.Remove(ItemToRemove);
            Debug.Log("Item removed : " + ItemToRemove.name);
        }
        else
        {
            Debug.Log("아이디가 "+id+"인 아이템은 인벤토리에 존재하지 않습니다.");
        }
    }


}
