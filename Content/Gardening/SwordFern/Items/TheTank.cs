using Terraria.ModLoader;
using Disarray.Content.Gardening.SwordFern.Projectiles;
using Terraria;

namespace Disarray.Content.Gardening.SwordFern.Items
{
	public class TheTank : SwordFernCombatModifiers
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Tank");
			Tooltip.SetDefault("Conjures a shortlived spark that, upon hitting enemies, decreases your next incoming damage by 25%");
			Item.staff[item.type] = true;
		}

		public override int ProjectileType => ModContent.ProjectileType<TankSpark>();
	}
}