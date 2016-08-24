using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public enum ITEMTYPE
    {
        NONE = -1,
        POTION = 0,
        HEAD = 1,
        BODY = 2,
        ARM = 3,
        FOOT = 4,
        RING = 5,
        TRINKET = 6,
        WEAPON = 7,
        MATERIAL = 8,
        MISC = 9
    }

    [Serializable]
    public class Item
    {
        public ITEMTYPE type = ITEMTYPE.NONE;
        public string NAME { get; set; }
        public long ID { get; set; }
        public string description { get; set; }
        public int VALUE { get; set; }
        public int RARITY { get; set; }
        public bool STACKABLE { get; set; }
        public string SLUG { get; set; }

        public Sprite SPRITE { get; set; }

        public Item () {
            ID = -1;
            NAME = "";
            type = ITEMTYPE.NONE;
            description = "";
            VALUE = -1;
            SLUG = "cursorHand_grey";
            SPRITE = Resources.Load<Sprite>(SLUG);
        }

        public Item(ITEMTYPE type, string name, long itemCode, string description, int value,
                    bool stackable, int rarity, string slug)
        {
            this.type = type;
            this.NAME = name;
            this.ID = itemCode;
            this.description = description;
            this.VALUE = value;
            STACKABLE = stackable;
            RARITY = rarity;
            SLUG = slug;

            this.SPRITE = Resources.Load<Sprite>(SLUG);
        }
    }
}
