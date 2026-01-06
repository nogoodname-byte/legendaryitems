using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace legendaryitems.common.itemdrops
{
    public class Modtheplayer : ModPlayer
    {
        private bool fullyChargedShown = false;
        public float storedDamage;
        public const float MaxCharge = 500f;

        // wait ~10 seconds (60 ticks = 1 second)
        private const int DecayDelayTicks = 600;
        private int decayDelay = 0;

        public override void ResetEffects()
        {
            // Soft-cap
            if (storedDamage > MaxCharge)
                storedDamage = MaxCharge;

            // Count down the decay delay if we have charge
            if (storedDamage > 0f && decayDelay > 0)
                decayDelay--;

            // Start decaying only AFTER the wait period ends
            if (storedDamage > 0f && decayDelay <= 0)
                storedDamage -= 0.15f;

            if (storedDamage < 0f)
                storedDamage = 0f;
            
            // Fully charged feedback
            if (storedDamage >= MaxCharge)
            {
                if (!fullyChargedShown)
                {
                    CombatText.NewText(Player.Hitbox, Color.Gold, "Fully Charged!");
                    fullyChargedShown = true;
                }
            }
            else
            {
                // Drop below full — allow future notification again
                fullyChargedShown = false;
            }
        }

        // Call this whenever the weapon gains charge
        public void AddCharge(float amount)
        {
            storedDamage += amount;

            // Reset decay delay when gaining charge
            decayDelay = DecayDelayTicks;

            if (storedDamage > MaxCharge)
                storedDamage = MaxCharge;
        }
    }
}
