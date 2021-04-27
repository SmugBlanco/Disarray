using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Forge.Content.Items.Vitalium
{
	public class VitaliumStaffProjectile : ModProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Vitalium Bolt");

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 2;
			projectile.timeLeft = 180;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.alpha = 255;
		}

		public override void AI()
		{
			if (Main.rand.Next(4) == 0)
			{
				int spawnedDust = Dust.NewDust(projectile.position, 0, 0, DustID.Grass, 0, 0, 0, default, 1f);
				Main.dust[spawnedDust].velocity = projectile.velocity;
				Main.dust[spawnedDust].noGravity = true;
			}

			if (Main.rand.Next(6) == 0)
			{
				int spawnedDust = Dust.NewDust(projectile.position, 0, 0, DustID.JungleGrass, 0, 0, 0, default, 1f);
				Main.dust[spawnedDust].velocity = projectile.velocity;
				Main.dust[spawnedDust].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < 0.25f)
			{
				target.AddBuff(BuffID.Poisoned, 180);
			}
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			if (Main.rand.NextFloat() < 0.25f)
			{
				target.AddBuff(BuffID.Poisoned, 180);
			}
		}
	}
}