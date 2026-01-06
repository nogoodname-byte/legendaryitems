using legendaryitems.common.weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace legendaryitems.common.itemdrops
{
    public class Modthesystem : ModSystem
    {
        bool dragging;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(l => l.Name.Equals("Vanilla: Resource Bars"));
            if (index == -1) return;

            layers.Insert(index + 1, new LegacyGameInterfaceLayer(
                "LegendaryItems: ChargeBar",
                () =>
                {
                    DrawChargeBar(Main.spriteBatch);
                    return true;
                },
                InterfaceScaleType.UI
            ));
        }

        private void DrawChargeBar(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var cfg = ModContent.GetInstance<config.Sstickconfig>();

            // Show ONLY if player has StockpilingStick
            bool hasStick = player.inventory.Any(i =>
                !i.IsAir &&
                i.type == ModContent.ItemType<Stockpilingstick>());

            if (!hasStick)
                return;

            var modPlayer = player.GetModPlayer<Modtheplayer>();

            float percent = MathHelper.Clamp(modPlayer.storedDamage / Modtheplayer.MaxCharge, 0f, 1f);

            if (percent <= 0f)
                return;

            Vector2 pos = cfg.ChargeBarPosition;

            int barWidth = 200;
            int barHeight = 14;

            // Background
            DrawRect(spriteBatch, pos, barWidth, barHeight, Color.Black * 0.55f);

            // Fill
            DrawRect(
                spriteBatch,
                pos + new Vector2(2, 2),
                (int)((barWidth - 4) * percent),
                barHeight - 4,
                percent >= 1f ? Color.Gold : Color.CornflowerBlue
            );

            // Border
            DrawRectOutline(spriteBatch, pos, barWidth, barHeight, Color.White);
            string text = percent >= 1f ? "FULL" : $"{percent * 100f:0}%";
            Vector2 textSize = Terraria.GameContent.FontAssets.ItemStack.Value.MeasureString(text);

            // Pulse when full
            float scale = 1f;
            if (percent >= 1f)
            {
                // 6f = speed, 0.15f = strength
                scale = 1f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.15f;
            }

            Vector2 textPos = pos + new Vector2(
                (barWidth - textSize.X * scale) / 2f,
                (barHeight - textSize.Y * scale) / 2f
            );

            // draw a dark shadow for readability
            Terraria.UI.Chat.ChatManager.DrawColorCodedString(
                spriteBatch,
                Terraria.GameContent.FontAssets.ItemStack.Value,
                text,
                textPos + new Vector2(1, 1),
                Color.Black * 0.6f,
                0f,
                Vector2.Zero,
                new Vector2(scale)
            );

            // main text
            Terraria.UI.Chat.ChatManager.DrawColorCodedString(
                spriteBatch,
                Terraria.GameContent.FontAssets.ItemStack.Value,
                text,
                textPos,
                percent >= 1f ? Color.Gold : Color.White,
                0f,
                Vector2.Zero,
                new Vector2(scale)
            );

            // --- Dragging ---
            Rectangle hitbox = new Rectangle((int)pos.X, (int)pos.Y, barWidth, barHeight);

            if (hitbox.Contains(Main.MouseScreen.ToPoint()) && Main.mouseLeft && Main.mouseLeftRelease)
                dragging = true;

            if (dragging)
            {
                cfg.ChargeBarPosition = Main.MouseScreen - new Vector2(barWidth / 2, barHeight / 2);

                if (!Main.mouseLeft)
                    dragging = false;
            }
        }

        private void DrawRect(SpriteBatch sb, Vector2 pos, int width, int height, Color color)
        {
            sb.Draw(TextureAssets.MagicPixel.Value,
                new Rectangle((int)pos.X, (int)pos.Y, width, height),
                color);
        }

        private void DrawRectOutline(SpriteBatch sb, Vector2 pos, int width, int height, Color color)
        {
            DrawRect(sb, pos, width, 1, color);
            DrawRect(sb, pos + new Vector2(0, height - 1), width, 1, color);
            DrawRect(sb, pos, 1, height, color);
            DrawRect(sb, pos + new Vector2(width - 1, 0), 1, height, color);
        }
    }
}
