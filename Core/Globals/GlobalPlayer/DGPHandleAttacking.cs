using Disarray.Core.Properties;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Globals
{
	public partial class DisarrayGlobalPlayer : ModPlayer
	{
		public override bool? CanHitNPC(Item item, NPC target)
		{
			bool? continueHitNPC = base.CanHitNPC(item, target);
			foreach (PlayerProperty properties in ActiveProperties)
			{
				bool? canBeHit = properties.CanHitNPC(player, item, target);

				if (canBeHit.HasValue)
				{
					if (!continueHitNPC.HasValue)
					{
						continueHitNPC = canBeHit.Value;
					}

					if (!canBeHit.Value)
					{
						continueHitNPC = false;
					}
				}
			}
			return continueHitNPC;
		}

		public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
		{
			bool? continueHitNPC = base.CanHitNPCWithProj(proj, target);
			foreach (PlayerProperty properties in ActiveProperties)
			{
				bool? canBeHit = properties.CanHitNPCWithProj(player, proj, target);

				if (canBeHit.HasValue)
				{
					if (!continueHitNPC.HasValue)
					{
						continueHitNPC = canBeHit.Value;
					}

					if (!canBeHit.Value)
					{
						continueHitNPC = false;
					}
				}
			}
			return continueHitNPC;
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.ModifyHitNPC(player, item, target, ref damage, ref knockback, ref crit);
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.ModifyHitNPCWithProj(player, proj, target, ref damage, ref knockback, ref crit);
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.OnHitNPC(player, item, target, damage, knockback, crit);
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.OnHitNPCWithProj(player, proj, target, damage, knockback, crit);
			}
		}

		//---------------------------------------------------------PVP Stuff but no one does pvp so is it really needed?! Probably not!----------------------------------------------------------------------------------

		public override bool CanHitPvp(Item item, Player target)
		{
			bool canHitPVP = base.CanHitPvp(item, target);
			foreach (PlayerProperty properties in ActiveProperties)
			{
				if (!properties.CanHitPvp(player, item, target))
				{
					canHitPVP = false;
				}
			}
			return canHitPVP;
		}

		public override bool CanHitPvpWithProj(Projectile proj, Player target)
		{
			bool canHitPVP = base.CanHitPvpWithProj(proj, target);
			foreach (PlayerProperty properties in ActiveProperties)
			{
				if (!properties.CanHitPvpWithProj(player, proj, target))
				{
					canHitPVP = false;
				}
			}
			return canHitPVP;
		}

		public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.ModifyHitPvp(player, item, target, ref damage, ref crit);
			}
		}

		public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.ModifyHitPvpWithProj(player, proj, target, ref damage, ref crit);
			}
		}

		public override void OnHitPvp(Item item, Player target, int damage, bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.OnHitPvp(player, item, target, damage, crit);
			}
		}

		public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
		{
			foreach (PlayerProperty properties in ActiveProperties)
			{
				properties.OnHitPvpWithProj(player, proj, target, damage, crit);
			}
		}
	}
}