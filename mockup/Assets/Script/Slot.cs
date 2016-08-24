using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts;

public class Slot : MonoBehaviour, IDropHandler {

    private Inventory inv;
    public int id;

    void Start()
    {
        inv = GameObject.Find("ItemDatabase").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if (inv.content[id].ID == -1)
        {
            inv.content[droppedItem.slotID] = new Item();
            droppedItem.slotID = id;
            inv.content[id] = droppedItem.item;
        } else if (droppedItem.slotID != id)
        {
            // save old item
            Transform item = this.transform.GetChild(0);

            // give the old item the dropped items information
            item.GetComponent<ItemData>().slotID = droppedItem.slotID;
            item.transform.SetParent(inv.slots[droppedItem.slotID].transform);
            item.transform.position = inv.slots[droppedItem.slotID].transform.position;

            droppedItem.slotID = id;
            //droppedItem.transform.SetParent(this.transform);
            //droppedItem.transform.position = this.transform.position;

            inv.content[item.GetComponent<ItemData>().slotID] = item.GetComponent<ItemData>().item;
            inv.content[id] = droppedItem.item;
        }
    }
}
