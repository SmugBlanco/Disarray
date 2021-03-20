using Terraria;
using Disarray.Core.Properties;
using Disarray.Core.Autoload;
using Disarray.ID;
using Disarray.Gardening.Content.SwordFern.PlayerProperties;

namespace Disarray.Gardening.Content.SwordFern.Projectiles
{
	public class TankSpark : SparkProjectile
	{
		public override string Texture => Directory;

		public override int DustType => DustID.SapphireBolt;

		public override void SetStaticDefaults() => DisplayName.SetDefault("Tank Spark");

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => PlayerProperty.ImplementProperty(Main.player[projectile.owner], AutoloadedClass.CreateNewInstance<TankSparkProperty>());
	}
}