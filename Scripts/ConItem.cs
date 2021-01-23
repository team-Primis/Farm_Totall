using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConItem : MonoBehaviour, IPointerClickHandler
{

    Item item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            Debug.Log(item.Ename + " 아이템을 클릭하셨습니다.");
        }
        else
        {
            Debug.Log("해당칸은 비었군요!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
