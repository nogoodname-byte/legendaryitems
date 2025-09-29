using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Security;
using legendaryitems.common.projectiles;

namespace legendaryitems.common.weaponwuse
{
    internal class Rod : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanFishInLava[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(gold: 10, silver: 50);
            Item.rare = ItemRarityID.Expert;
            Item.fishingPole = 25;
            Item.shoot = ModContent.ProjectileType<MyCustomBobber>();
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = false;
            Item.shootSpeed = 10f;
        }
        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            lineOriginOffset = new Vector2(5f, -2f);
            lineColor = Color.Cyan * 0.8f;
        }
    }
}
