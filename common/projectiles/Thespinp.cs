using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Timers;
using legendaryitems.stuff.weapons;

namespace legendaryitems.common.projectiles
{
    public class Thespinp : ModProjectile
    {
        public int i;
        public float damageI = TheSpin.damage;
        public float damager = TheSpin.damage;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 320;            // the rang in pixels 
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1;       // the time alive
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15;                 // the speed
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
        }
        public override void OnKill(int timeLeft)
        {
            i = 0;
            damager = damageI;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            UpdateSecP();
            if (i < 1)
            {
                i++;
                damageI = damageDone;
                damager = damageDone;
            }
        }
        public void UpdateSecP()
        {
            if (damager <= damageI * 10)
            {
                damager = damager + damageI * 0.5f;
                Projectile.damage = (int)damager;
            }
            if (damager >= damageI * 10)
            {
                damager = damageI * 10;
                Projectile.damage = (int)damager;
            }
            Projectile.damage = (int)damager;
        }
    }
}
