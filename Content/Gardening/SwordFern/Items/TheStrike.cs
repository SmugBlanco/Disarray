using Disarray.Content.Gardening.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Content.Gardening.SwordFern.Items
{
	public class TheStrike : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Strike");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, increases your next hit's damage by 250% IF it is a critical strike"
			+ "\nEffect doubles for true melee strikes.");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<StrikeSpark>();
	}
}