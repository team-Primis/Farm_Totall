using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerDb : MonoBehaviour
{
    // Start is called before the first frame update
    private itemDatabase db;

    //아이템을 담아놓을 보관상자
    public List<Item> container = new List<Item>();
    void Start()
    {
        db = GameObject.Find("Database").GetComponent<itemDatabase>();
        PutInContainer(1);
        PutInContainer("pumpkin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //해당 id를 가진 아이템을 보관상자에 넣음
    public void PutInContainer(int id)
    {
        Item item = db.GetItem(id);
        if (item == null) {
            Debug.Log(id + " id 를 가진 아이템은 데이터베이스에 없습니다.");
            return;
        }
        container.Add(item);
        Debug.Log(id + "이라는 id를 가진 아이템을 보관상자에 추가합니다. ");
    }

    public void PutInContainer(string name)
    {
        Item item = db.GetItem(name);
        if(item == null)
        {
            Debug.Log(name + " 이라는 이름을 가진 아이템은 데이터베이스에 없습니다.");
            return;
        }
        container.Add(item);
        Debug.Log(name + "이라는 이름을 가진 아이템을 보관상자에 추가합니다. ");
    }
}
