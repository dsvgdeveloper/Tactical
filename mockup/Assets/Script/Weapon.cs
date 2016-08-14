using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Weapon : Item
    {
        public Weapon()
        {
            this.type = ITEMTYPE.WEAPON;
        }
    }
}
