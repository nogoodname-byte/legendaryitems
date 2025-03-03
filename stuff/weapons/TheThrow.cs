using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using legendaryitems.projectiles;

namespace legendaryitems.stuff.weapons
{
    public class TheThrow : ModItem    
    {
        public static int sec;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;                                                       // tells the code it is a Yoyo and to exicute Yoyo code
            object value = "The Throw";
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.channel = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 20;
            Item.knockBack = 2;
            Item.crit = 6;
            Item.value = Item.buyPrice(gold: 10, silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Thethrowp>();                                      // tells it what projectile to shoot
            Item.shootSpeed = 6f;
            Item.noMelee = true;                                                                      // tells the code that the item itself is not a weapon
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 360); // 60 = 1s
            UpdateSecP();
        }

        public void UpdateSecP()
        {
            Console.WriteLine("updating sec");
            sec = sec + 1;
            Thethrowp.sec = sec;
        }
    }
}
