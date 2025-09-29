using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace legendaryitems.common.accessories
{
    public class TheFactory : ModItem
    {
        public int summons;
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 10, silver: 50);
            Item.rare = ItemRarityID.Expert;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxTurrets += player.maxMinions;
            Item.defense = player.maxMinions;
            Item.lifeRegen = player.maxMinions;
            player.GetDamage(DamageClass.Summon) += 0.10f * player.maxMinions;
            player.GetAttackSpeed(DamageClass.Summon) += 0.20f * player.maxMinions;
            player.maxMinions = 1;

        }
    }
}
