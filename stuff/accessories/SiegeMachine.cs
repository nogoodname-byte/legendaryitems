using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using legendaryitems.projectiles;
using System;

namespace legendaryitems.stuff.accessories
{
    public class SiegeMachine : ModItem
    {
        public int summons;
        public int star;
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
            summons = player.maxMinions;
            player.maxTurrets += summons / 2;
            Item.defense = summons * 2;
            Item.lifeRegen = summons / 2;
            player.GetDamage(DamageClass.Summon) += 0.05f * summons;
            player.GetAttackSpeed(DamageClass.Summon) += 0.10f * summons;
            player.maxMinions = 1;
            
        }
    }
}
