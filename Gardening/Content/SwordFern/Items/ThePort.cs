using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
{
	public class ThePort : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Port");
			Tooltip.SetDefault("Conjures a shortlived spark that teleports you to it's death spot"
			+ "\nOnly one spark may exist from a conjurer at a time.");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<PortSpark>();
	}
}