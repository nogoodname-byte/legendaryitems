using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using legendaryitems.stuff;
using System.Security.Cryptography.X509Certificates;
using System;

namespace legendaryitems.projectiles
{
    public class Thethrowp : ModProjectile
    {
        int sec = 0;
        int thehit = 0;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 60);
            //base.OnHitNPC(target, hit, ++thehit);
            if (thehit < 10)
            {
                target.AddBuff(BuffID.WeaponImbueCursedFlames, 60);
                ++thehit;
            }
        }
        public override void SetStaticDefaults()
        {
            if (sec < 2)
            {
                sec = 2;
            }
            if (thehit > 0)
            {
                sec = 2 + thehit;
            }
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 320;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = sec;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;    
            Projectile.height = 32;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
        }
    }
}
