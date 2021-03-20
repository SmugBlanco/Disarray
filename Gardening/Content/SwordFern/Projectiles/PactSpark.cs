using Terraria;
using Disarray.Core.Properties;
using Disarray.ID;
using Disarray.Gardening.Content.SwordFern.PlayerProperties;

namespace Disarray.Gardening.Content.SwordFern.Projectiles
{
	public class PactSpark : SparkProjectile
	{
		public override string Texture => Directory;

		public override int DustType => DustID.DiamondBolt;

		public override void SetStaticDefaults() => DisplayName.SetDefault("Pact Spark");

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => PlayerProperty.ImplementProperty(Main.player[projectile.owner], new PactSparkProperty());
	}
}