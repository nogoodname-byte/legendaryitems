using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace legendaryitems.config
{
    public class Dropsconfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Craft Instead of Drop")]
        [DefaultValue(true)]
        public bool CraftInsteadOfDrop;
    }
}
