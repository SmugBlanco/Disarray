using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
{
	public class ThePierce : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Pierce");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, increases your armor piercing by 20 for 5 seconds.");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<PierceSpark>();
	}
}