using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace legendaryitems.common.projectiles
{
    internal class MyCustomBobber : ModProjectile
    {
        public static readonly Color[] PossibleLineColors = [
            new Color(255, 215, 0), // A gold color
			new Color(0, 191, 255) // A blue color
		];

        // This holds the index of the fishing line color in the PossibleLineColors array.
        private int fishingLineColorIndex;

        public Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 61;
            Projectile.bobber = true;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            DrawOriginOffsetY = 0;
        }
        public override void AI()
        {
            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, 255);
            }

        }

        // These last two methods are required so the line color is properly synced in multiplayer.
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)fishingLineColorIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            fishingLineColorIndex = reader.ReadByte();
        }
    }
}
