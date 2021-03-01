using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Gardening.SwordFern.Buffs;
using Disarray.Core.Data;
using Terraria.UI;

namespace Disarray.Content.Gardening.SwordFern.Projectiles
{
	public class PierceSpark : SparkProjectile
	{
		public override string Texture => "Disarray/Content/Gardening/SwordFern/Projectiles/SparkProjectile";

		public override int DustType => DustID.AmberBolt;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pierce Spark");
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Main.player[projectile.owner].AddBuff(ModContent.BuffType<SwordFernArmorPiercingBoost>(), 60 * 5);
        }
    }
}