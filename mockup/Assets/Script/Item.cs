using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class Item
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
        public ITEMTYPE type = ITEMTYPE.NONE;
        public string name = "";
        public long itemCode;
        public string description = "";
        public int count = 0;

        public Item () {
        }

        public Item(ITEMTYPE type, string name, long itemCode, string description, int count)
        {
            this.type = type;
            this.name = name;
            this.itemCode = itemCode;
            this.description = description;
            this.count = count;
        }
    }
}
