using Terraria;
using Terraria.ModLoader;
using Disarray.Content.Gardening.Forest.SwordFern.Buffs;

namespace Disarray.Content.Gardening.Forest.SwordFern.Projectiles
{
	public class PierceSpark : SparkProjectile
	{
		public override string Texture => "Disarray/Content/Gardening/Forest/SwordFern/Projectiles/SparkProjectile";

		public override int DustType => 90;

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