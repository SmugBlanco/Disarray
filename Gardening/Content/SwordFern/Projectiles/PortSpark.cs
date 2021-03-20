using Terraria;
using Microsoft.Xna.Framework;
using Disarray.ID;

namespace Disarray.Gardening.Content.SwordFern.Projectiles
{
	public class PortSpark : SparkProjectile
	{
		public override string Texture => Directory;

		public override int DustType => DustID.TopazBolt;

		public override void SetStaticDefaults() => DisplayName.SetDefault("Port Spark");

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.timeLeft = 60;

			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			player.Teleport(projectile.Center - new Vector2(0, player.height / 2));
		}
	}
}