using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Disarray.Content.ProjectileTypes
{
	public abstract class Pole : ModProjectile
	{
		public Vector2 Center { get => projectile.Center; set => projectile.position = value - new Vector2(projectile.width / 2, projectile.height / 2); }

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.rotation += MathHelper.ToRadians(6);
			Center = player.Center;
		}
	}
}