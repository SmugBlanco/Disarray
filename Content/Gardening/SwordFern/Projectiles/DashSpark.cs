using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Gardening.SwordFern.Buffs;
using Disarray.Core.Data;

namespace Disarray.Content.Gardening.SwordFern.Projectiles
{
	public class DashSpark : SparkProjectile
	{
		public override string Texture => "Disarray/Content/Gardening/SwordFern/Projectiles/SparkProjectile";

		public override int DustType => DustID.EmeraldBolt;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dash Spark");
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.player[projectile.owner].AddBuff(ModContent.BuffType<SwordFernSpeedBoost>(), 60 * 5);
		}
	}
}