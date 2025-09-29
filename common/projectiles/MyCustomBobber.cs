using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Security;


namespace legendaryitems.common.projectiles
{
    internal class MyCustomBobber : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 61;
            Projectile.bobber = true;
        }
        public override void AI()
        {
            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, 255);
            }

        }
    }
}
