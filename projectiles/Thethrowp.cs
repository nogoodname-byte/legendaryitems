using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Security.Cryptography.X509Certificates;
using System;
using legendaryitems.stuff.weapons;
using System.Security;

namespace legendaryitems.projectiles
{
    public class Thethrowp : ModProjectile
    {
        public int sec;
        public static float damageI = 20;
        public static float damager = 20;
        public override void SetStaticDefaults()
        {
            if (sec < 2)
            {
                sec = 2;
            }
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 320;            // the rang in pixels 
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = sec;      // the time alive
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
            damager = damageI;
            sec = 2;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = sec;      // the time alive
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            UpdateSecP();
        }
        public void UpdateSecP()
        {
            if(damager <= damageI*3)
            {
                damager = damager+damageI*0.1f;
                Projectile.damage = (int)damager;
                Console.WriteLine("damage+");
            }
            if (damager >= damageI*3)
            {
                damager = damageI*3;
                Projectile.damage = (int)damager;
            }
            ++sec;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = sec;      // the time alive
            Projectile.damage = (int)damager;
        }
    }
}
