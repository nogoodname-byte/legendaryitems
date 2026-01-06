using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;


namespace legendaryitems.config
{
    public class Sstickconfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public Vector2 ChargeBarPosition = new Vector2(400, 90);
    }
}
