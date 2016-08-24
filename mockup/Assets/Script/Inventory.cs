using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;

    ItemDataBase itemDatabase;

    public GameObject inventorySlot;
    public GameObject inventoryItem;

    [SerializeField]
    public List<Item> content = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public int slotCount = 16;

    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;

        itemDatabase = GetComponent<ItemDataBase>();

        //itemDatabase.ConstructItemDatabase();

        for (int i = 0; i < slotCount; i++)
        {
            content.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }



        addItem(0);
        addItem(0);
        addItem(0);
        addItem(0);
        addItem(1);
        addItem(2);
        addItem(3);
        addItem(4);
        addItem(5);
        addItem(5);
        addItem(5);
        addItem(5);
        addItem(5);
        addItem(5);
        addItem(5);
        addItem(5);
        addItem(6);
        addItem(7);
        addItem(8);
        addItem(9);
        addItem(10);
    }

    public void addItem(long id)
    {
        // fix errors and slugs and item constructors
        Item itemToAdd = itemDatabase.fetchItemByID(id);

        if (itemToAdd.STACKABLE && checkIfItemIsInInventory(itemToAdd))
        {
            for (int i = 0; i < content.Count; i++)
            {
                if (itemToAdd.ID == content[i].ID)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.count++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.count.ToString();
                    return;
                }
            }
        }

        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].ID == -1)
            {
                content[i] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slotID = i;
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.localPosition = Vector2.zero;
                itemObj.GetComponent<Image>().sprite = itemToAdd.SPRITE;
                //itemObj.transform.GetChild(0).GetComponent<ItemData>().count = 1;
                slots[i].name = "SlotFor "+ itemToAdd.NAME;
                break;
            }
        }
    }

    bool checkIfItemIsInInventory(Item item)
    {
        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }
}
