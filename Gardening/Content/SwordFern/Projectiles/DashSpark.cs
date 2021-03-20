using Terraria;
using Terraria.ModLoader;
using Disarray.ID;
using Disarray.Gardening.Content.SwordFern.Buffs;

namespace Disarray.Gardening.Content.SwordFern.Projectiles
{
	public class DashSpark : SparkProjectile
	{
		public override string Texture => Directory;

		public override int DustType => DustID.EmeraldBolt;

		public override void SetStaticDefaults() => DisplayName.SetDefault("Dash Spark");

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => Main.player[projectile.owner].AddBuff(ModContent.BuffType<SwordFernSpeedBoost>(), 60 * 5);
	}
}