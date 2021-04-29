using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Disarray.Forge.Core.GlobalPlayers
{
	public class PoisonPlayer : ModPlayer
	{
		public float PoisonousAttackChance;

		public int PoisonousAttackDuration;

		public override void ResetEffects()
		{
			PoisonousAttackChance = 0;
			PoisonousAttackDuration = 0;
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < PoisonousAttackChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < PoisonousAttackChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}

		public override void OnHitPvp(Item item, Player target, int damage, bool crit)
		{
			if (Main.rand.NextFloat() < PoisonousAttackChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}

		public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
		{
			if (Main.rand.NextFloat() < PoisonousAttackChance)
			{
				target.AddBuff(BuffID.Poisoned, PoisonousAttackDuration);
			}
		}
	}
}