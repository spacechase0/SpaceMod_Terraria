using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpaceMod
{
    class ImaginaryHookProjectile : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Imaginary Hook Projectile");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.StaticHook);
		}
        
		public override float GrappleRange()
		{
			return 1000;
		}
        
        public override bool? SingleGrappleHook(Player player)
        {
            return true;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 1;
		}
        
		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 25;
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 25;
		}

        public override void AI()
        {
            Lighting.AddLight(Main.player[projectile.owner].MountedCenter, 0f, 0.3f, 0.6f);
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 28)
                projectile.localAI[0] = 0;
            Utils.PlotTileLine(projectile.Center, Main.player[projectile.owner].MountedCenter, 8f, new Utils.PerLinePoint(DelegateMethods.CastLightOpen));

            if (projectile.ai[0] == 0 && Main.myPlayer == projectile.owner)
            {
                projectile.velocity.X = 0f;
                projectile.velocity.Y = 0f;

                float num115 = (float)Main.mouseX + Main.screenPosition.X;
                float num116 = (float)Main.mouseY + Main.screenPosition.Y;
                if (Main.player[projectile.owner].gravDir == -1f)
                {
                    num116 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                }
                projectile.position.X = num115;
                projectile.position.Y = num116;

                projectile.position.X = (float)(projectile.position.ToTileCoordinates().X * 16 + 8 - projectile.width / 2);
                projectile.position.Y = (float)(projectile.position.ToTileCoordinates().Y * 16 + 8 - projectile.height / 2);

                projectile.netUpdate = true;
            }

            projectile.ai[0] = 2f;
            Main.player[projectile.owner].grappling[Main.player[projectile.owner].grapCount] = projectile.whoAmI;
            Main.player[projectile.owner].grapCount++;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 center = projectile.Center;
			Vector2 distToProj = playerCenter - projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance))
			{
				distToProj.Normalize();
                distToProj *= 20f;
				center += distToProj;
				distToProj = playerCenter - center;
				distance = distToProj.Length();
				Color drawColor = lightColor;

                Texture2D tex = mod.GetTexture("ImaginaryHookChain");
                int num = (int)projectile.localAI[ 0 ] / 7;
                Rectangle sourceRect = new Rectangle(0, tex.Height / 4 * num, tex.Width, tex.Height / 4);
                Vector2 origin = new Vector2(tex.Width / 2, tex.Height / 4 / 2);

				//Draw chain
				spriteBatch.Draw(tex, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					sourceRect, drawColor, projRotation, origin, 1f, SpriteEffects.None, 0f);
			}
			return true;
		}

        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return false;
        }
    }
}
