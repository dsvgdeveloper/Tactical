using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Assets.Scripts;
using System.IO;
using System;

public class ItemDataBase : MonoBehaviour {
    List<Item> database = new List<Item>();
    //Dictionary<string, Item> database = new Dictionary<string, Item>();

    JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "\\StreamingAssets\\Items.json"));
        ConstructItemDatabase();
    }

    public void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            int type = (int)itemData[i]["type"];
            switch(type)
            {
                case 0:
                    addDBPotion(i);
                    break;
                case 1:
                    addDBHead(i);
                    break;
                case 2:
                    addDBBody(i);
                    break;
                case 3:
                    addDBArm(i);
                    break;
                case 4:
                    addDBFoot(i);
                    break;
                case 5:
                    addDBRing(i);
                    break;
                case 6:
                    addDBTrinket(i);
                    break;
                case 7:
                    addDBWeapon(i);
                    break;
                case 8:
                    addDBMaterial(i);
                    break;
                case 9:
                    addDBMisc(i);
                    break;
                default:
                    // throw error thing
                    break;
            };
        }
    }

    private void addDBMisc(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(),
            new Consumable(
                ITEMTYPE.MISC,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBMaterial(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(),
            new Consumable(
                ITEMTYPE.MISC,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBTrinket(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Trinket(
                ITEMTYPE.TRINKET,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBRing(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Ring(
                ITEMTYPE.RING,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBFoot(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Feet(
                ITEMTYPE.FOOT,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBArm(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Arm(
                ITEMTYPE.ARM,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBBody(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Body(
                ITEMTYPE.BODY,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBHead(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Head(
                ITEMTYPE.HEAD,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBPotion(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Consumable(
                ITEMTYPE.POTION,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    private void addDBWeapon(int i)
    {
        database.Add(
            //itemData[i]["id"].ToString(), 
            new Weapon(
                ITEMTYPE.WEAPON,
                itemData[i]["name"].ToString(),
                (int)itemData[i]["id"],
                itemData[i]["description"].ToString(),
                (int)itemData[i]["value"],
                Boolean.Parse(itemData[i]["stackable"].ToString()),
                (int)itemData[i]["rarity"],
                itemData[i]["slug"].ToString())
        );
    }

    public Item fetchItemByID(long id)
    {
        Item ret;
        //if (!database.TryGetValue (id, out ret)) return null;

        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID.Equals(id))
            {
                ret = database[i];
                return ret;
            }
        }
        
        return null;
    }
}
