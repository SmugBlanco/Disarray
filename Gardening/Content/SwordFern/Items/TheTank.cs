using Disarray.Gardening.Content.SwordFern.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Gardening.Content.SwordFern.Items
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