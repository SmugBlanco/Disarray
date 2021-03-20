using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
{
	public class ThePact : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Pact");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, heals you back for 75% of health loss on next hit IF:"
			+ "\nThe damage taken is equal to or exceeds 100. Otherwise you get struck again for 50 damage.");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<PactSpark>();
	}
}