using Terraria;
using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Disarray.Content.Gardening.Forest.SwordFern.PlayerProperties;

namespace Disarray.Content.Gardening.Forest.SwordFern.Projectiles
{
	public class TankSpark : SparkProjectile
	{
		public override string Texture => "Disarray/Content/Gardening/Forest/SwordFern/Projectiles/SparkProjectile";

		public override int DustType => 88;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tank Spark");
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			DisarrayGlobalPlayer disarrayGlobalPlayer = Main.player[projectile.owner].GetModPlayer<DisarrayGlobalPlayer>();
			Main.NewText(disarrayGlobalPlayer.ManuallyRemovedProperties.Count);
			PlayerProperty.ImplementProperty(Main.player[projectile.owner], new TankSparkProperty());
		}
	}
}