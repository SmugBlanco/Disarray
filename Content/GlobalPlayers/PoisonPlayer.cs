using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Forge.Core.GlobalPlayers
{
	public class PoisonPlayer : ModPlayer
	{
		public float PoisonousAttackChance;

		public float PoisonousAttackDefaultChance = 0.1f;

		public int PoisonousAttackDuration;

		public float PoisonousDamage = 1;

		public override void ResetEffects()
		{
			PoisonousAttackChance = 0;
			PoisonousAttackDefaultChance = 0.1f;
			PoisonousAttackDuration = 0;
			PoisonousDamage = 1;
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (target.HasBuff(BuffID.Poisoned))
			{
				damage = (int)(damage * PoisonousDamage);
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.HasBuff(BuffID.Poisoned))
			{
				damage = (int)(damage * PoisonousDamage);
			}
		}

		public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
		{
			if (target.HasBuff(BuffID.Poisoned))
			{
				damage = (int)(damage * PoisonousDamage);
			}
		}

		public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
		{
			if (target.HasBuff(BuffID.Poisoned))
			{
				damage = (int)(damage * PoisonousDamage);
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (PoisonousAttackChance > 0 && Main.rand.NextFloat() < PoisonousAttackChance + PoisonousAttackDefaultChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (PoisonousAttackChance > 0 && Main.rand.NextFloat() < PoisonousAttackChance + PoisonousAttackDefaultChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}

		public override void OnHitPvp(Item item, Player target, int damage, bool crit)
		{
			if (PoisonousAttackChance > 0 && Main.rand.NextFloat() < PoisonousAttackChance + PoisonousAttackDefaultChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}

		public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
		{
			if (PoisonousAttackChance > 0 && Main.rand.NextFloat() < PoisonousAttackChance + PoisonousAttackDefaultChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}
	}
}