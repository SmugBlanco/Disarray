using Terraria;
using Terraria.ModLoader;
using Disarray.ID;
using Disarray.Gardening.Content.SwordFern.Buffs;

namespace Disarray.Gardening.Content.SwordFern.Projectiles
{
	public class PierceSpark : SparkProjectile
	{
		public override string Texture => Directory;

		public override int DustType => DustID.AmberBolt;

        public override void SetStaticDefaults() => DisplayName.SetDefault("Pierce Spark");

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => Main.player[projectile.owner].AddBuff(ModContent.BuffType<SwordFernArmorPiercingBoost>(), 60 * 5);
    }
}