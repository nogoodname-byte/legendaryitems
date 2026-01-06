using legendaryitems.common.itemdrops;
using legendaryitems.common.projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

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
            Item.shoot = ModContent.ProjectileType<Sstick1>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var p = Main.LocalPlayer.GetModPlayer<Modtheplayer>();

            tooltips.Add(new TooltipLine(Mod, "description",
                "Charge up damage by hitting enemies. Right-click to release a % of stored damage"));

            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<Modtheplayer>();

            float percent = (modPlayer.storedDamage / Modtheplayer.MaxCharge) * 100f;
            percent = Utils.Clamp(percent, 0f, 100f);

            tooltips.Add(new TooltipLine(Mod, "chargePercent",
                $"Charge: {percent:0}%"));
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                var modPlayer = player.GetModPlayer<Modtheplayer>();
                if (modPlayer.storedDamage > 0)
                {
                    int burstDamage = (int)modPlayer.storedDamage;
                    bool crit = Main.rand.Next(100) < player.GetCritChance(DamageClass.Melee);
                    if (crit)
                        burstDamage = (int)(burstDamage * 1.5f);
                    modPlayer.storedDamage = 0;
                    Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Sstick>(), burstDamage, knockback, player.whoAmI);
                    DostickVisuals(player);
                }
                    return false;
            }
            return true;
        }
        private void DostickVisuals(Player player)
        {
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(
                    player.Center,
                    10,
                    10,
                    Terraria.ID.DustID.GoldCoin,
                    Main.rand.NextFloat(-3f, 3f),
                    Main.rand.NextFloat(-3f, 3f)
                );
                Main.dust[dust].noGravity = true;
            }

            player.velocity *= 0.8f;

            Terraria.Audio.SoundEngine.PlaySound(
                Terraria.ID.SoundID.Item74,
                player.Center);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var p = Main.LocalPlayer.GetModPlayer<Modtheplayer>();

            if (p.storedDamage >= Modtheplayer.MaxCharge * 0.9f)
            {
                Lighting.AddLight(Item.Center, 0.9f, 0.8f, 0.1f);
            }
        }
    }
}
