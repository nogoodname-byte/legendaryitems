using legendaryitems.common.weaponwuse;
using legendaryitems.config;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace legendaryitems.common.itemdrops
{
    public class TheRod : ModPlayer
    {
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (!ModContent.GetInstance<Dropsconfig>().CraftInsteadOfDrop)
            {
                // Example: drop your rod
                if (Main.rand.NextBool(20)) // 0.00000005% chance
                    itemDrop = ModContent.ItemType<Rod>();
            }
        }
    }
}
