using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
{
	public class TheDash : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Dash");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, increases your movement speed by 50% for 5 seconds.");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<DashSpark>();
	}
}