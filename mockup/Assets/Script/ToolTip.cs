using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using System;

public class ToolTip : MonoBehaviour {
    private Item item;
    private string data;
    public GameObject toolTip;

	// Use this for initialization
	void Start () {
        toolTip = GameObject.Find("ToolTip");
        toolTip.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (toolTip.activeSelf)
        {
            toolTip.transform.position = Input.mousePosition;
        }
	}

    public void Activate(Item item)
    {
        this.item = item;
        toolTip.SetActive(true);
        ConstructDataString();
    }

    public void Deactivate()
    {
        toolTip.SetActive(false);
    }

    public void ConstructDataString()
    {
        switch(item.RARITY)
        {
            case 0:
                data = "<color=#c7c7c7><b>";
                break;
            case 1:
                data = "<color=#ffffff><b>";
                break;
            case 2:
                data = "<color=#40ff00><b>";
                break;
            case 3:
                data = "<color=#0015ff><b>";
                break;
            case 4:
                data = "<color=#c000ff><b>";
                break;
            case 5:
                data = "<color=#ff8100><b>";
                break;
            case 6:
                data = "<color=#ffea00><b>";
                break;
            case 7:
                data = "<color=#00BFFF><b>";
                break;
            case 8:
                data = "<color=#ff0000><b>";
                break;
            case 9:
                data = "<color=#000000><b>";
                break;
            default:
                data = "<color=#FC90d5><b>";
                break;
        }

        data += item.NAME + "</b></color>\n\n" + item.description +"\n\n";

        switch (item.type)
        {
            case ITEMTYPE.POTION:
                ConstructDataStringPotion();
                break;
            case ITEMTYPE.HEAD:
                ConstructDataStringHead();
                break;
            case ITEMTYPE.BODY:
                ConstructDataStringBody();
                break;
            case ITEMTYPE.ARM:
                ConstructDataStringArm();
                break;
            case ITEMTYPE.FOOT:
                ConstructDataStringFoot();
                break;
            case ITEMTYPE.RING:
                ConstructDataStringRing();
                break;
            case ITEMTYPE.TRINKET:
                ConstructDataStringTrinket();
                break;
            case ITEMTYPE.WEAPON:
                ConstructDataStringWeapon();
                break;
            case ITEMTYPE.MATERIAL:
                ConstructDataStringMaterial();
                break;
            case ITEMTYPE.MISC:
                ConstructDataStringMisc();
                break;
            default:
                // throw error thing
                break;
        };

        data += "\n\n" + item.VALUE;

        toolTip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

    private void ConstructDataStringMisc()
    {
        data += "specific misc item type information goes here";
    }

    private void ConstructDataStringMaterial()
    {
        data += "specific material item type information goes here";
    }

    private void ConstructDataStringWeapon()
    {
        data += "specific weapon item type information goes here";
    }

    private void ConstructDataStringTrinket()
    {
        data += "specific trinket item type information goes here";
    }

    private void ConstructDataStringRing()
    {
        data += "specific ring item type information goes here";
    }

    private void ConstructDataStringFoot()
    {
        data += "specific foot item type information goes here";
    }

    private void ConstructDataStringArm()
    {
        data += "specific arm item type information goes here";
    }

    private void ConstructDataStringBody()
    {
        data += "specific body item type information goes here";
    }

    private void ConstructDataStringHead()
    {
        data += "specific head item type information goes here";
    }

    private void ConstructDataStringPotion()
    {
        data += "specific potion item type information goes here";
    }
}