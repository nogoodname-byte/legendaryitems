using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace legendaryitems.config
{
    // This config is SERVER-SIDE (affects world, not per-client)
    public class RodServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Range(10, 1000)]
        [Increment(10)]
        [DefaultValue(100)]
        public int MaxBobbers;
    }
}
