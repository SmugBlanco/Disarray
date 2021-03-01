using Terraria;
using Disarray.Core.Data;

namespace Disarray.Content.Gardening.SwordFern.Projectiles
{
	public class BetrayalSpark : SparkProjectile
	{
		public override string Texture => "Disarray/Content/Gardening/SwordFern/Projectiles/SparkProjectile";

		public override int DustType => DustID.AmethystBolt;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Betrayal Spark");
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.timeLeft = 60;

			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.hostile = true;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
		}

		public override bool CanHitPlayer(Player target) => false;

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.townNPC)
			{
				damage *= 25;
			}
		}
	}
}