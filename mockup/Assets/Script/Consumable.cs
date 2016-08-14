using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Consumable : Item
    {
        public Consumable (ITEMTYPE type)
        {
            this.type = type;
        }
    }
}
