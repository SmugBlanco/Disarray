using Terraria;
using Disarray.Core.Properties;
using Disarray.Content.Gardening.SwordFern.PlayerProperties;
using Disarray.Core.Data;

namespace Disarray.Content.Gardening.SwordFern.Projectiles
{
	public class StrikeSpark : SparkProjectile
	{
		public override string Texture => "Disarray/Content/Gardening/SwordFern/Projectiles/SparkProjectile";

		public override int DustType => DustID.RubyBolt;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Strike Spark");
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => PlayerProperty.ImplementProperty(Main.player[projectile.owner], new StrikeSparkProperty());
	}
}