using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerDb : MonoBehaviour
{
    // Start is called before the first frame update
    private itemDatabase db;
    public  ContainerUI conUI;

    //빼려고 선택된 아이템
    public Item selectedItem; 

    //아이템을 담아놓을 보관상자
    public List<Item> container = new List<Item>();
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
       
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           
            PutInContainer("pumpkin");
        }
    }

    //해당 id를 가진 아이템을 보관상자에 넣음
    public void PutInContainer(int id)
    {
        Item item = db.GetItem(id);
         //해당 아아템이 존재하지 않음
        if (item == null) {
            Debug.Log(id + " id 를 가진 아이템은 데이터베이스에 없습니다.");
            return;
        }
       
        container.Add(item);
        conUI.AddNewItem(item);
        Debug.Log(id + "이라는 id를 가진 아이템을 보관상자에 추가합니다. ");
    }

    public void PutInContainer(string name, int num=1)
    {
        Item item = db.GetItem(name);
        //존재하지 않는 아이템을 넣으려고 했을 경우
        if(item == null)
        {
            Debug.Log(name + " 이라는 이름을 가진 아이템은 데이터베이스에 없습니다.");
            return;
        }
        //아이템을 처음 넣을 경우
        if (CheckForItem(name) == null)
        {
            item.count = num;
            container.Add(item);
            conUI.AddNewItem(item);
            Debug.Log(name + "이라는 이름을 가진 아이템을 보관상자에 추가합니다. ");
        }
        //아이템이 이미 보관상자에 존재할경우
        else
        {
            item.count += num;
            conUI.UpdateUI(item);
            Debug.Log(name + "이라는 아이템을 " + num + "개 더 추가합니다.");
        }
    }

    public Item CheckForItem(int id)
    {
        return container.Find(item => item.id == id);
    }

    public Item CheckForItem(string name)
    {
        return container.Find(item => item.Ename == name);
    }
}
