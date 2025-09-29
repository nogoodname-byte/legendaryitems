using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;
using legendaryitems.common.projectiles;

namespace legendaryitems.common.weapons
{
    internal class Stockpilingstick : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.UseSound = SoundID.Item1;
            Item.knockBack = 4f;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 2;
            Item.crit = 6;
            Item.rare = ItemRarityID.Expert;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 2.1f;
            Item.shoot = ModContent.ProjectileType<Sstick>();
        }
    }
}
