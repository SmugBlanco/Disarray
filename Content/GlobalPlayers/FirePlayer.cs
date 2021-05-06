using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Core.GlobalPlayers
{
	public class FirePlayer : ModPlayer
	{
		public float OnFireChance;
		public float OnFireDefaultChance = 0.1f;
		public float OnFireDamage = 1f;

		public float FrostburnChance;
		public float FrostburnDefaultChance = 0.1f;
		public float FrostburnDamage = 1f;

		public float ShadowFlameChance;
		public float ShadowFlameDefaultChance = 0.05f;
		public float ShadowFlameDamage = 1f;

		public float CursedInfernoChance;
		public float CursedInfernoDefaultChance = 0.05f;
		public float CursedInfernoDamage = 1f;

		public int GeneralDuration = 180;

		public override void ResetEffects()
		{
			OnFireChance = 0;
			OnFireDefaultChance = 0.1f;
			OnFireDamage = 1f;

			FrostburnChance = 0;
			FrostburnDefaultChance = 0.1f;
			FrostburnDamage = 1f;

			ShadowFlameChance = 0;
			ShadowFlameDefaultChance = 0.05f;
			ShadowFlameDamage = 1f;

			CursedInfernoChance = 0;
			CursedInfernoDefaultChance = 0.05f;
			CursedInfernoDamage = 1f;

			GeneralDuration = 180;
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.HasBuff(BuffID.OnFire))
			{
				damage = (int)(damage * OnFireDamage);
			}

			if (target.HasBuff(BuffID.Frostburn))
			{
				damage = (int)(damage * FrostburnDamage);
			}

			if (target.HasBuff(BuffID.ShadowFlame))
			{
				damage = (int)(damage * ShadowFlameDamage);
			}

			if (target.HasBuff(BuffID.CursedInferno))
			{
				damage = (int)(damage * CursedInfernoDamage);
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.HasBuff(BuffID.OnFire))
			{
				damage = (int)(damage * OnFireDamage);
			}

			if (target.HasBuff(BuffID.Frostburn))
			{
				damage = (int)(damage * FrostburnDamage);
			}

			if (target.HasBuff(BuffID.ShadowFlame))
			{
				damage = (int)(damage * ShadowFlameDamage);
			}

			if (target.HasBuff(BuffID.CursedInferno))
			{
				damage = (int)(damage * CursedInfernoDamage);
			}
		}

		public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
		{
			if (target.HasBuff(BuffID.OnFire))
			{
				damage = (int)(damage * OnFireDamage);
			}

			if (target.HasBuff(BuffID.Frostburn))
			{
				damage = (int)(damage * FrostburnDamage);
			}

			if (target.HasBuff(BuffID.ShadowFlame))
			{
				damage = (int)(damage * ShadowFlameDamage);
			}

			if (target.HasBuff(BuffID.CursedInferno))
			{
				damage = (int)(damage * CursedInfernoDamage);
			}
		}

		public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
		{
			if (target.HasBuff(BuffID.OnFire))
			{
				damage = (int)(damage * OnFireDamage);
			}

			if (target.HasBuff(BuffID.Frostburn))
			{
				damage = (int)(damage * FrostburnDamage);
			}

			if (target.HasBuff(BuffID.ShadowFlame))
			{
				damage = (int)(damage * ShadowFlameDamage);
			}

			if (target.HasBuff(BuffID.CursedInferno))
			{
				damage = (int)(damage * CursedInfernoDamage);
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (OnFireChance > 0 && Main.rand.NextFloat() < OnFireChance + OnFireDefaultChance)
			{
				target.AddBuff(BuffID.OnFire, GeneralDuration);
			}

			if (FrostburnChance > 0 && Main.rand.NextFloat() < FrostburnChance + FrostburnDefaultChance)
			{
				target.AddBuff(BuffID.Frostburn, GeneralDuration);
			}

			if (ShadowFlameChance > 0 && Main.rand.NextFloat() < ShadowFlameChance + ShadowFlameDefaultChance)
			{
				target.AddBuff(BuffID.ShadowFlame, GeneralDuration);
			}

			if (CursedInfernoChance > 0 && Main.rand.NextFloat() < CursedInfernoChance + CursedInfernoDefaultChance)
			{
				target.AddBuff(BuffID.CursedInferno, GeneralDuration);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (OnFireChance > 0 && Main.rand.NextFloat() < OnFireChance + OnFireDefaultChance)
			{
				target.AddBuff(BuffID.OnFire, GeneralDuration);
			}

			if (FrostburnChance > 0 && Main.rand.NextFloat() < FrostburnChance + FrostburnDefaultChance)
			{
				target.AddBuff(BuffID.Frostburn, GeneralDuration);
			}

			if (ShadowFlameChance > 0 && Main.rand.NextFloat() < ShadowFlameChance + ShadowFlameDefaultChance)
			{
				target.AddBuff(BuffID.ShadowFlame, GeneralDuration);
			}

			if (CursedInfernoChance > 0 && Main.rand.NextFloat() < CursedInfernoChance + CursedInfernoDefaultChance)
			{
				target.AddBuff(BuffID.CursedInferno, GeneralDuration);
			}
		}

		public override void OnHitPvp(Item item, Player target, int damage, bool crit)
		{
			if (OnFireChance > 0 && Main.rand.NextFloat() < OnFireChance + OnFireDefaultChance)
			{
				target.AddBuff(BuffID.OnFire, GeneralDuration);
			}

			if (FrostburnChance > 0 && Main.rand.NextFloat() < FrostburnChance + FrostburnDefaultChance)
			{
				target.AddBuff(BuffID.Frostburn, GeneralDuration);
			}

			if (ShadowFlameChance > 0 && Main.rand.NextFloat() < ShadowFlameChance + ShadowFlameDefaultChance)
			{
				target.AddBuff(BuffID.ShadowFlame, GeneralDuration);
			}

			if (CursedInfernoChance > 0 && Main.rand.NextFloat() < CursedInfernoChance + CursedInfernoDefaultChance)
			{
				target.AddBuff(BuffID.CursedInferno, GeneralDuration);
			}
		}

		public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
		{
			if (OnFireChance > 0 && Main.rand.NextFloat() < OnFireChance + OnFireDefaultChance)
			{
				target.AddBuff(BuffID.OnFire, GeneralDuration);
			}

			if (FrostburnChance > 0 && Main.rand.NextFloat() < FrostburnChance + FrostburnDefaultChance)
			{
				target.AddBuff(BuffID.Frostburn, GeneralDuration);
			}

			if (ShadowFlameChance > 0 && Main.rand.NextFloat() < ShadowFlameChance + ShadowFlameDefaultChance)
			{
				target.AddBuff(BuffID.ShadowFlame, GeneralDuration);
			}

			if (CursedInfernoChance > 0 && Main.rand.NextFloat() < CursedInfernoChance + CursedInfernoDefaultChance)
			{
				target.AddBuff(BuffID.CursedInferno, GeneralDuration);
			}
		}
	}
}