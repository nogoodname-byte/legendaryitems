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
        public int sec = 0;
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
