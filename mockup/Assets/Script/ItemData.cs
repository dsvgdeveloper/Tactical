using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, 
                        IBeginDragHandler, 
                        IDragHandler, 
                        IEndDragHandler, 
                        IPointerDownHandler, 
                        IPointerEnterHandler, 
                        IPointerExitHandler
{
    public Item item;
    Vector2 offset;
    Inventory inv;
    private ToolTip toolTip;
    public int count;
    public int slotID;

    void Start()
    {
        inv = GameObject.Find("ItemDatabase").GetComponent<Inventory>();
        toolTip = inv.GetComponent<ToolTip>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slotID].transform);
        this.transform.position = inv.slots[slotID].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.Deactivate();
    }
}
