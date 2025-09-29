using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using legendaryitems.npcstuff;
using legendaryitems.common.projectiles;

namespace legendaryitems.stuff.weapons
{
    public class TheThrow : ModItem    
    {
        public static int damage = 20;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;                                                       // tells the code it is a Yoyo and to exicute Yoyo code
            object value = "The Throw";
        }
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.channel = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = damage;
            Item.knockBack = 2;
            Item.crit = 6;
            Item.value = Item.buyPrice(gold: 10, silver: 50);
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Thethrowp>();                                      // tells it what projectile to shoot
            Item.shootSpeed = 6f;
            Item.noMelee = true;                                                                      // tells the code that the item itself is not a weapon
        }
        Condition DownedWoF = new("Mods.YourMod.Conditions.DownedWallOfFlesh", () => Main.hardMode);
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TheSpin>(), 1);
            recipe.AddCondition(Condition.NearShimmer);
            recipe.AddCondition(DownedWoF);
            recipe.Register();
        }
    }
}
