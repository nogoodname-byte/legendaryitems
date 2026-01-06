using legendaryitems.common.projectiles;
using legendaryitems.common.weapons;
using legendaryitems.common.weaponwuse;
using legendaryitems.config;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace legendaryitems.common.weaponwuse

{
    internal class Rod : ModItem
    {
        public static int setBobbers = 1;

        // Tooltip + Shoot updated from config (separate file)
        private int bobberAmount = 1;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanFishInLava[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 37;
            Item.height = 48;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = ItemRarityID.Expert;

            Item.fishingPole = 50;
            Item.shoot = ProjectileID.BobberGolden;
            Item.shootSpeed = 12f;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source,
            Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var config = ModContent.GetInstance<RodServerConfig>();
            int maxBobbers = config.MaxBobbers;

            int quests = player.anglerQuestsFinished;
            int questMax = 1 + quests / 3;

            int desired = Utils.Clamp(setBobbers, 1, maxBobbers);
            bobberAmount = Utils.Clamp(desired, 1, questMax);

            float spread = 18f;

            for (int i = 0; i < bobberAmount; i++)
            {
                float offset = MathHelper.Lerp(-spread, spread,
                    bobberAmount == 1 ? 0.5f : (float)i / (bobberAmount - 1));

                Vector2 newVel = velocity.RotatedBy(MathHelper.ToRadians(offset));
                Projectile.NewProjectile(source, position, newVel, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            lineOriginOffset = new Vector2(5, -2);
            lineColor = Color.Cyan;
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(BuffID.Sonar, 2);
            player.AddBuff(BuffID.Crate, 2);
            player.AddBuff(BuffID.Fishing, 2);
            player.AddBuff(BuffID.Gills, 2);
            player.AddBuff(BuffID.WaterWalking, 2);
            player.AddBuff(BuffID.Lucky, 2);

            int quests = player.anglerQuestsFinished;
            player.fishingSkill = 10 + quests / 2;

            player.accFishingLine = true;

            Lighting.AddLight(player.Center, 0.2f, 0.5f, 1f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var config = ModContent.GetInstance<RodServerConfig>();

            Player player = Main.LocalPlayer;
            int quests = player.anglerQuestsFinished;
            int questBobbers = 1 + quests / 3;

            tooltips.Add(new TooltipLine(Mod, "FishingSkill",
                $"Fishing Skill: {10 + quests / 2} (scales with quests)"));

            tooltips.Add(new TooltipLine(Mod, "QuestCount",
                $"Angler Quests Completed: {quests}"));

            tooltips.Add(new TooltipLine(Mod, "BobberSettings",
                $"Selected Bobbers: {setBobbers}"));

            tooltips.Add(new TooltipLine(Mod, "ActualBobbers",
                $"Bobbers Actually Cast: {bobberAmount}"));

            tooltips.Add(new TooltipLine(Mod, "Limits",
                $"Bobber Limits: 1–{config.MaxBobbers}, Quest Max: {questBobbers}"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldenFishingRod, 1);
            recipe.AddIngredient(ItemID.AnglerHat, 1);
            recipe.AddIngredient(ItemID.AnglerPants, 1);
            recipe.AddIngredient(ItemID.AnglerVest, 1);
            recipe.AddIngredient(ItemID.LavaproofTackleBag, 1);
            recipe.AddCondition(Condition.NearShimmer);
            recipe.AddCondition(new Condition(
        "Enabled in config",
        () => ModContent.GetInstance<Dropsconfig>().CraftInsteadOfDrop));
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.GoldenFishingRod, 1);
            recipe2.AddIngredient(ItemID.AnglerHat, 1);
            recipe2.AddIngredient(ItemID.AnglerPants, 1);
            recipe2.AddIngredient(ItemID.AnglerVest, 1);
            recipe2.AddIngredient(ItemID.HighTestFishingLine, 1);
            recipe2.AddIngredient(ItemID.AnglerEarring, 1);
            recipe2.AddIngredient(ItemID.TackleBox, 1);
            recipe2.AddIngredient(ItemID.LavaFishingHook, 1);
            recipe2.AddCondition(Condition.NearShimmer);
            recipe2.AddCondition(new Condition(
        "Enabled in config",
        () => ModContent.GetInstance<Dropsconfig>().CraftInsteadOfDrop));
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.GoldenFishingRod, 1);
            recipe3.AddIngredient(ItemID.AnglerHat, 1);
            recipe3.AddIngredient(ItemID.AnglerPants, 1);
            recipe3.AddIngredient(ItemID.AnglerVest, 1);
            recipe3.AddIngredient(ItemID.AnglerTackleBag, 1);
            recipe3.AddIngredient(ItemID.LavaFishingHook, 1);
            recipe3.AddCondition(Condition.NearShimmer);
            recipe3.AddCondition(new Condition(
        "Enabled in config",
        () => ModContent.GetInstance<Dropsconfig>().CraftInsteadOfDrop));
            recipe3.Register();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, 0.1f, 0.3f, 0.9f);
        }
    }

    // -----------------------------------------------------
    // Keybind Registration System
    // -----------------------------------------------------
    public class RodKeybinds : ModSystem
    {
        public static ModKeybind Increase;
        public static ModKeybind Decrease;
        public static ModKeybind Max;
        public static ModKeybind Min;

        public override void Load()
        {
            Increase = KeybindLoader.RegisterKeybind(Mod, "Increase Bobbers", "OemPlus");
            Decrease = KeybindLoader.RegisterKeybind(Mod, "Decrease Bobbers", "OemMinus");
            Max = KeybindLoader.RegisterKeybind(Mod, "Set Bobbers to Max", "PageUp");
            Min = KeybindLoader.RegisterKeybind(Mod, "Set Bobbers to Min", "PageDown");
        }

        public override void Unload()
        {
            Increase = null;
            Decrease = null;
            Max = null;
            Min = null;
        }
    }

    // -----------------------------------------------------
    // Keybind Logic (ModPlayer)
    // -----------------------------------------------------
    public class RodPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            var config = ModContent.GetInstance<RodServerConfig>();
            int max = config.MaxBobbers;

            if (RodKeybinds.Increase.JustPressed)
            {
                Rod.setBobbers = Utils.Clamp(Rod.setBobbers + 1, 1, max);
                Main.NewText($"Bobbers set to {Rod.setBobbers}");
            }

            if (RodKeybinds.Decrease.JustPressed)
            {
                Rod.setBobbers = Utils.Clamp(Rod.setBobbers - 1, 1, max);
                Main.NewText($"Bobbers set to {Rod.setBobbers}");
            }

            if (RodKeybinds.Max.JustPressed)
            {
                Rod.setBobbers = max;
                Main.NewText($"Bobbers set to MAX ({max})");
            }

            if (RodKeybinds.Min.JustPressed)
            {
                Rod.setBobbers = 1;
                Main.NewText("Bobbers set to MIN (1)");
            }
        }
    }
}