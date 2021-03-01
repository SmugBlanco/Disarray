using Disarray.Core.Globals;
using Disarray.Core.Properties;
using Terraria;
using Terraria.DataStructures;

namespace Disarray.Content.Gardening.SwordFern.PlayerProperties
{
    public class PactSparkProperty : PlayerProperty
    {
		public override void OnHitByProjectile(Player player, Projectile proj, int damage, bool crit)
		{
			if (damage >= 100)
			{
				int healedAmount = (int)(damage * 0.75f);
				player.statLife += healedAmount;
				player.HealEffect(healedAmount);
			}
			else
			{
				player.immune = false;
				player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " could not maintain his pact."), 50, -1);
			}

			player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(LoadedProperties[Type]);
		}

		public override void OnHitByNPC(Player player, NPC npc, int damage, bool crit)
		{
			if (damage >= 100)
			{
				int healedAmount = (int)(damage * 0.75f);
				player.statLife += healedAmount;
				player.HealEffect(healedAmount);
			}
			else
			{
				player.immune = false;
				player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " could not maintain his pact."), 50, -1);
			}

			player.GetModPlayer<DisarrayGlobalPlayer>().ManuallyRemovedProperties.Remove(LoadedProperties[Type]);
		}
	}
}