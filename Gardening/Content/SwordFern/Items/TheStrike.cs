using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
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