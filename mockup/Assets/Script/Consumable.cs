using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Consumable : Item
    {
        public Consumable (ITEMTYPE type)
        {
            this.type = type;
        }

        public Consumable(ITEMTYPE type, string name, long itemCode, string description, int value,
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
