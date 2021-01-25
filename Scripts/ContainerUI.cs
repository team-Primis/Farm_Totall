using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public List<UIItem> container = new List<UIItem>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject itemslots in GameObject.FindGameObjectsWithTag("containerSlot"))
        {
            //container.Add(itemslots.GetComponent<UIItem>);
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        container[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(container.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(container.FindIndex(i => i.item == item), null);
    }
}
