using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Disarray.Content.Forge.Projectiles.Cloud
{
	public class CloudSpearWeapon : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cloud Spear");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;

			projectile.aiStyle = 19;

			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 15;

			projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}

		public float Movement { get => projectile.ai[0]; set => projectile.ai[0] = value; }

		public override void AI()
		{
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);

			if (!projOwner.frozen)
			{
				if (Movement == 0f)
				{
					Movement = 3f;
					projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
				{
					Movement -= 2.4f;
				}
				else
				{
					Movement += 2.1f;
				}
			}

			projectile.position += projectile.velocity * Movement;

			if (projOwner.itemAnimation == 0)
			{
				projectile.Kill();
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation, new Vector2(Main.projectileTexture[projectile.type].Width, 0), projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}