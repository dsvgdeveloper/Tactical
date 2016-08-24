using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Feet : Item
    {
        public Feet()
        {
            this.type = ITEMTYPE.FOOT;
        }

        public Feet(ITEMTYPE type, string name, long itemCode, string description, int value,
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
